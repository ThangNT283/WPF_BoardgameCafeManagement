using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class BillRepository : IBillRepository
    {
        public List<Bill> GetBills() => BillDAO.GetBills();
        public bool CreateBill(Bill bill) => BillDAO.CreateBill(bill);
        public bool UpdateBill(Bill bill) => BillDAO.UpdateBill(bill);
        public bool DeleteBill(int billId) => BillDAO.DeleteBill(billId);
        public List<Bill> SearchBill(int? tableId, DateTime? startTime, DateTime? endTime)
            => BillDAO.SearchBill(tableId, startTime, endTime);
    }
}
