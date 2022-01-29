using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Threading;
using WpfSimpleTest.Helpers;

namespace WpfSimpleTest.ViewModels
{
    class ChartViewModel : BaseViewModel
    {

        #region Properties
        private double _Interval = 33;

        public double Interval
        {
            get { return _Interval; }
            set
            {
                _Interval = value;
                OnPropertyChanged("Interval");
            }
        }

        private int _BufferLength = 5;

        public int BufferLength
        {
            get { return _BufferLength; }
            set
            {
                _BufferLength = value;
                OnPropertyChanged("BufferLength");
            }
        }

        private int _DataPoints = 4096;

        public int DataPoints
        {
            get { return _DataPoints; }
            set
            {
                _DataPoints = value;
                OnPropertyChanged("DataPoints");
            }
        }

        private ObservableCollection<LineRenderableSeriesViewModel> _renderableSeries;
        public ObservableCollection<LineRenderableSeriesViewModel> RenderableSeries
        {
            get { return _renderableSeries; }
            set
            {
                _renderableSeries = value;
                OnPropertyChanged("RenderableSeries");
            }
        }

        #endregion

        #region Commands 

        private RelayCommand _StartCommand;
        public RelayCommand StartCommand
        {
            get
            {
                return _StartCommand ??
                  (_StartCommand = new RelayCommand(obj => Start()));
            }
        }

        private RelayCommand _StopCommand;
        public RelayCommand StopCommand
        {
            get
            {
                return _StopCommand ??
                  (_StopCommand = new RelayCommand(obj => Stop()));
            }
        }

        private RelayCommand _ChangeIntervalCommand;
        public RelayCommand ChangeIntervalCommand
        {
            get
            {
                return _ChangeIntervalCommand ??
                  (_ChangeIntervalCommand = new RelayCommand(obj => ChangeInterval()));
            }
        }

        private RelayCommand _ChangeBufferCommand;
        public RelayCommand ChangeBufferCommand
        {
            get
            {
                return _ChangeBufferCommand ??
                  (_ChangeBufferCommand = new RelayCommand(obj => ChangeBuffer()));
            }
        }

        #endregion

        private readonly DispatcherTimer IntervalTimer;
        private double Frequency = -0.1;

        public ChartViewModel()
        {
           RenderableSeries = new ObservableCollection<LineRenderableSeriesViewModel>();
           IntervalTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, (int)Interval)
            };
           IntervalTimer.Tick += OnNewData;
        }

        private void OnNewData(object sender, EventArgs e)
        {
            AddSeries();
            UpdateOpacity();
        }

        private void AddSeries()
        {
            var sequnce = DataGenerator.GetSinwave(DataPoints, Frequency);
            Frequency += 0.01;

            var lineData = new XyDataSeries<double>();
            for (int i = 0; i < sequnce.Length; i++)
            {
                lineData.Append(i, sequnce[i]);
            }

            var series = new LineRenderableSeriesViewModel()
            {
                StrokeThickness = 2,
                Stroke = Colors.DarkRed,
                Opacity = 1,
                DataSeries = lineData,
            };

            if (RenderableSeries.Count > BufferLength)
                RenderableSeries.RemoveAt(RenderableSeries.Count - (1 + BufferLength));

            RenderableSeries.Add(series);

        }
        private void UpdateOpacity()
        {
            double[] opactities = DataGenerator.GetLinspace(0.1, 1, RenderableSeries.Count);

            for (int i = 0; i < RenderableSeries.Count - 1; i++)
            {
                RenderableSeries[i].Opacity = opactities[i];
            }
        }

        private void ChangeInterval()
        {
            IntervalTimer.Interval = new TimeSpan(0, 0, 0, 0,(int)Interval);
        }

        private void ChangeBuffer()
        {
            if (RenderableSeries.Count > BufferLength)

                for (int i = 0; i < RenderableSeries.Count - 1; i++)
                {
                    RenderableSeries.RemoveAt(i);
                }
        }

        public void Start()
        {
            IntervalTimer.Start();
        }

        public void Stop()
        {
            IntervalTimer.Stop();
        }
    }
}
