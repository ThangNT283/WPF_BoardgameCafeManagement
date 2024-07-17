using BoardgameCafeManagement.ViewModels;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for StaffManageView.xaml
    /// </summary>
    public partial class GameManageView : Page
    {
        public GameManageView()
        {
            InitializeComponent();
            DataContext = new GameManageViewModel(this);
        }
    }
}
