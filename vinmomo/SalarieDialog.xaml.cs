using System.Collections.Generic;
using System.Linq;
using System.Windows;
using vinmomo.Models;

namespace vinmomo
{
    public partial class SalarieDialog : Window
    {
        public Salarie Result { get; private set; }

        public SalarieDialog(IEnumerable<Service> services, IEnumerable<Site> sites, Salarie salarie = null)
        {
            InitializeComponent();

            CmbService.ItemsSource = services;
            CmbSite.ItemsSource = sites;

            if (salarie != null)
            {
                Result = new Salarie
                {
                    Id = salarie.Id,
                    Nom = salarie.Nom,
                    Prenom = salarie.Prenom,
                    TelephoneFixe = salarie.TelephoneFixe,
                    TelephonePortable = salarie.TelephonePortable,
                    Email = salarie.Email,
                    SiteId = salarie.SiteId,
                    ServiceId = salarie.ServiceId,
                    Site = salarie.Site,
                    Service = salarie.Service
                };

                TxtNom.Text = salarie.Nom;
                TxtPrenom.Text = salarie.Prenom;
                TxtTelFixe.Text = salarie.TelephoneFixe;
                TxtTelPort.Text = salarie.TelephonePortable;
                TxtEmail.Text = salarie.Email;

                if (salarie.Service != null)
                    CmbService.SelectedItem = services.FirstOrDefault(s => s.Id == salarie.ServiceId);

                if (salarie.Site != null)
                    CmbSite.SelectedItem = sites.FirstOrDefault(s => s.Id == salarie.SiteId);
            }
            else
            {
                Result = new Salarie();
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (CmbService.SelectedItem is not Service service ||
                CmbSite.SelectedItem is not Site site)
            {
                MessageBox.Show("Sélectionne un service et un site.");
                return;
            }

            Result.Nom = TxtNom.Text;
            Result.Prenom = TxtPrenom.Text;
            Result.TelephoneFixe = TxtTelFixe.Text;
            Result.TelephonePortable = TxtTelPort.Text;
            Result.Email = TxtEmail.Text;

            Result.ServiceId = service.Id;
            Result.SiteId = site.Id;
            Result.Service = service;
            Result.Site = site;

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
