using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IBillService
    {
        ObservableCollection<Bill> GetBills();
        bool CreateBill(Bill bill);
        bool UpdateBill(Bill bill);
        bool DeleteBill(int billId);
        ObservableCollection<Bill> SearchBill(int? tableId, string? customerName, DateTime? startTime, DateTime? endTime);
    }
}
