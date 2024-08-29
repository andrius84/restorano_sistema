using RestoranoSistema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Services.Interfaces;
using RestoranoSistema.Repositories.Interfaces;

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
