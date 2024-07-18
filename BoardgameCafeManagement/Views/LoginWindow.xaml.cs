using BoardgameCafeManagement.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace BoardgameCafeManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(IConfiguration configuration)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(configuration, this);
        }
    }
}