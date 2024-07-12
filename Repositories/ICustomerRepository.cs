using BusinessObjects;

namespace Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        Customer? GetByPhone(string phone);
        Customer? GetByEmail(string email);
    }
}
