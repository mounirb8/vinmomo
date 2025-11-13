
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.VisualBasic;
using vinmomo.Data;
using vinmomo.Models;

namespace vinmomo.ViewModels
{
    public class AdminSitesViewModel : ViewModelBase
    {
        private readonly AnnuaireContext _context = new AnnuaireContext();
        private ObservableCollection<Site> _sites;

        public ObservableCollection<Site> Sites
        {
            get { return _sites; }
            set { _sites = value; OnPropertyChanged(); }
        }

        public ICommand AddSiteCommand { get; }
        public ICommand UpdateSiteCommand { get; }
        public ICommand DeleteSiteCommand { get; }

        public AdminSitesViewModel()
        {
            Sites = new ObservableCollection<Site>(_context.Sites.ToList());
            AddSiteCommand = new RelayCommand(AddSite);
            UpdateSiteCommand = new RelayCommand(UpdateSite, CanUpdateOrDeleteSite);
            DeleteSiteCommand = new RelayCommand(DeleteSite, CanUpdateOrDeleteSite);
        }

        private void AddSite(object obj)
        {
            // For simplicity, we'll use an input dialog. A dedicated view would be better.
            var newSiteName = Microsoft.VisualBasic.Interaction.InputBox("Enter new site name:", "Add Site", "");
            if (!string.IsNullOrWhiteSpace(newSiteName))
            {
                var newSite = new Site { Ville = newSiteName };
                _context.Sites.Add(newSite);
                _context.SaveChanges();
                Sites.Add(newSite);
            }
        }

        private void UpdateSite(object obj)
        {
            if (obj is Site site)
            {
                var updatedSiteName = Microsoft.VisualBasic.Interaction.InputBox("Enter updated site name:", "Update Site", site.Ville);
                if (!string.IsNullOrWhiteSpace(updatedSiteName))
                {
                    site.Ville = updatedSiteName;
                    _context.Sites.Update(site);
                    _context.SaveChanges();
                    Sites = new ObservableCollection<Site>(_context.Sites.ToList()); // Refresh
                }
            }
        }

        private void DeleteSite(object obj)
        {
            if (obj is Site site)
            {
                _context.Sites.Remove(site);
                _context.SaveChanges();
                Sites.Remove(site);
            }
        }

        private bool CanUpdateOrDeleteSite(object obj)
        {
            return obj is Site;
        }
    }
}
