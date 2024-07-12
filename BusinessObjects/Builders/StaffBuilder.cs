namespace BusinessObjects.Builders
{
    public class StaffBuilder : IStaffBuilder
    {
        private Staff _staff;

        public StaffBuilder()
        {
            _staff = new Staff();
        }

        public Staff Build() => _staff;

        public IStaffBuilder SetId(int id)
        {
            _staff.Id = id;
            return this;
        }

        public IStaffBuilder SetUsername(string username)
        {
            _staff.Username = username;
            return this;
        }

        public IStaffBuilder SetPassword(string password)
        {
            _staff.Password = password;
            return this;
        }

        public IStaffBuilder SetFullname(string fullname)
        {
            _staff.Fullname = fullname;
            return this;
        }

        public IStaffBuilder SetPhone(string phone)
        {
            _staff.Phone = phone;
            return this;
        }

        public IStaffBuilder SetEmail(string email)
        {
            _staff.Email = email;
            return this;
        }

        public IStaffBuilder SetStatus(bool status)
        {
            _staff.Status = status;
            return this;
        }
    }
}
