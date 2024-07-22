using BoardgameCafeManagement.Commands;
using BusinessObjects;
using BusinessObjects.Builders;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class StaffTableViewModel : ViewModelBase
    {
        private readonly ITableService _tableService;

        private static string SPECIAL_CHAR_PATTERN = @"[!@#$%^&*(),.?""':{}|<>]";
        private static string LETTER_PATTERN = @"[a-zA-Z]";
        private static string SPACE_PATTERN = @"\s";

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
        private string _capacity;
        public string Capacity
        {
            get { return _capacity; }
            set { _capacity = value; OnPropertyChanged(nameof(Capacity)); }
        }
        private string _capacityError;
        public string CapacityError
        {
            get { return _capacityError; }
            set { _capacityError = value; OnPropertyChanged(nameof(CapacityError)); }
        }
        #endregion

        #region Commands
        public RelayCommand TableSelectionChanged { get; }
        public RelayCommand UpdateTable { get; }
        public RelayCommand SearchTable { get; }
        #endregion

        public StaffTableViewModel()
        {
            _tableService = new TableService();

            Tables = _tableService.GetTables();

            TableSelectionChanged = new RelayCommand(LoadDataFields);
            UpdateTable = new RelayCommand(UpdateStatus);
            SearchTable = new RelayCommand(Search);
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

        private void Search()
        {
            if (SearchInput != null && Capacity != null)
            {
                SearchInput = Regex.Replace(SearchInput.Trim(), @"\s+", " ");
                Capacity = Capacity.Trim();
            }

            Tables = _tableService.SearchTableByNumber(SearchInput);
            if (!Capacity.IsNullOrEmpty() && IsValidCapacity())
            {
                Tables = new ObservableCollection<Table>(Tables.
                    Where(t => t.Capacity == Convert.ToInt32(Capacity)));
            }
        }

        private void Refresh()
        {
            SelectedTableNumber = "";
            Status = false;
            SelectedTable = null;

            Search();
        }
        #endregion

        #region Validation
        private bool IsValidCapacity()
        {
            Capacity = _capacity.Trim();
            if (Regex.IsMatch(_capacity, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_capacity, SPACE_PATTERN) ||
                Regex.IsMatch(_capacity, LETTER_PATTERN))
            {
                CapacityError = "Invalid";
                return false;
            }

            try
            {
                if (!_capacity.IsNullOrEmpty() && Convert.ToInt32(_capacity) <= 0)
                {
                    CapacityError = "Invalid";
                    return false;
                }
            }
            catch (Exception ex)
            {
                CapacityError = "Invalid";
                MessageBox.Show(ex.Message);
                return false;
            }

            CapacityError = string.Empty;
            return true;
        }
        #endregion
    }
}
