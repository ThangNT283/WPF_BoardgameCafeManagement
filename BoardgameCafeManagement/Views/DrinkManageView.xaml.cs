using BoardgameCafeManagement.ViewModels;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for StaffManageView.xaml
    /// </summary>
    public partial class DrinkManageView : Page
    {
        public DrinkManageView()
        {
            InitializeComponent();
            DataContext = new DrinkManageViewModel(this);
        }
    }
}
