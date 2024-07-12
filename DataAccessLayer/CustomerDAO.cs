using BusinessObjects;
using System.Windows;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        private static readonly BoardgameCafeContext _context = new BoardgameCafeContext();

        public CustomerDAO()
        {
        }

        public static List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public static bool CreateCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool UpdateCustomer(Customer customer)
        {
            try
            {
                Customer? customerToUpdate = _context.Customers.Find(customer.Id);
                if (customerToUpdate == null)
                {
                    throw new Exception("Customer ID " + customer.Id + " not found!");
                }
                _context.Entry(customerToUpdate).CurrentValues.SetValues(customer);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static Customer? GetByPhone(string phone)
        {
            return _context.Customers.Where(c => c.Phone == phone).FirstOrDefault();
        }

        public static Customer? GetByEmail(string email)
        {
            return _context.Customers.Where(s => s.Email == email).FirstOrDefault();
        }
    }
}
