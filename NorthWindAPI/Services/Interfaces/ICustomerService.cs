using NorthWindAPI.DTOs;
using NorthWindAPI.Models;
using NorthWindAPI.Paginations;

namespace NorthWindAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetAllCustomers();
        IEnumerable<Customer> GetAllCustomersWithOrders();
        Customer GetCustomerById(string id);
        Customer GetCustomerByName(string name);
        Customer GetCustomerByIdWithOrders(string id);
        Customer GetCustomerByNameWithOrders(string name);
        CustomerDTO GetGeneralInfoCustomerById(string id);
        CustomerDTO GetGeneralInfoCustomerByName(string name);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer); 
        void DeleteCustomer(string id);
        IQueryable<Order> GetOrdersByCustomerId(string id);
    }
}
