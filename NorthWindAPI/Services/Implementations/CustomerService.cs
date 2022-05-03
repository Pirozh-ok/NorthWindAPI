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
            var customer = _context.Customers
                .SingleOrDefault(c => c.CustomerId == id);

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

        public IEnumerable<Customer> GetAllCustomersWithOrders()
        {
            return _context.Customers
                            .Include(c => c.Orders)
                            .ToList();
        }

        public Customer GetCustomerById(string id)
        {
            var customer = _context.Customers
                .SingleOrDefault(c => c.CustomerId == id);

            if (customer is null)
                throw new Exception("Покупатель не найден");

            return customer;
        }

        public Customer GetCustomerByIdWithOrders(string id)
        {
            var customer = _context.Customers
                .Include(c => c.Orders)
                .SingleOrDefault(c => c.CustomerId == id);

            if (customer is null)
                throw new Exception("Покупатель не найден");

            return customer;
        }

        public Customer GetCustomerByName(string name)
        {
            var customer = _context.Customers
                .SingleOrDefault(c => c.ContactName == name);

            if (customer is null)
                throw new Exception("Покупатель не найден");

            return customer; 
        }

        public Customer GetCustomerByNameWithOrders(string name)
        {
            var customer = _context.Customers
                .Include(c => c.Orders)
                .SingleOrDefault(c => c.ContactName == name);

            if (customer is null)
                throw new Exception("Покупатель не найден");

            return customer;
        }

        public CustomerDTO GetGeneralInfoCustomerById(string id)
        {
            var customer = _context.Customers
                .SingleOrDefault(c => c.CustomerId == id);

            return customer is not null ?
                new CustomerDTO { CompanyName = customer.CompanyName, Phone = customer.Phone } :
                null;
        }

        public CustomerDTO GetGeneralInfoCustomerByName(string name)
        {
            var customer = _context.Customers
                .SingleOrDefault(c => c.CompanyName == name);

            return customer is not null ?
                new CustomerDTO { CompanyName = customer.CompanyName, Phone = customer.Phone } :
                null;
        }

        public IEnumerable<Order> GetOrdersByCustomerId(string id)
        {
            return _context.Orders
                .Where(c => c.CustomerId == id);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException("Передано пустое значение");
            }

            var updateToCustomer = _context.Customers
                .SingleOrDefault(c => c.CustomerId == customer.CustomerId);

            if(updateToCustomer is null)
            {
                throw new Exception("Пользователь не найден");
            }

            updateToCustomer.CompanyName = customer.CompanyName;
            updateToCustomer.ContactName = customer.ContactName;
            updateToCustomer.ContactTitle = customer.ContactTitle;
            updateToCustomer.Address = customer.Address;
            updateToCustomer.City = customer.City;
            updateToCustomer.Country = customer.Country;
            updateToCustomer.Phone = customer.Phone;
            updateToCustomer.Fax = customer.Fax;

            _context.SaveChanges();
        }
    }
}
