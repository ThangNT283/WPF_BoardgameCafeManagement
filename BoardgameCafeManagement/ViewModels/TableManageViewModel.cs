using BoardgameCafeManagement.Commands;
using BoardgameCafeManagement.Views;
using BusinessObjects;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class TableManageViewModel : ViewModelBase
    {
        private readonly ITableService _tableService;
        private readonly TableManageView _tableManageView;

        #region Fields
        private ObservableCollection<Table> _tables;
        public ObservableCollection<Table> Tables
        {
            get { return _tables; }
            set { _tables = value; OnPropertyChanged(nameof(Tables)); }
        }
        private Table _selectedTable;
        public Table SelectedTable
        {
            get { return _selectedTable; }
            set { _selectedTable = value; OnPropertyChanged(nameof(SelectedTable)); }
        }
        private string _searchInput;
        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; OnPropertyChanged(nameof(SearchInput)); }
        }
        #endregion

        #region Commands
        public RelayCommand RefreshList { get; }
        public RelayCommand CreateTable { get; }
        public RelayCommand UpdateTable { get; }
        //public RelayCommand SearchTable { get; }
        #endregion

        public TableManageViewModel(TableManageView tableManageView)
        {
            _tableService = new TableService();
            _tableManageView = tableManageView;

            Refresh();

            RefreshList = new RelayCommand(Refresh);
            CreateTable = new RelayCommand(Create);
            UpdateTable = new RelayCommand(Update);
            //SearchTable = new RelayCommand(Search);
        }

        #region Actions
        private void Refresh()
        {
            Tables = _tableService.GetTables();
            SearchInput = string.Empty;
        }
        private void Create()
        {
            new TableManageForm(_tableManageView, "create", null).Show();
        }
        private void Update()
        {
            if (SelectedTable == null)
            {
                MessageBox.Show("Please choose a table to edit");
                return;
            }
            new TableManageForm(_tableManageView, "update", SelectedTable).Show();
        }
        private void Search()
        {

        }
        #endregion
    }
}
