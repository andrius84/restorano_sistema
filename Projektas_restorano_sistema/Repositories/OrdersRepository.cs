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
        public void WriteOrderToJsonFile(Order order)
        {
            // Serialize the order object to JSON format
            string jsonString = JsonSerializer.Serialize(order, new JsonSerializerOptions
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

            // Deserialize the JSON string to a list of Order objects
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString);

            return orders;
        }
    }
}