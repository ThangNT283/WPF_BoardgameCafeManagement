using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetCustomers() => CustomerDAO.GetCustomers();
        public bool CreateCustomer(Customer customer) => CustomerDAO.CreateCustomer(customer);
        public bool UpdateCustomer(Customer customer) => CustomerDAO.UpdateCustomer(customer);
        public Customer? GetByPhone(string phone) => CustomerDAO.GetByPhone(phone);
        public Customer? GetByEmail(string email) => CustomerDAO.GetByEmail(email);
    }
}
