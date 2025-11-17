using System.Windows;
using vinmomo.Models;
using vinmomo.ViewModels;
using vinmomo.Services;
using System.Linq;
using System.Threading.Tasks;

namespace vinmomo
{
    public partial class AdminSitesWindow : Window
    {
        private readonly AdminSitesViewModel _vm = new();

        public AdminSitesWindow()
        {
            InitializeComponent();

            DataContext = _vm;

            Loaded += AdminSitesWindow_Loaded;
        }

        private async void AdminSitesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _vm.LoadAsync();
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var site = new Site();

            var dialog = new SiteDialog
            {
                Owner = this,
                DataContext = site
            };

            if (dialog.ShowDialog() == true)
            {
                await _vm.AddAsync(site);
                await _vm.LoadAsync();
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (GridSites.SelectedItem is not Site selected)
                return;

            var clone = new Site
            {
                Id = selected.Id,
                Ville = selected.Ville
            };

            var dialog = new SiteDialog
            {
                Owner = this,
                DataContext = clone
            };

            if (dialog.ShowDialog() == true)
            {
                await _vm.UpdateAsync(clone);
                await _vm.LoadAsync();
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GridSites.SelectedItem is not Site selected)
                return;

            // Vérifier si le site est utilisé par un salarié
            var salaries = await new ApiSalarieService().GetSalariesAsync();
            bool siteUtilise = salaries.Any(s => s.SiteId == selected.Id);

            if (siteUtilise)
            {
                MessageBox.Show(
                    "Impossible de supprimer ce site car des salariés y sont rattachés.",
                    "Suppression interdite",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            // Confirmation
            if (MessageBox.Show("Supprimer ce site ?", "Confirmation",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            await _vm.DeleteAsync(selected.Id);
            await _vm.LoadAsync();
        }
    }
}
