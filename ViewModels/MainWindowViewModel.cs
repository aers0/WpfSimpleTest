using WpfSimpleTest.Helpers;

namespace WpfSimpleTest.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private int selectedTab = 0;

        public int SelectedTab
        {
            get { return selectedTab; }
            set
            {
                selectedTab = value;
                Mediator.Instance.NotifyColleagues(ViewModelMessages.SelectedTab, selectedTab);
            }
        }

  
}
}
