using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories.Interfaces;
using RestoranoSistema.Services.Interfaces;

namespace RestoranoSistema.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _orderRepository;
        public OrdersService(IOrdersRepository ordersRepository)
        {
            _orderRepository = ordersRepository;
        }
        public void CreateOrder(Table table)
        {
            var order = new Order();
            order.Id = Guid.NewGuid();
            order.OrderTime = DateTime.Now;
            order.Table = table;
            _orderRepository.AddOrder(order);
        }
        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }
        public void DeleteOrder(Guid orderId)
        {
            var order = _orderRepository.GetOrders().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            _orderRepository.DeleteOrder(order);
        }
        public void AddDishToOrder(Guid orderId, Dish dish)
        {
            var order = _orderRepository.GetOrders().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Dishes ??= new List<Dish>();
            order.Dishes.Add(dish);
            _orderRepository.UpdateOrder(order);
        }
        public void AddBeverageToOrder(Guid orderId, Beverage beverage)
        {
            var order = _orderRepository.GetOrders().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Beverages ??= new List<Beverage>();
            order.Beverages.Add(beverage);
            _orderRepository.UpdateOrder(order);
        }
        public void DeleteDishFromOrder(Guid orderId, Dish dish)
        {
            var order = _orderRepository.GetOrders().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Dishes.RemoveAll(d => d.Id == dish.Id);
            _orderRepository.UpdateOrder(order);
        }
        public void DeleteBeverageFromOrder(Guid orderId, Beverage beverage)
        {
            var order = _orderRepository.GetOrders().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Beverages.RemoveAll(b => b.Id == beverage.Id);
            _orderRepository.UpdateOrder(order);
        }
        public List<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }
        public Order GetOrderByTableId(int tableId)
        {
            return _orderRepository.GetOrders().FirstOrDefault(x => x.Table.Id == tableId);
        }
        public Order GetOrderById(Guid orderId)
        {
            return _orderRepository.GetOrders().FirstOrDefault(x => x.Id == orderId);
        }
    }
}
