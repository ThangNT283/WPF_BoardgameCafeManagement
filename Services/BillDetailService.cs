using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class BillDetailService : IBillDetailService
    {
        private readonly IBillDetailRepository _billDetailRepo;

        public BillDetailService()
        {
            _billDetailRepo = new BillDetailRepository();
        }

        public ObservableCollection<BillDetail> GetDetailByBillId(int billId)
        {
            return new ObservableCollection<BillDetail>(_billDetailRepo.GetDetailByBillId(billId));
        }
        public ObservableCollection<BillDetail> GetDetailByDrinkId(int drinkId)
        {
            return new ObservableCollection<BillDetail>(_billDetailRepo.GetDetailByDrinkId(drinkId));
        }
        public bool CreateBillDetail(BillDetail detail) => _billDetailRepo.CreateBillDetail(detail);
        public bool UpdateBillDetail(BillDetail detail) => _billDetailRepo.UpdateBillDetail(detail);
        public bool DeleteBillDetail(int billDetailId) => _billDetailRepo.DeleteBillDetail(billDetailId);
    }
}
