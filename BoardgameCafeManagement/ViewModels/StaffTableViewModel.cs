using BoardgameCafeManagement.Commands;
using BusinessObjects;
using BusinessObjects.Builders;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class StaffTableViewModel : ViewModelBase
    {
        private readonly ITableService _tableService;

        #region Fields
        private ObservableCollection<Table> _tables;
        public ObservableCollection<Table> Tables
        {
            get { return _tables; }
            set { _tables = value; OnPropertyChanged(nameof(Tables)); }
        }
        private Table? _selectedTable;
        public Table? SelectedTable
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
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }
        private string? _selectedTableNumber;
        public string? SelectedTableNumber
        {
            get { return _selectedTableNumber; }
            set { _selectedTableNumber = value; OnPropertyChanged(nameof(SelectedTableNumber)); }
        }
        #endregion

        #region Commands
        public RelayCommand TableSelectionChanged { get; }
        public RelayCommand UpdateTable { get; }
        #endregion

        public StaffTableViewModel()
        {
            _tableService = new TableService();

            Tables = _tableService.GetTables();

            TableSelectionChanged = new RelayCommand(LoadDataFields);
            UpdateTable = new RelayCommand(UpdateStatus);
        }

        #region Actions
        private void LoadDataFields()
        {
            if (SelectedTable != null)
            {
                SelectedTableNumber = SelectedTable.TableNumber;
                Status = SelectedTable.Status;
            }
        }

        private void UpdateStatus()
        {
            if (SelectedTableNumber.IsNullOrEmpty())
            {
                MessageBox.Show("Please choose a table to update");
                return;
            }

            ITableBuilder tableBuilder = new TableBuilder();
            Table table = tableBuilder
                .SetId(SelectedTable.Id)
                .SetTableNumber(SelectedTableNumber)
                .SetCapacity(SelectedTable.Capacity)
                .SetStatus(Status)
                .Build();
            bool isSuccess = _tableService.UpdateTable(table);
            if (isSuccess)
            {
                MessageBox.Show("Update table successfully!");
            }
            else
            {
                MessageBox.Show("Failed to update table");
            }

            Refresh();
        }

        private void Refresh()
        {
            SelectedTableNumber = "";
            Status = false;
            SelectedTable = null;

            Tables = _tableService.GetTables();
        }
        #endregion
    }
}
