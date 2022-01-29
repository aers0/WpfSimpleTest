using System.Windows.Controls;
using WpfSimpleTest.ViewModels;

namespace WpfSimpleTest.Views
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class Chart : UserControl
    {
        public Chart()
        {
            InitializeComponent();
            DataContext = new ChartViewModel();
        }
    }
}
