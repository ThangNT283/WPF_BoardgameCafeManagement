using BoardgameCafeManagement.ViewModels;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for StaffTableView.xaml
    /// </summary>
    public partial class StaffTableView : Page
    {
        public StaffTableView()
        {
            InitializeComponent();
            DataContext = new StaffTableViewModel();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
