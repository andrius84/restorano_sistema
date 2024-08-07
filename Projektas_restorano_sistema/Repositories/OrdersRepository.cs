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
            // Read the JSON string from a file
            string jsonString = File.ReadAllText(_filePath);

            // Deserialize the JSON string to a list of Order objects
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString);

            // Add the new order to the list
            orders.Add(order);

            // Serialize the list of Order objects to JSON format
            jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions
            {
                WriteIndented = true // to make the JSON output pretty-printed
            });

            // Write the JSON string to a file
            File.WriteAllText(_filePath, jsonString);
        }
        public List<Order> ReadOrdersFromJsonFile()
        {
            // Read the JSON string from a file
            string jsonString = File.ReadAllText(_filePath);

            if (string.IsNullOrEmpty(jsonString))
            {
                return new List<Order>();   
            }
            // Deserialize the JSON string to a list of Order objects
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString);

            return orders;
        }
        public void UpdateOrderToJsonFile(Order order)
        {
            // Read the JSON string from a file
            string jsonString = File.ReadAllText(_filePath);

            // Deserialize the JSON string to a list of Order objects
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString);

            // Find the order to update in the list
            Order orderToUpdate = orders.FirstOrDefault(x => x.Id == order.Id);

            // Update the order in the list
            orderToUpdate = order;

            // Serialize the list of Order objects to JSON format
            jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions
            {
                WriteIndented = true // to make the JSON output pretty-printed
            });

            // Write the JSON string to a file
            File.WriteAllText(_filePath, jsonString);
        }
        public void DeleteOrderFromJsonFile(Order order)
        {
            // Read the JSON string from a file
            string jsonString = File.ReadAllText(_filePath);

            // Deserialize the JSON string to a list of Order objects
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString);

            // Find the order to delete in the list
            Order orderToDelete = orders.FirstOrDefault(x => x.Id == order.Id);

            // Remove the order from the list
            orders.Remove(orderToDelete);

            // Serialize the list of Order objects to JSON format
            jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions
            {
                WriteIndented = true // to make the JSON output pretty-printed
            });

            // Write the JSON string to a file
            File.WriteAllText(_filePath, jsonString);

        }
    }
}