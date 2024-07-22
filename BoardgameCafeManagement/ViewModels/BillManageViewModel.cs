using BoardgameCafeManagement.Commands;
using BoardgameCafeManagement.Views;
using BusinessObjects;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BoardgameCafeManagement.ViewModels
{
    public class BillManageViewModel : ViewModelBase
    {
        private readonly IBillService _billService;
        private readonly ITableService _tableService;

        #region Fields
        private ObservableCollection<Bill> _bills;
        public ObservableCollection<Bill> Bills
        {
            get { return _bills; }
            set { _bills = value; OnPropertyChanged(nameof(Bills)); }
        }
        private Bill _selectedBill;
        public Bill SelectedBill
        {
            get { return _selectedBill; }
            set { _selectedBill = value; OnPropertyChanged(nameof(SelectedBill)); }
        }
        private int _tableId;
        public int TableId
        {
            get { return _tableId; }
            set { _tableId = value; OnPropertyChanged(nameof(TableId)); }
        }
        private ObservableCollection<Table> _tables;
        public ObservableCollection<Table> Tables
        {
            get { return _tables; }
            set { _tables = value; OnPropertyChanged(nameof(Tables)); }
        }
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }
        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; OnPropertyChanged(nameof(CustomerName)); }
        }
        #endregion

        #region Commands
        public RelayCommand RefreshList { get; }
        public RelayCommand ViewBill { get; }
        public RelayCommand SearchBill { get; }
        public RelayCommand RefreshTableOption { get; }
        #endregion

        public BillManageViewModel()
        {
            _billService = new BillService();
            _tableService = new TableService();

            Tables = _tableService.GetTables();
            TableId = -1;
            Refresh();

            RefreshList = new RelayCommand(Refresh);
            ViewBill = new RelayCommand(View);
            SearchBill = new RelayCommand(Search);
            RefreshTableOption = new RelayCommand(RefreshTable);
        }

        #region Actions
        private void Refresh()
        {
            Bills = _billService.GetBills();
            TableId = 1;
            CustomerName = string.Empty;
            StartDate = null;
            EndDate = null;
        }
        private void View()
        {
            if (SelectedBill == null)
            {
                MessageBox.Show("Please choose a bill to view details");
                return;
            }

            new BillDetailCard(SelectedBill).Show();
        }
        private void Search()
        {
            if (EndDate <= StartDate)
            {
                MessageBox.Show("End date must be later than start date");
                return;
            }

            CustomerName = Regex.Replace(CustomerName.Trim(), @"\s+", " ");
            Bills = _billService.SearchBill(TableId, CustomerName, StartDate, EndDate);
        }
        private void RefreshTable()
        {
            TableId = -1;
        }
        #endregion
    }
}
