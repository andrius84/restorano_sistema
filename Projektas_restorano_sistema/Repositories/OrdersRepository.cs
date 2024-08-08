using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RestoranoSistema.Models;

namespace RestoranoSistema.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly Order _order;
        private readonly string _filePath;
        public OrdersRepository(Order order, string filePath)
        {
            _order = order;
            _filePath = filePath;
        }
        public void AddOrderToJsonFile(Order order)
        {
            string jsonString = File.ReadAllText(_filePath);
            List<Order> orders = new List<Order>();
            if (!string.IsNullOrEmpty(jsonString))
            {
                orders = JsonSerializer.Deserialize<List<Order>>(jsonString);
            }
            orders.Add(order);
            jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_filePath, jsonString);
        }
        public List<Order> ReadOrdersFromJsonFile()
        {
            string jsonString = File.ReadAllText(_filePath);
            if (string.IsNullOrEmpty(jsonString))
            {
                return new List<Order>();
            }
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString);
            return orders;
        }
        public void UpdateOrderToJsonFile(Order order)
        {
            string jsonString = File.ReadAllText(_filePath);
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString) ?? new List<Order>();
            Order orderToUpdate = orders.FirstOrDefault(x => x.Id == order.Id);
            if (orderToUpdate != null)
            {
                orderToUpdate.Dishes = order.Dishes;
                orderToUpdate.Beverages = order.Beverages;
                orderToUpdate.TotalPrice = order.TotalPrice;
                jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(_filePath, jsonString);
            }
        }
        public void DeleteOrderFromJsonFile(Order order)
        {
            string jsonString = File.ReadAllText(_filePath);
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString) ?? new List<Order>();
            Order? orderToDelete = orders.FirstOrDefault(x => x.Id == order.Id);
            if (orderToDelete != null)
            {
                orders.Remove(orderToDelete);
                jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(_filePath, jsonString);
            }
        }
    }
}