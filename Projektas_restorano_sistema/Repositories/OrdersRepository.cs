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
        public OrdersRepository(string filePath)
        {
            //_order = order;
            _filePath = filePath;
        }
        public void AddOrderToJsonFile(Order order)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Įvyko klaida pridedant užsakymą į JSON failą: {ex.Message}");
            }
        }
        public List<Order> ReadOrdersFromJsonFile()
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                if (string.IsNullOrEmpty(jsonString))
                {
                    return new List<Order>();
                }
                List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString);
                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Įvyko klaida nuskaitant užsakymus iš JSON failo: {ex.Message}");
                return new List<Order>();
            }
        }
        public void UpdateOrderToJsonFile(Order order)
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString) ?? new List<Order>();
                Order orderToUpdate = orders.FirstOrDefault(x => x.Id == order.Id);
                if (orderToUpdate != null)
                {
                    orderToUpdate.Dishes = order.Dishes;
                    orderToUpdate.Beverages = order.Beverages;
                    jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    File.WriteAllText(_filePath, jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Įvyko klaida atnaujinant užsakymą JSON faile: {ex.Message}");
            }
        }
        public void DeleteOrderFromJsonFile(Order order)
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString) ?? new List<Order>();
                Order orderToDelete = orders.FirstOrDefault(x => x.Id == order.Id);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Įvyko klaida ištrinant užsakymą iš JSON failo: {ex.Message}");
            }
        }
    }
}