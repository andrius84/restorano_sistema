using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;
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
            _orderRepository.AddOrderToJsonFile(order);
        }
        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrderToJsonFile(order);
        }
        public void DeleteOrder(Guid orderId)
        {
            var order = _orderRepository.ReadOrdersFromJsonFile().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            _orderRepository.DeleteOrderFromJsonFile(order);
        }
        public void AddDishToOrder(Guid orderId, Dish dish)
        {
            var order = _orderRepository.ReadOrdersFromJsonFile().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Dishes ??= new List<Dish>();
            order.Dishes.Add(dish);
            _orderRepository.UpdateOrderToJsonFile(order);
        }
        public void AddBeverageToOrder(Guid orderId, Beverage beverage)
        {
            var order = _orderRepository.ReadOrdersFromJsonFile().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Beverages ??= new List<Beverage>();
            order.Beverages.Add(beverage);
            _orderRepository.UpdateOrderToJsonFile(order);
        }
        public void DeleteDishFromOrder(Guid orderId, Dish dish)
        {
            var order = _orderRepository.ReadOrdersFromJsonFile().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Dishes.RemoveAll(d => d.Id == dish.Id);
            _orderRepository.UpdateOrderToJsonFile(order);
        }
        public void DeleteBeverageFromOrder(Guid orderId, Beverage beverage)
        {
            var order = _orderRepository.ReadOrdersFromJsonFile().FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.Beverages.RemoveAll(b => b.Id == beverage.Id);
            _orderRepository.UpdateOrderToJsonFile(order);
        }
        public List<Order> GetOrders()
        {
            return _orderRepository.ReadOrdersFromJsonFile();
        }
        public Order GetOrderByTableId(int tableId)
        {
            return _orderRepository.ReadOrdersFromJsonFile().FirstOrDefault(x => x.Table.Id == tableId);
        }
        public Order GetOrderById(Guid orderId)
        {
            return _orderRepository.ReadOrdersFromJsonFile().FirstOrDefault(x => x.Id == orderId);
        }
    }
}
