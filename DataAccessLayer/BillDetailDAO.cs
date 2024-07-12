using BusinessObjects;
using System.Windows;

namespace DataAccessLayer
{
    public class BillDetailDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public BillDetailDAO()
        {
        }

        public static List<BillDetail> GetDetailByBillId(int billId)
        {
            return _context.BillDetails.Where(d => d.BillId == billId).ToList();
        }

        public static List<BillDetail> GetDetailByDrinkId(int drinkId)
        {
            return _context.BillDetails.Where(d => d.BillId == drinkId).ToList();
        }

        public static bool CreateBillDetail(BillDetail detail)
        {
            try
            {
                _context.BillDetails.Add(detail);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UpdateBillDetail(BillDetail detail)
        {
            try
            {
                BillDetail? detailToUpdate = _context.BillDetails.Find(detail.Id);
                if (detailToUpdate == null)
                {
                    throw new Exception("Bill detail ID " + detail.Id + " not found!");
                }
                _context.Entry(detailToUpdate).CurrentValues.SetValues(detail);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool DeleteBillDetail(int billDetailId)
        {
            try
            {
                Bill? detailToDelete = _context.Bills.Find(billDetailId);
                if (detailToDelete == null)
                {
                    throw new Exception("Bill detail ID " + billDetailId + " not found!");
                }

                _context.Bills.Remove(detailToDelete);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
