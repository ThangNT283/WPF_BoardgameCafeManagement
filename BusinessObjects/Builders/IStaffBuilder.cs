namespace BusinessObjects.Builders
{
    public interface IStaffBuilder
    {
        IStaffBuilder SetId(int id);
        IStaffBuilder SetUsername(string username);
        IStaffBuilder SetPassword(string password);
        IStaffBuilder SetFullname(string fullname);
        IStaffBuilder SetEmail(string email);
        IStaffBuilder SetPhone(string phone);
        IStaffBuilder SetStatus(bool status);
        Staff Build();
    }
}
