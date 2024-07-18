using BusinessObjects;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public class BillDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public BillDAO()
        {
        }

        public static List<Bill> GetBills()
        {
            return _context.Bills.OrderByDescending(b => b.PaidAt).ToList();
        }

        public static bool CreateBill(Bill bill)
        {
            try
            {
                _context.Bills.Add(bill);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UpdateBill(Bill bill)
        {
            try
            {
                Bill? billToUpdate = _context.Bills.Find(bill.Id);
                if (billToUpdate == null)
                {
                    throw new Exception("Bill ID " + bill.Id + " not found!");
                }
                _context.Entry(billToUpdate).CurrentValues.SetValues(bill);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool DeleteBill(int billId)
        {
            try
            {
                Bill? billToDelete = _context.Bills.Find(billId);
                if (billToDelete == null)
                {
                    throw new Exception("Bill ID " + billId + " not found!");
                }

                _context.Bills.Remove(billToDelete);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static List<Bill> SearchBill(int? tableId, string? customerName, DateTime? startTime, DateTime? endTime)
        {
            return _context.Bills
                .Where(b => !tableId.HasValue || b.TableId == tableId.Value)
                .Where(b => string.IsNullOrEmpty(customerName) || b.CustomerName.Contains(customerName))
                .Where(b => !startTime.HasValue || b.PaidAt >= startTime.Value)
                .Where(b => !endTime.HasValue || b.PaidAt <= endTime.Value)
                .ToList();
        }
    }
}
