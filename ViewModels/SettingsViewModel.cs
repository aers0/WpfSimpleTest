using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WpfSimpleTest.Helpers;
using WpfSimpleTest.Models;

namespace WpfSimpleTest.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        private readonly AppDbContext _Context = new AppDbContext();
        public ObservableCollection<SettingsValue> Values { get; private set; }

        private RelayCommand _SaveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _SaveCommand ??
                  (_SaveCommand = new RelayCommand(obj => {

                      _Context.SaveChanges();
                  }));
            }
        }

        public SettingsViewModel()
        {
            _Context.Database.EnsureCreated();
            _Context.SettingsValue.Load();

            Values =_Context.SettingsValue.Local.ToObservableCollection();

        }

    }


}
