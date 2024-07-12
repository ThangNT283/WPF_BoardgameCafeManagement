using BoardgameCafeManagement.ViewModels;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for BillManageView.xaml
    /// </summary>
    public partial class BillManageView : Page
    {
        public BillManageView()
        {
            InitializeComponent();
            DataContext = new BillManageViewModel();
        }
    }
}
