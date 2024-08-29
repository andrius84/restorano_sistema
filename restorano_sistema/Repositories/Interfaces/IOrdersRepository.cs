using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories.Interfaces;

namespace RestoranoSistema.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        void AddOrder(Order order);
        List<Order> GetOrders();
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
