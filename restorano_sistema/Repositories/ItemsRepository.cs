using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories.Interfaces;

namespace RestoranoSistema.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly RestoranasDbContext _context;

        public ItemsRepository(RestoranasDbContext context)
        {
            _context = context;
        }

        public List<Dish> GetFoodList()
        {
            try
            {
                return _context.Dishes.ToList(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving food list: {ex.Message}");
                return new List<Dish>();
            }
        }

        public List<Beverage> GetBeverageList()
        {
            try
            {
                return _context.Beverages.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving beverage list: {ex.Message}");
                return new List<Beverage>();
            }
        }
    }
}
