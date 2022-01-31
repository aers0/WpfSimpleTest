using System.Windows.Controls;
using WpfSimpleTest.ViewModels;

namespace WpfSimpleTest.Views
{
    partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }
    }
}
