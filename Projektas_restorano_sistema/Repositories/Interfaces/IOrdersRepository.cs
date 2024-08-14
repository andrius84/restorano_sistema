using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;

namespace RestoranoSistema.Repositories
{
    public interface IOrdersRepository
    {
        void AddOrderToJsonFile(Order order);
        List<Order> ReadOrdersFromJsonFile();
        void UpdateOrderToJsonFile(Order order);
        void DeleteOrderFromJsonFile(Order order);
    }
}
