using BoardgameCafeManagement.ViewModels;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for StaffBillView.xaml
    /// </summary>
    public partial class StaffBillView : Page
    {
        public StaffBillView()
        {
            InitializeComponent();
            DataContext = new StaffBillViewModel();
        }
    }
}
