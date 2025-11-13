
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using vinmomo.Data;
using vinmomo.Models;

namespace vinmomo.ViewModels
{
    public class AdminSalariesViewModel : ViewModelBase
    {
        private readonly AnnuaireContext _context = new AnnuaireContext();
        private ObservableCollection<Salarie> _salaries;

        public ObservableCollection<Salarie> Salaries
        {
            get { return _salaries; }
            set { _salaries = value; OnPropertyChanged(); }
        }

        public ICommand AddSalarieCommand { get; }
        public ICommand UpdateSalarieCommand { get; }
        public ICommand DeleteSalarieCommand { get; }

        public AdminSalariesViewModel()
        {
            Salaries = new ObservableCollection<Salarie>(_context.Salaries.Include(s => s.Site).Include(s => s.Service).ToList());
            AddSalarieCommand = new RelayCommand(AddSalarie);
            UpdateSalarieCommand = new RelayCommand(UpdateSalarie, CanUpdateOrDeleteSalarie);
            DeleteSalarieCommand = new RelayCommand(DeleteSalarie, CanUpdateOrDeleteSalarie);
        }

        private void AddSalarie(object obj)
        {
            // Placeholder for adding a new employee. A dedicated window or user control would be needed here.
            System.Windows.MessageBox.Show("Add Salarie functionality not implemented yet.");
        }

        private void UpdateSalarie(object obj)
        {
            // Placeholder for updating an employee. A dedicated window or user control would be needed here.
            System.Windows.MessageBox.Show("Update Salarie functionality not implemented yet.");
        }

        private void DeleteSalarie(object obj)
        {
            if (obj is Salarie salarie)
            {
                _context.Salaries.Remove(salarie);
                _context.SaveChanges();
                Salaries.Remove(salarie);
            }
        }

        private bool CanUpdateOrDeleteSalarie(object obj)
        {
            return obj is Salarie;
        }
    }
}
