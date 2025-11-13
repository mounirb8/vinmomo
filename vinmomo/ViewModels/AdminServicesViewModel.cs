
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.VisualBasic;
using vinmomo.Data;
using vinmomo.Models;

namespace vinmomo.ViewModels
{
    public class AdminServicesViewModel : ViewModelBase
    {
        private readonly AnnuaireContext _context = new AnnuaireContext();
        private ObservableCollection<Service> _services;

        public ObservableCollection<Service> Services
        {
            get { return _services; }
            set { _services = value; OnPropertyChanged(); }
        }

        public ICommand AddServiceCommand { get; }
        public ICommand UpdateServiceCommand { get; }
        public ICommand DeleteServiceCommand { get; }

        public AdminServicesViewModel()
        {
            Services = new ObservableCollection<Service>(_context.Services.ToList());
            AddServiceCommand = new RelayCommand(AddService);
            UpdateServiceCommand = new RelayCommand(UpdateService, CanUpdateOrDeleteService);
            DeleteServiceCommand = new RelayCommand(DeleteService, CanUpdateOrDeleteService);
        }

        private void AddService(object obj)
        {
            var newServiceName = Microsoft.VisualBasic.Interaction.InputBox("Enter new service name:", "Add Service", "");
            if (!string.IsNullOrWhiteSpace(newServiceName))
            {
                var newService = new Service { Nom = newServiceName };
                _context.Services.Add(newService);
                _context.SaveChanges();
                Services.Add(newService);
            }
        }

        private void UpdateService(object obj)
        {
            if (obj is Service service)
            {
                var updatedServiceName = Microsoft.VisualBasic.Interaction.InputBox("Enter updated service name:", "Update Service", service.Nom);
                if (!string.IsNullOrWhiteSpace(updatedServiceName))
                {
                    service.Nom = updatedServiceName;
                    _context.Services.Update(service);
                    _context.SaveChanges();
                    Services = new ObservableCollection<Service>(_context.Services.ToList()); // Refresh
                }
            }
        }

        private void DeleteService(object obj)
        {
            if (obj is Service service)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
                Services.Remove(service);
            }
        }

        private bool CanUpdateOrDeleteService(object obj)
        {
            return obj is Service;
        }
    }
}
