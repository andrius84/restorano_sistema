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
        public OrdersRepository(Order order)
        {
            _order = order;

        }
        public void WriteOrderToJsonFile(Order order, string filePath)
        {
            // Serialize the order object to JSON format
            string jsonString = JsonSerializer.Serialize(order, new JsonSerializerOptions
            {
                WriteIndented = true // to make the JSON output pretty-printed
            });

            // Write the JSON string to a file
            File.WriteAllText(filePath, jsonString);
        }
        public List<Dish> LoadDishesListFromCsv()
        {
            var dishes = new List<Dish>();
            var lines = File.ReadAllLines("../../../Repositories/dishes.csv");
            foreach (var line in lines)
            {
                var values = line.Split(',');
                var dish = new Dish
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    Price = double.Parse(values[2])
                };
                dishes.Add(dish);
            }
            return dishes;
        }
        }
    }
}