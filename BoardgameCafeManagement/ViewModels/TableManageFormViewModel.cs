using BoardgameCafeManagement.Commands;
using BusinessObjects;
using BusinessObjects.Builders;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Text.RegularExpressions;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    public class TableManageFormViewModel : ViewModelBase
    {
        private readonly ITableService _tableService;
        private readonly Table? _selectedTable;

        private static string SPECIAL_CHAR_PATTERN = @"[!@#$%^&*(),.?""':{}|<>]";
        private static string LETTER_PATTERN = @"[a-zA-Z]";
        private static string SPACE_PATTERN = @"\s";

        #region Properties
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }
        private string _tableNumber;
        public string TableNumber
        {
            get { return _tableNumber; }
            set { _tableNumber = value; OnPropertyChanged(nameof(TableNumber)); }
        }
        private string _capacity;
        public string Capacity
        {
            get { return _capacity; }
            set { _capacity = value; OnPropertyChanged(nameof(Capacity)); }
        }
        private int _capacityValue = 0;
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }
        private string _actionName;
        public string ActionName
        {
            get { return _actionName; }
            set { _actionName = value; OnPropertyChanged(nameof(ActionName)); }
        }
        #endregion

        #region Errors
        private string _tableNumberError;
        public string TableNumberError
        {
            get { return _tableNumberError; }
            set { _tableNumberError = value; OnPropertyChanged(nameof(TableNumberError)); }
        }
        private string _capacityError;
        public string CapacityError
        {
            get { return _capacityError; }
            set { _capacityError = value; OnPropertyChanged(nameof(CapacityError)); }
        }
        #endregion

        #region Commands
        public RelayCommand Action { get; }
        #endregion

        public TableManageFormViewModel(string action, Table? selectedTable)
        {
            _tableService = new TableService();
            ActionName = action.ToUpper();
            _selectedTable = selectedTable;
            if (_selectedTable != null)
            {
                Id = _selectedTable.Id;
                TableNumber = _selectedTable.TableNumber;
                Capacity = _selectedTable.Capacity.ToString();
                Status = _selectedTable.Status;
            }

            if (action.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                Action = new RelayCommand(CreateTable);
            }
            else if (action.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                Action = new RelayCommand(UpdateTable);
            }
        }

        #region Actions
        private void CreateTable()
        {
            if (IsValidFields())
            {
                ITableBuilder builder = new TableBuilder();
                Table table = builder.SetTableNumber(TableNumber)
                    .SetCapacity(_capacityValue)
                    .SetStatus(Status)
                    .Build();
                bool isSuccess = _tableService.CreateTable(table);

                if (!isSuccess)
                {
                    MessageBox.Show("Failed to create table!");
                }
                else
                {
                    MessageBox.Show("Create table successfully!");
                }
            }
        }

        private void UpdateTable()
        {
            if (IsValidFields())
            {
                ITableBuilder builder = new TableBuilder();
                Table table = builder.SetId(_selectedTable.Id)
                    .SetTableNumber(TableNumber)
                    .SetCapacity(_capacityValue)
                    .SetStatus(Status)
                    .Build();
                bool isSuccess = _tableService.UpdateTable(table);

                if (!isSuccess)
                {
                    MessageBox.Show("Failed to update table!");
                }
                else
                {
                    MessageBox.Show("Update table successfully!");
                }
            }
        }
        #endregion

        #region Validating
        private bool IsValidFields()
        {
            bool isValid = true;

            isValid &= ValidateTableNumber();
            isValid &= ValidateCapacity();

            return isValid;
        }

        private bool ValidateTableNumber()
        {
            if (_tableNumber.IsNullOrEmpty())
            {
                TableNumberError = "Table number cannot be empty";
                return false;
            }

            TableNumber = _tableNumber.Trim();
            if (Regex.IsMatch(_tableNumber, SPECIAL_CHAR_PATTERN) || Regex.IsMatch(_tableNumber, SPACE_PATTERN))
            {
                TableNumberError = "No special character or space in table number";
                return false;
            }
            if (_tableNumber.Length != 4)
            {
                TableNumberError = "Table number must have length of 4";
                return false;
            }
            if (_tableService.GetTables().ToList()
                .Find(t => t.TableNumber == _tableNumber &&
                    (_selectedTable == null || t.Id != _selectedTable.Id)) is not null)
            {
                TableNumberError = "Table number cannot be duplicated";
                return false;
            }

            TableNumberError = string.Empty;
            return true;
        }

        private bool ValidateCapacity()
        {
            if (_capacity.IsNullOrEmpty())
            {
                CapacityError = "Capacity cannot be empty";
                return false;
            }

            Capacity = _capacity.Trim();
            if (Regex.IsMatch(_capacity, SPECIAL_CHAR_PATTERN) ||
                Regex.IsMatch(_capacity, SPACE_PATTERN) ||
                Regex.IsMatch(_capacity, LETTER_PATTERN))
            {
                CapacityError = "Capacity can only be number";
                return false;
            }

            try
            {
                _capacityValue = Convert.ToInt32(_capacity);
                if (_capacityValue <= 0)
                {
                    CapacityError = "Capacity must be larger than 0";
                    return false;
                }
            }
            catch (Exception ex)
            {
                CapacityError = "Invalid capacity";
                MessageBox.Show(ex.Message);
                return false;
            }

            CapacityError = string.Empty;
            return true;
        }
        #endregion
    }
}