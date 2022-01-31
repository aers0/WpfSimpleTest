using System.Windows;
using WpfSimpleTest.ViewModels;

namespace WpfSimpleTest.Views
{
    partial class MainWindow : Window
    {
       public MainWindow()
        {
            DataContext = new MainWindowViewModel();
        }
    }
}
