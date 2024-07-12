using BoardgameCafeManagement.ViewModels;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for StaffManageView.xaml
    /// </summary>
    public partial class StaffManageView : Page
    {
        public StaffManageView()
        {
            InitializeComponent();
            DataContext = new StaffManageViewModel(this);
        }
    }
}
