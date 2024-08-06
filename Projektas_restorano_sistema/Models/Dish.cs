using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Repositories;


namespace RestoranoSistema.Models
{
    public class Dish : MenuItem
    {
        private IItemsRepository _itemsRepository;
        public Dish(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }
        public Dish() { }
        public List<Dish> Dishes()
        {
            return _itemsRepository.GetFoodList().OrderByDescending(x => x.Name).ToList();
        }
    }
}
