using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class StaffRepository : IStaffRepository
    {
        public List<Staff> GetStaffs() => StaffDAO.GetStaffs();
        public bool CreateStaff(Staff staff) => StaffDAO.CreateStaff(staff);
        public bool UpdateStaff(Staff staff) => StaffDAO.UpdateStaff(staff);
        public Staff? GetByPhone(string phone) => StaffDAO.GetByPhone(phone);
        public Staff? GetByEmail(string email) => StaffDAO.GetByEmail(email);
        public List<Staff> SearchStaff(string input) => StaffDAO.SearchStaff(input);
    }
}
