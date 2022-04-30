using NorthWindAPI.DTOs;
using NorthWindAPI.Models;
using NorthWindAPI.Paginations;
using NorthWindAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customer is null)
            {
                throw new Exception("Покупатель не найден!");
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetAllCustomers(CustomerPagination filter)
        {
            return _context.Customers
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            //.Include(c => c.Orders)
                            .ToList();
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

        public IEnumerable<Order> GetOrdersByCustomerId(string id)
        {
            return _context.Orders.Where(c => c.CustomerId == id);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException("Передано пустое значение");
            }

            var updateCustomer = _context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);

            if(updateCustomer is null)
            {
                throw new Exception("Не удалось найти пользователя");
            }

            updateCustomer.CompanyName = customer.CompanyName;
            updateCustomer.ContactName = customer.ContactName;
            updateCustomer.ContactTitle = customer.ContactTitle;
            updateCustomer.Address = customer.Address;
            updateCustomer.City = customer.City;
            updateCustomer.Country = customer.Country;
            updateCustomer.Phone = customer.Phone;
            updateCustomer.Fax = customer.Fax;

            _context.SaveChanges();
        }
    }
}
