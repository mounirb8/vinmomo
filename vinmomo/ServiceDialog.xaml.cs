using System.Windows;

namespace vinmomo
{
    public partial class ServiceDialog : Window
    {
        public ServiceDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
