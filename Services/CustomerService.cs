using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService()
        {
            _customerRepo = new CustomerRepository();
        }

        public ObservableCollection<Customer> GetCustomers()
        {
            return new ObservableCollection<Customer>(_customerRepo.GetCustomers());
        }
        public bool CreateCustomer(Customer customer) => _customerRepo.CreateCustomer(customer);
        public bool UpdateCustomer(Customer customer) => _customerRepo.UpdateCustomer(customer);
        public Customer? GetByPhone(string phone) => _customerRepo.GetByPhone(phone);
        public Customer? GetByEmail(string email) => _customerRepo.GetByEmail(email);
    }
}
