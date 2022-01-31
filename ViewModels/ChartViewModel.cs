using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Threading;
using WpfSimpleTest.Helpers;

namespace WpfSimpleTest.ViewModels
{
    class ChartViewModel : BaseViewModel
    {
        #region Properties
        private double interval = 33;

        public double Interval
        {
            get { return interval; }
            set
            {
                interval = value;
                OnPropertyChanged("Interval");
            }
        }

        private int bufferLength = 5;

        public int BufferLength
        {
            get { return bufferLength; }
            set
            {
                bufferLength = value;
                OnPropertyChanged("BufferLength");
            }
        }

        private int pointsCount = 4096;

        public int PointsCount
        {
            get { return pointsCount; }
            set
            {
                pointsCount = value;
                OnPropertyChanged("PointsCount");
            }
        }

        private int selectedTab;
        public int SelectedTab
        {
            get { return selectedTab; }
            set
            {
                selectedTab = value;
                if(SelectedTab != 0 )
                {
                    intervalTimer.Stop();
                }
                else
                {
                    if (isRunning)
                        intervalTimer.Start();
                }
            }
        }

        private ObservableCollection<LineRenderableSeriesViewModel> renderableSeries;
        public ObservableCollection<LineRenderableSeriesViewModel> RenderableSeries
        {
            get { return renderableSeries; }
            set
            {
                renderableSeries = value;
                OnPropertyChanged("RenderableSeries");
            }
        }
        #endregion Properties

        #region Commands 

        private RelayCommand startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                return startCommand ??
                  (startCommand = new RelayCommand(obj => {

                      intervalTimer.Start();
                      isRunning = true;
                  }));
            }
        }

        private RelayCommand stopCommand;
        public RelayCommand StopCommand
        {
            get
            {
                return stopCommand ??
                  (stopCommand = new RelayCommand(obj => {
                      intervalTimer.Stop();
                      isRunning = false;
                  }));
            }
        }

        private RelayCommand changeIntervalCommand;
        public RelayCommand ChangeIntervalCommand
        {
            get
            {
                return changeIntervalCommand ??
                  (changeIntervalCommand = new RelayCommand(obj => intervalTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)Interval)));
            }
        }

        private RelayCommand updateChartCommand;
        public RelayCommand UpdateChartCommand
        {
            get
            {
                return updateChartCommand ??
                  (updateChartCommand = new RelayCommand(obj => {
                      InitData();
                  }));
            }
        }
        #endregion Commands

        private readonly DispatcherTimer intervalTimer;
        private double frequency = -0.1;
        private int offset;
        private double[] opacities;

        private bool isRunning;

        public ChartViewModel()
        {
            Mediator.Instance.Register(o => { SelectedTab = (int) o; }, ViewModelMessages.SelectedTab);

            RenderableSeries = new ObservableCollection<LineRenderableSeriesViewModel>();

            intervalTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, (int)Interval)
            };

           intervalTimer.Tick += OnNewData;

           InitData();
        }

        private void InitData()
        {
            RenderableSeries.Clear();
            offset = 0;

            for (int i = 0; i < BufferLength; i++)
            {
                RenderableSeries.Add(new LineRenderableSeriesViewModel()
                {
                    StrokeThickness = 2,
                    Stroke = Colors.DarkRed,
                    Opacity = 0
                }) ;
            }
            opacities = GetLinspace(0.1, 1, RenderableSeries.Count);

            FirstTick();
        }

        private void FirstTick()
        {
            for (int i = 0; i < RenderableSeries.Count; i++)
            {
                XyDataSeries<double, double> lineData = new XyDataSeries<double, double>(PointsCount);
                for (int ii = 0; ii < PointsCount; ii++)
                {
                    lineData.Append(ii, 100 * Math.Sin(ii * 0.002 + frequency) * 1);
                }

                RenderableSeries[i].DataSeries = lineData;
            }
        }

        private void OnNewData(object sender, EventArgs e)
        {
            UpdateSeries();
            UpdateOpacity();
            UpdateOffset();
        }

        private void UpdateSeries()
        {
            using (RenderableSeries[offset].DataSeries.SuspendUpdates())
            {
                double random = new Random().Next(140, 150);

                for (int i = 0; i < RenderableSeries[offset].DataSeries.Count; i++)
                {
                    ((XyDataSeries<double, double>)RenderableSeries[offset].DataSeries).Update(i, random * Math.Sin(i * 0.002 + frequency) * 1);
                }
            }
            frequency += 0.1;
        }

        private void UpdateOffset()
        {
            offset++;
            if(offset == RenderableSeries.Count)
                offset = 0;
        }

        private void UpdateOpacity()
        {
            int o = offset;

            for (int i = opacities.Length -1 ; i >= 0 ; i--)
            {
                RenderableSeries[o].Opacity = opacities[i];

                if (o == 0)
                    o = RenderableSeries.Count;
                o--;
            }
        }

        private double[] GetLinspace(double startval, double endval, int steps)
        {
            if (steps == 1)
                return new double[] { 1 };

            double interval = endval / Math.Abs(endval) * Math.Abs(endval - startval) / (steps - 1);
            double[] value = (from val in Enumerable.Range(0, steps)
                              select startval + (val * interval)).ToArray();
            return value;
        }
    }
}
