using BusinessObjects;

namespace Repositories
{
    public interface IBillDetailRepository
    {
        List<BillDetail> GetDetailByBillId(int billId);
        List<BillDetail> GetDetailByDrinkId(int drinkId);
        bool CreateBillDetail(BillDetail detail);
        bool UpdateBillDetail(BillDetail detail);
        bool DeleteBillDetail(int billDetailId);
    }
}
