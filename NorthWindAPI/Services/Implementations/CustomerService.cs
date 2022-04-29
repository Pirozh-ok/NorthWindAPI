using NorthWindAPI.DTOs;
using NorthWindAPI.Models;
using NorthWindAPI.Services.Interfaces;

namespace NorthWindAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly NorthwindContext _context;
        public CustomerService(NorthwindContext context)
        {
            _context = context;
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException("Передано пустое значение");
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(string id)
        {
            using var context = new NorthwindContext();
            return context.Customers.SingleOrDefault(c => c.CustomerId == id);
        }

        public Customer GetCustomerByName(string name)
        {
            using var context = new NorthwindContext();
            return context.Customers.SingleOrDefault(c => c.ContactName == name);
        }

        public CustomerDTO GetGeneralInfoCustomerById(string id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerId == id);
            return customer is not null ?
                new CustomerDTO { CompanyName = customer.CompanyName, Phone = customer.Phone } :
                null;
        }

        public CustomerDTO GetGeneralInfoCustomerByName(string name)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.CompanyName == name);
            return customer is not null ?
                new CustomerDTO { CompanyName = customer.CompanyName, Phone = customer.Phone } :
                null;
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
