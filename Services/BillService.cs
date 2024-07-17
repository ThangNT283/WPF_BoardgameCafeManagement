using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepo;

        public BillService()
        {
            _billRepo = new BillRepository();
        }

        public ObservableCollection<Bill> GetBills()
        {
            return new ObservableCollection<Bill>(_billRepo.GetBills());
        }
        public bool CreateBill(Bill bill) => _billRepo.CreateBill(bill);
        public bool UpdateBill(Bill bill) => _billRepo.UpdateBill(bill);
        public bool DeleteBill(int billId) => _billRepo.DeleteBill(billId);
        public ObservableCollection<Bill> SearchBill(int? tableId, DateTime? startTime, DateTime? endTime)
        {
            return new ObservableCollection<Bill>(_billRepo.SearchBill(tableId, startTime, endTime));
        }
    }
}
