using BoardgameCafeManagement.ViewModels;
using BusinessObjects;
using System.Windows;

namespace BoardgameCafeManagement.Views
{
    public partial class StaffManageForm : Window
    {
        private readonly StaffManageView _staffManageView;

        public StaffManageForm(StaffManageView staffManageView, string action, Staff selectedStaff)
        {
            InitializeComponent();
            DataContext = new StaffManageFormViewModel(action, selectedStaff);
            _staffManageView = staffManageView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dataContext = _staffManageView.DataContext as StaffManageViewModel;
            if (dataContext != null)
            {
                dataContext.RefreshList.Execute(null);
            }
        }
    }
}
