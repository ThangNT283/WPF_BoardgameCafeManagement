using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class BillDetailRepository : IBillDetailRepository
    {
        public List<BillDetail> GetDetailByBillId(int billId) => BillDetailDAO.GetDetailByBillId(billId);
        public List<BillDetail> GetDetailByDrinkId(int drinkId) => BillDetailDAO.GetDetailByDrinkId(drinkId);
        public bool CreateBillDetail(BillDetail detail) => BillDetailDAO.CreateBillDetail(detail);
        public bool UpdateBillDetail(BillDetail detail) => BillDetailDAO.UpdateBillDetail(detail);
        public bool DeleteBillDetail(int billDetailId) => BillDetailDAO.DeleteBillDetail(billDetailId);
    }
}
