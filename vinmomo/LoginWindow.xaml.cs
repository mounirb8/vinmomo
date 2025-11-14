using System.Windows;

namespace vinmomo
{
    public partial class LoginWindow : Window
    {
        public bool IsAdmin { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            IsAdmin = false;
        }

        private void BtnConnexionAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (TxtLogin.Text == "admin" && TxtPassword.Password == "admin123")
            {
                IsAdmin = true;
                DialogResult = true;
                Close();
            }
            else
            {
                LblError.Text = "Identifiants incorrects.";
            }
        }

        private void BtnLectureSeule_Click(object sender, RoutedEventArgs e)
        {
            IsAdmin = false;
            DialogResult = false;
            Close();
        }
    }
}
