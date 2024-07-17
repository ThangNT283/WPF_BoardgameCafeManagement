using BoardgameCafeManagement.Commands;
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
        public RelayCommand GameManage { get; }
        public RelayCommand TableManage { get; }

        public StaffViewModel()
        {
            BillManage = new RelayCommand(OpenBillManage);
            GameManage = new RelayCommand(OpenGameManage);
            TableManage = new RelayCommand(OpenTableManage);
        }

        public void OpenBillManage()
        {
            MainContent = new StaffBillView();
        }
        public void OpenGameManage()
        {
            MainContent = new StaffGameView();
        }
        public void OpenTableManage()
        {
            MainContent = new StaffTableView();
        }
    }
}
