﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;

namespace RestoranoSistema.Repositories.Interfaces
{
    public interface IItemsRepository
    {
        List<Dish> GetFoodList();
        List<Beverage> GetBeverageList();
    }
}
