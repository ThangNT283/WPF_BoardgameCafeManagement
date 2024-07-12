using BusinessObjects;

namespace Repositories
{
    public interface IStaffRepository
    {
        List<Staff> GetStaffs();
        bool CreateStaff(Staff staff);
        bool UpdateStaff(Staff staff);
        Staff? GetByPhone(string phone);
        Staff? GetByEmail(string email);
        List<Staff> SearchStaff(string input);
    }
}
