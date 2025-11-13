using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using vinmomo.Models;
using vinmomo.Services;

namespace vinmomo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApiSalarieService _salarieService = new();
        private readonly ApiSiteService _siteService = new();
        private readonly ApiServiceService _serviceService = new();

        public ObservableCollection<Salarie> Salaries { get; } = new();
        public ObservableCollection<Site> Sites { get; } = new();
        public ObservableCollection<Service> Services { get; } = new();

        public ICollectionView SalariesView { get; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SalariesView.Refresh();
            }
        }

        private Site _selectedSite;
        public Site SelectedSite
        {
            get => _selectedSite;
            set
            {
                _selectedSite = value;
                OnPropertyChanged();
                SalariesView.Refresh();
            }
        }

        private Service _selectedService;
        public Service SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged();
                SalariesView.Refresh();
            }
        }

        public MainViewModel()
        {
            SalariesView = CollectionViewSource.GetDefaultView(Salaries);
            SalariesView.Filter = FilterPredicate;
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                // --- Sites ---
                Sites.Clear();
                var sites = await _siteService.GetSitesAsync();
                foreach (var s in sites)
                    Sites.Add(s);

                // --- Services ---
                Services.Clear();
                var services = await _serviceService.GetServicesAsync();
                foreach (var s in services)
                    Services.Add(s);

                // --- Salaries ---
                Salaries.Clear();
                var salaries = await _salarieService.GetSalariesAsync();
                foreach (var s in salaries)
                    Salaries.Add(s);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erreur lors du chargement : {ex.Message}");
            }
        }

        private bool FilterPredicate(object obj)
        {
            if (obj is not Salarie s) return false;

            bool matchSearch =
                string.IsNullOrWhiteSpace(SearchText)
                || (s.Nom?.ToLower().Contains(SearchText.ToLower()) ?? false)
                || (s.Prenom?.ToLower().Contains(SearchText.ToLower()) ?? false);

            bool matchSite = SelectedSite == null || s.SiteId == SelectedSite.Id;
            bool matchService = SelectedService == null || s.ServiceId == SelectedService.Id;

            return matchSearch && matchSite && matchService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
