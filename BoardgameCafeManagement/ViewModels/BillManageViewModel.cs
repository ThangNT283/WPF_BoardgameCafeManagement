using BusinessObjects;
using System.Collections.ObjectModel;

namespace BoardgameCafeManagement.ViewModels
{
    class BillManageViewModel : ViewModelBase
    {
        private readonly BoardgameCafeContext _context;

        private ObservableCollection<Bill> _bills;
        public ObservableCollection<Bill> Bills
        {
            get { return _bills; }
            set { _bills = value; OnPropertyChanged(nameof(Bills)); }
        }

        public BillManageViewModel()
        {
            _context = new BoardgameCafeContext();

            Bills = new ObservableCollection<Bill>(_context.Bills);
        }
    }
}
