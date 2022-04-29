using NorthWindAPI.Models;

namespace NorthWindAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(string id);
        Customer GetCustomerByName(string name);
        Customer GetGeneralInfoCustomerById(string id);
        Customer GetGeneralInfoCustomerByName(string name);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer); 
        void DeleteCustomer(string id);
    }
}
