using System.Windows;
using vinmomo.Models;
using vinmomo.ViewModels;
using vinmomo.Services;
using System.Linq;
using System.Threading.Tasks;

namespace vinmomo
{
    public partial class AdminServicesWindow : Window
    {
        private readonly AdminServicesViewModel _vm = new();

        public AdminServicesWindow()
        {
            InitializeComponent();

            DataContext = _vm;

            Loaded += AdminServicesWindow_Loaded;
        }

        private async void AdminServicesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _vm.LoadAsync();
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var service = new Service();

            var dialog = new ServiceDialog
            {
                Owner = this,
                DataContext = service
            };

            if (dialog.ShowDialog() == true)
            {
                await _vm.AddAsync(service);
                await _vm.LoadAsync();
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (GridServices.SelectedItem is not Service selected)
                return;

            var clone = new Service
            {
                Id = selected.Id,
                Nom = selected.Nom
            };

            var dialog = new ServiceDialog
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
            if (GridServices.SelectedItem is not Service selected)
                return;

            // Vérifier si le service est utilisé par un salarié
            var salaries = await new ApiSalarieService().GetSalariesAsync();
            bool serviceUtilise = salaries.Any(s => s.ServiceId == selected.Id);

            if (serviceUtilise)
            {
                MessageBox.Show(
                    "Impossible de supprimer ce service car des salariés y sont rattachés.",
                    "Suppression interdite",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            // Confirmation
            if (MessageBox.Show("Supprimer ce service ?", "Confirmation",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            await _vm.DeleteAsync(selected.Id);
            await _vm.LoadAsync();
        }
    }
}
