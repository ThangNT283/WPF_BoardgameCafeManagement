using BoardgameCafeManagement.ViewModels;
using BusinessObjects;
using System.Windows;

namespace BoardgameCafeManagement.Views
{
    public partial class TableManageForm : Window
    {
        private readonly TableManageView _tableManageView;

        public TableManageForm(TableManageView tableManageView, string action, Table selectedTable)
        {
            InitializeComponent();
            DataContext = new TableManageFormViewModel(action, selectedTable);
            _tableManageView = tableManageView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dataContext = _tableManageView.DataContext as TableManageViewModel;
            if (dataContext != null)
            {
                dataContext.RefreshList.Execute(null);
            }
        }
    }
}
