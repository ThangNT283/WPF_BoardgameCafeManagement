using BoardgameCafeManagement.ViewModels;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for StaffManageView.xaml
    /// </summary>
    public partial class TableManageView : Page
    {
        public TableManageView()
        {
            InitializeComponent();
            DataContext = new TableManageViewModel(this);
        }
    }
}
