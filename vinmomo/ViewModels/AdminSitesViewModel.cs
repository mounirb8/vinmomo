using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using vinmomo.Models;
using vinmomo.Services;

namespace vinmomo.ViewModels
{
    public class AdminSitesViewModel : INotifyPropertyChanged
    {
        private readonly ApiSiteService _service = new();

        public ObservableCollection<Site> Sites { get; } = new();

        private Site _selectedSite;
        public Site SelectedSite
        {
            get => _selectedSite;
            set
            {
                _selectedSite = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            Sites.Clear();
            var list = await _service.GetSitesAsync();
            foreach (var s in list)
                Sites.Add(s);
        }

        public async Task AddAsync(Site site)
        {
            await _service.AddSiteAsync(site);
        }

        public async Task UpdateAsync(Site site)
        {
            await _service.UpdateSiteAsync(site.Id, site);
        }

        public async Task DeleteAsync(int id)
        {
            await _service.DeleteSiteAsync(id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
