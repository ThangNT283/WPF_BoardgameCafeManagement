using BoardgameCafeManagement.ViewModels;
using System.Windows.Controls;

namespace BoardgameCafeManagement.Views
{
    /// <summary>
    /// Interaction logic for StaffGameView.xaml
    /// </summary>
    public partial class StaffGameView : Page
    {
        public StaffGameView()
        {
            InitializeComponent();
            DataContext = new StaffGameViewModel();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
