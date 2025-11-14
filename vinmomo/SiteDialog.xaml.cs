using System.Windows;

namespace vinmomo
{
    public partial class SiteDialog : Window
    {
        public SiteDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
