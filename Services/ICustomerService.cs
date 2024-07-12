using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface ICustomerService
    {
        ObservableCollection<Customer> GetCustomers();
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        Customer? GetByPhone(string phone);
        Customer? GetByEmail(string email);
    }
}
