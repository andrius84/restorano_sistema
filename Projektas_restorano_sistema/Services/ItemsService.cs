using RestoranoSistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Repositories;
using RestoranoSistema.Services.Interfaces;

namespace RestoranoSistema.Services
{
    public class ItemsService(IItemsRepository itemsRepository) : IItemsService
    {
        private readonly IItemsRepository _itemsRepository = itemsRepository;
        public List<Beverage> Beverages()
        {
            return _itemsRepository.GetBeverageList().OrderByDescending(x => x.Name).ToList();
        }
        public List<Dish> Dishes()
        {
            return _itemsRepository.GetFoodList().OrderByDescending(x => x.Name).ToList();
        }
    }
}
