using System.Windows;
using vinmomo.Models;
using vinmomo.ViewModels;

namespace vinmomo
{
    public partial class AdminServicesWindow : Window
    {
        private readonly AdminServicesViewModel _vm;

        public AdminServicesWindow()
        {
            InitializeComponent();

            _vm = new AdminServicesViewModel();
            DataContext = _vm;

            // chargement de la liste au démarrage
            Loaded += async (_, __) => await _vm.LoadAsync();
        }

        // === Ajouter ===
        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ServiceDialog
            {
                Owner = this,
                DataContext = new Service()
            };

            if (dialog.ShowDialog() == true)
            {
                var created = (Service)dialog.DataContext;
                await _vm.AddAsync(created);
                await _vm.LoadAsync();
            }
        }

        // === Modifier ===
        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (GridServices.SelectedItem is not Service selected)
                return;

            // clone pour ne pas modifier directement la ligne tant que l’utilisateur n’a pas validé
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

        // === Supprimer ===
        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GridServices.SelectedItem is not Service selected)
                return;

            if (MessageBox.Show("Supprimer ce service ?", "Confirmation",
                                MessageBoxButton.YesNo, MessageBoxImage.Question)
                != MessageBoxResult.Yes)
                return;

            await _vm.DeleteAsync(selected.Id);
            await _vm.LoadAsync();
        }
    }
}
