using NorthWindAPI.Models;

namespace NorthWindAPI.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrdersWithDetails();
        Order GetOrderById(int id);
        IEnumerable<Order> GetOrdersByCustomer(string customerId);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
    }
}
