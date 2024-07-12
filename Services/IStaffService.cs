using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IStaffService
    {
        ObservableCollection<Staff> GetStaffs();
        bool CreateStaff(Staff staff);
        bool UpdateStaff(Staff staff);
        Staff? GetByPhone(string phone);
        Staff? GetByEmail(string email);
        ObservableCollection<Staff> SearchStaff(string input);
    }
}
