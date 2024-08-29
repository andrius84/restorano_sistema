using RestoranoSistema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Services.Interfaces
{
    public interface IItemsService
    {
        List<Beverage> Beverages();
        List<Dish> Dishes();
    }
}
