using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Repositories;

namespace RestoranoSistema.Models
{
    public class Beverage : MenuItem
    {
        private IItemsRepository _itemsRepository;
        public Beverage(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }
        public Beverage() { }
        public List<Beverage> Beverages()
        {
            return _itemsRepository.GetBeverageList().OrderByDescending(x => x.Name).ToList();
        }
    }
}
