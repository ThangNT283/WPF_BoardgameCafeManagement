using BusinessObjects;
using System.Globalization;
using System.Text;
using System.Windows;

namespace DataAccessLayer
{
    public class StaffDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public StaffDAO()
        {
        }

        public static List<Staff> GetStaffs()
        {
            return _context.Staff.ToList();
        }

        public static bool CreateStaff(Staff staff)
        {
            try
            {
                _context.Staff.Add(staff);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UpdateStaff(Staff staff)
        {
            try
            {
                Staff? staffToUpdate = _context.Staff.Find(staff.Id);
                if (staffToUpdate == null)
                {
                    throw new Exception("Staff ID " + staff.Id + " not found!");
                }
                _context.Entry(staffToUpdate).CurrentValues.SetValues(staff);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static Staff? GetByPhone(string phone)
        {
            return _context.Staff.Where(s => s.Phone == phone).FirstOrDefault();
        }

        public static Staff? GetByEmail(string email)
        {
            return _context.Staff.Where(s => s.Email == email).FirstOrDefault();
        }

        public static List<Staff> SearchStaff(string input)
        {
            List<Staff> results = new List<Staff>();

            foreach (Staff s in _context.Staff)
            {
                if (RemoveDiacritics(s.Fullname.ToLower()).Contains(input.ToLower()) ||
                    RemoveDiacritics(s.Username.ToLower()).Contains(input.ToLower()) ||
                    (s.Email != null && s.Email.ToLower().Contains(input.ToLower())))
                {
                    results.Add(s);
                }
            }

            return results;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
