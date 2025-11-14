using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using vinmomo.Models;
using vinmomo.Services;

namespace vinmomo.ViewModels
{
    public class AdminServicesViewModel : INotifyPropertyChanged
    {
        private readonly ApiServiceService _api = new();

        public ObservableCollection<Service> Services { get; } = new();

        private Service _selectedService;
        public Service SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged();
            }
        }

        // Chargement de la liste
        public async Task LoadAsync()
        {
            Services.Clear();
            var list = await _api.GetServicesAsync();
            foreach (var s in list)
            {
                Services.Add(s);
            }
        }

        // Création
        public Task AddAsync(Service service)
        {
            return _api.AddServiceAsync(service);
        }

        // Mise à jour
        public Task UpdateAsync(Service service)
        {
            return _api.UpdateServiceAsync(service);
        }

        // Suppression
        public Task DeleteAsync(int id)
        {
            return _api.DeleteServiceAsync(id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
