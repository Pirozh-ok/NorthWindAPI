using Microsoft.EntityFrameworkCore;
using NorthWindAPI.Models;
using NorthWindAPI.Services.Interfaces;

namespace NorthWindAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly NorthwindContext _context;
        public OrderService(NorthwindContext context)
        {
            _context = context;
        }
        public void CreateOrder(Order order)
        {
           if(order is null)
            {
                throw new ArgumentNullException("Передано пустое значение");
            }

           _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrderById(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public IEnumerable<Order> GetAllOrdersWithDetails()
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            var order =  _context.Orders
                .SingleOrDefault(o => o.OrderId == id);

            if(order is null)
            {
                throw new Exception("Заказ не найден!");
            }

            return order;
        }

        public IEnumerable<Order> GetOrdersByCustomer(string customerId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.CustomerId == customerId);
        }

        public void UpdateOrder(Order order)
        {
            var orderToUpdate = GetOrderById(order.OrderId);

            orderToUpdate.OrderDate = order.OrderDate;
            orderToUpdate.DeliveryDate = order.DeliveryDate;
            orderToUpdate.OrderDetails = order.OrderDetails;
            orderToUpdate.Customer = order.Customer;
            _context.SaveChanges();
        }
    }
}
