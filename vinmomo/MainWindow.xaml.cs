using System.Windows;
using vinmomo.ViewModels;

namespace vinmomo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
