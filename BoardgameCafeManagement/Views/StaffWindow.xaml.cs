using BoardgameCafeManagement.ViewModels;
using System.Windows;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public StaffWindow()
        {
            InitializeComponent();
            DataContext = new StaffViewModel();
        }
    }
}
