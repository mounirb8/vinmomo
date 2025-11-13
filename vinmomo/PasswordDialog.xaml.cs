using System.Windows;

namespace vinmomo
{
    public partial class PasswordDialog : Window
    {
        public string Password
        {
            get { return PasswordBox.Password; }
        }

        public PasswordDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
