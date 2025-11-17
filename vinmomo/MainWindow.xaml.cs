using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using vinmomo.Models;
using vinmomo.ViewModels;

namespace vinmomo
{
    public partial class MainWindow : Window
    {
        private bool _isAdmin;
        private MainViewModel _vm;

        // Constructeur utilisé automatiquement par le XAML
        public MainWindow() : this(false)
        {
        }

        // Constructeur logique (lecture seule / admin)
        public MainWindow(bool isAdmin)
        {
            InitializeComponent();

            _vm = new MainViewModel();
            DataContext = _vm;

            _isAdmin = isAdmin;
            ConfigureMode();
        }

        private void ConfigureMode()
        {
            BtnAdd.Visibility = _isAdmin ? Visibility.Visible : Visibility.Collapsed;
            BtnEdit.Visibility = _isAdmin ? Visibility.Visible : Visibility.Collapsed;
            BtnDelete.Visibility = _isAdmin ? Visibility.Visible : Visibility.Collapsed;
            BtnExitAdmin.Visibility = _isAdmin ? Visibility.Visible : Visibility.Collapsed;

            BtnManageSites.Visibility = _isAdmin ? Visibility.Visible : Visibility.Collapsed;
            BtnManageServices.Visibility = _isAdmin ? Visibility.Visible : Visibility.Collapsed;
        }

        // ==============================
        //  COMBINAISON SECRÈTE ADMIN
        // ==============================
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl)
                && Keyboard.IsKeyDown(Key.LeftAlt)
                && e.Key == Key.A)
            {
                OpenLogin();
            }
        }

        private void OpenLogin()
        {
            var login = new LoginWindow();
            bool? result = login.ShowDialog();

            if (result == true && login.IsAdmin)
            {
                var admin = new MainWindow(true);
                admin.Show();
                Close();
            }
        }

        private void BtnExitAdmin_Click(object sender, RoutedEventArgs e)
        {
            var viewer = new MainWindow(false);
            viewer.Show();
            Close();
        }

        // ==============================
        // CRUD SALARIES
        // ==============================

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SalarieDialog(_vm.Services, _vm.Sites, null);
            if (dlg.ShowDialog() == true)
            {
                Salarie nouveau = dlg.Result;
                await _vm.AddSalarieAsync(nouveau);
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedSalarie == null)
            {
                MessageBox.Show("Sélectionne un salarié à modifier.");
                return;
            }

            var original = _vm.SelectedSalarie;
            var clone = new Salarie
            {
                Id = original.Id,
                Nom = original.Nom,
                Prenom = original.Prenom,
                TelephoneFixe = original.TelephoneFixe,
                TelephonePortable = original.TelephonePortable,
                Email = original.Email,
                SiteId = original.SiteId,
                ServiceId = original.ServiceId,
                Site = original.Site,
                Service = original.Service
            };

            var dlg = new SalarieDialog(_vm.Services, _vm.Sites, clone);
            if (dlg.ShowDialog() == true)
            {
                Salarie modifie = dlg.Result;
                await _vm.UpdateSalarieAsync(modifie);
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedSalarie == null)
            {
                MessageBox.Show("Sélectionne un salarié à supprimer.");
                return;
            }

            if (MessageBox.Show($"Supprimer {_vm.SelectedSalarie.Nom} {_vm.SelectedSalarie.Prenom} ?",
                                "Confirmation",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                await _vm.DeleteSelectedAsync();
            }
        }

        // ==============================
        //  GESTION DES SITES
        // ==============================
        private async void BtnManageSites_Click(object sender, RoutedEventArgs e)
        {
            var win = new AdminSitesWindow
            {
                Owner = this
            };

            win.ShowDialog();

            // refresh des ComboBox
            await _vm.LoadSitesAsync();
        }

        // ==============================
        //  GESTION DES SERVICES
        // ==============================
        private async void BtnManageServices_Click(object sender, RoutedEventArgs e)
        {
            var win = new AdminServicesWindow
            {
                Owner = this
            };

            win.ShowDialog();

            // refresh des ComboBox
            await _vm.LoadServicesAsync();
        }
    }
}
