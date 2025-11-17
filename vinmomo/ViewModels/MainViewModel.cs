using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        private Salarie _selectedSalarie;
        public Salarie SelectedSalarie
        {
            get => _selectedSalarie;
            set
            {
                _selectedSalarie = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            SalariesView = CollectionViewSource.GetDefaultView(Salaries);
            SalariesView.Filter = FilterPredicate;

            _ = LoadDataAsync();
        }

        // ======================================================================
        //   CHARGEMENT GLOBAL (sites + services + salariés)
        // ======================================================================

        public async Task LoadDataAsync()
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

                SelectedSalarie = Salaries.FirstOrDefault();
                SalariesView.Refresh();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erreur lors du chargement : {ex.Message}");
            }
        }

        // ======================================================================
        //   MÉTHODES UTILISÉES APRÈS MODIFICATION DANS LES FENÊTRES ADMIN
        // ======================================================================

        public async Task LoadSitesAsync()
        {
            Sites.Clear();
            var sites = await _siteService.GetSitesAsync();
            foreach (var s in sites)
                Sites.Add(s);

            SalariesView.Refresh();
        }

        public async Task LoadServicesAsync()
        {
            Services.Clear();
            var services = await _serviceService.GetServicesAsync();
            foreach (var s in services)
                Services.Add(s);

            SalariesView.Refresh();
        }

        // ======================================================================
        //   CRUD SALARIÉS
        // ======================================================================

        public async Task AddSalarieAsync(Salarie salarie)
        {
            await _salarieService.AddSalarieAsync(salarie);
            await LoadDataAsync();
        }

        public async Task UpdateSalarieAsync(Salarie salarie)
        {
            await _salarieService.UpdateSalarieAsync(salarie.Id, salarie);
            await LoadDataAsync();
        }

        public async Task DeleteSelectedAsync()
        {
            if (SelectedSalarie == null) return;

            await _salarieService.DeleteSalarieAsync(SelectedSalarie.Id);
            await LoadDataAsync();
        }

        // ======================================================================
        //   FILTRES / AFFICHAGE
        // ======================================================================

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

        // ======================================================================
        //   INotifyPropertyChanged
        // ======================================================================

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
