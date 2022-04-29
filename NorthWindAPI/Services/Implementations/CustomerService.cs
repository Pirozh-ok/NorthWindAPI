using NorthWindAPI.Models;
using NorthWindAPI.Services.Interfaces;

namespace NorthWindAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        public void CreateCustomer(Customer customer)
        {
            using var context = new NorthwindContext();

        }

        public void DeleteCustomer(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            using var context = new NorthwindContext();
            return context.Customers.ToList();
        }

        public Customer GetCustomerById(string id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByName(string name)
        {
            throw new NotImplementedException();
        }

        public Customer GetGeneralInfoCustomerById(string id)
        {
            throw new NotImplementedException();
        }

        public Customer GetGeneralInfoCustomerByName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
