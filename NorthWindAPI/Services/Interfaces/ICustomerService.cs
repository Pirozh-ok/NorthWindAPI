using NorthWindAPI.DTOs;
using NorthWindAPI.Models;
using NorthWindAPI.Paginations;

namespace NorthWindAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers(CustomerPagination filter);
        Customer GetCustomerById(string id);
        Customer GetCustomerByName(string name);
        CustomerDTO GetGeneralInfoCustomerById(string id);
        CustomerDTO GetGeneralInfoCustomerByName(string name);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer); 
        void DeleteCustomer(string id);
    }
}
