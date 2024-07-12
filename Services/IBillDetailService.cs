using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IBillDetailService
    {
        ObservableCollection<BillDetail> GetDetailByBillId(int billId);
        ObservableCollection<BillDetail> GetDetailByDrinkId(int drinkId);
        bool CreateBillDetail(BillDetail detail);
        bool UpdateBillDetail(BillDetail detail);
        bool DeleteBillDetail(int billDetailId);
    }
}
