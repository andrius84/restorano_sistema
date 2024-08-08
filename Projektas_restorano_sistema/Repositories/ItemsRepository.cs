using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;

namespace RestoranoSistema.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly string _foodfilePath;
        private readonly string _drinksfilePath;
        public ItemsRepository(string foodfilePath, string drinksfilePath)
        {
            _foodfilePath = foodfilePath;
            _drinksfilePath = drinksfilePath;
        }
        public List<Dish> GetFoodList()
        {
            List<Dish> items = new List<Dish>();
            string[] lines = File.ReadAllLines(_foodfilePath);
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                Dish item = new Dish();
                item.Id = int.TryParse(values[0] ?? "0", out int id) ? id : 0;
                item.Category = values[1];
                item.Name = values[2];
                item.Price = decimal.TryParse(values[3]?.Replace(" ", "") ?? "0", out decimal price) ? price : 0;
                items.Add(item);
            }
            return items;
        }
        public List<Beverage> GetBeverageList()
        {
            List<Beverage> items = new List<Beverage>();
            string[] lines = File.ReadAllLines(_drinksfilePath);
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                Beverage item = new Beverage();
                item.Id = int.TryParse(values[0] ?? "0", out int id) ? id : 0;
                item.Category = values[1];
                item.Name = values[2];
                item.Price = decimal.TryParse(values[3] ?? "0", out decimal price) ? price : 0;
                items.Add(item);
            }
            return items;
        }
    }
}
