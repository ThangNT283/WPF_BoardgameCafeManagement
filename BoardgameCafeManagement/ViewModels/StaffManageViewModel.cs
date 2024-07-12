using BoardgameCafeManagement.Commands;
using BoardgameCafeManagement.Views;
using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace BoardgameCafeManagement.ViewModels
{
    class StaffManageViewModel : ViewModelBase
    {
        private readonly IStaffService _staffService;
        private readonly StaffManageView _staffManageView;

        #region Fields
        private ObservableCollection<Staff> _staffs;
        public ObservableCollection<Staff> Staffs
        {
            get { return _staffs; }
            set { _staffs = value; OnPropertyChanged(nameof(Staffs)); }
        }
        private Staff _selectedStaff;
        public Staff SelectedStaff
        {
            get { return _selectedStaff; }
            set { _selectedStaff = value; OnPropertyChanged(nameof(SelectedStaff)); }
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
        public RelayCommand CreateStaff { get; }
        public RelayCommand UpdateStaff { get; }
        public RelayCommand SearchStaff { get; }
        #endregion

        public StaffManageViewModel(StaffManageView staffManageView)
        {
            _staffService = new StaffService();
            _staffManageView = staffManageView;

            Refresh();

            RefreshList = new RelayCommand(Refresh);
            CreateStaff = new RelayCommand(Create);
            UpdateStaff = new RelayCommand(Update);
            SearchStaff = new RelayCommand(Search);
        }

        #region Actions
        private void Refresh()
        {
            //Staffs = new ObservableCollection<Staff>(_context.Staff);
            Staffs = _staffService.GetStaffs();
            SearchInput = string.Empty;
        }
        private void Create()
        {
            new StaffManageForm(_staffManageView, "create", null).Show();
        }
        private void Update()
        {
            if (SelectedStaff == null)
            {
                MessageBox.Show("Please choose a staff to edit");
                return;
            }
            new StaffManageForm(_staffManageView, "update", SelectedStaff).Show();
        }
        private void Search()
        {
            if (SearchInput.IsNullOrEmpty())
            {
                Refresh();
                return;
            }

            SearchInput = Regex.Replace(SearchInput.Trim(), @"\s+", " ");
            Staffs = _staffService.SearchStaff(SearchInput);
        }
        #endregion
    }
}
