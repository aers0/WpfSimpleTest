using System.Windows.Controls;
using WpfSimpleTest.ViewModels;

namespace WpfSimpleTest.Views
{
    partial class Chart : UserControl
    {
        public Chart()
        {
            InitializeComponent();
            DataContext = new ChartViewModel();
        }
    }
}
