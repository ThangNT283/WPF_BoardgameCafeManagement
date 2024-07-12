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
            DataContext = new LoginViewModel(configuration);
        }

        //private void btnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    var res = MessageBox.Show("The application will be closed. Continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (res == MessageBoxResult.Yes)
        //    {
        //        this.Close();
        //    }
        //}
    }
}