using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepo;

        public StaffService()
        {
            _staffRepo = new StaffRepository();
        }

        public ObservableCollection<Staff> GetStaffs()
        {
            return new ObservableCollection<Staff>(_staffRepo.GetStaffs());
        }
        public bool CreateStaff(Staff staff) => _staffRepo.CreateStaff(staff);
        public bool UpdateStaff(Staff staff) => _staffRepo.UpdateStaff(staff);
        public Staff? GetByPhone(string phone) => _staffRepo.GetByPhone(phone);
        public Staff? GetByEmail(string email) => _staffRepo.GetByEmail(email);
        public ObservableCollection<Staff> SearchStaff(string input)
        {
            return new ObservableCollection<Staff>(_staffRepo.SearchStaff(input));
        }
    }
}
