using BoardgameCafeManagement.ViewModels;
using System.Windows;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            DataContext = new AdminViewModel();
        }
    }
}
