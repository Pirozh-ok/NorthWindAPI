using NorthWindAPI.Models;

namespace NorthWindAPI.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        IEnumerable<Order> GetOrderByCustomer(string customerId);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
    }
}
