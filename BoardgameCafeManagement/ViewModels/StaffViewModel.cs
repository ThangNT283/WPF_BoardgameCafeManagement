﻿using BoardgameCafeManagement.Commands;
using BoardgameCafeManagement.Views;

namespace BoardgameCafeManagement.ViewModels
{
    class StaffViewModel : ViewModelBase
    {
        private object _mainContent;
        public object MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; OnPropertyChanged(nameof(MainContent)); }
        }

        public RelayCommand BillManage { get; }
        //public RelayCommand DrinkManage { get; }
        //public RelayCommand StaffManage { get; }
        public RelayCommand TableManage { get; }

        public StaffViewModel()
        {
            BillManage = new RelayCommand(OpenBillManage);
            //DrinkManage = new RelayCommand(OpenDrinkManage);
            //StaffManage = new RelayCommand(OpenStaffManage);
            TableManage = new RelayCommand(OpenTableManage);
        }

        public void OpenBillManage()
        {
            MainContent = new StaffBillView();
        }

        //public void OpenDrinkManage()
        //{
        //    MainContent = new DrinkManageView();
        //}

        //public void OpenStaffManage()
        //{
        //    MainContent = new StaffManageView();
        //}

        public void OpenTableManage()
        {
            MainContent = new StaffTableView();
        }
    }
}
