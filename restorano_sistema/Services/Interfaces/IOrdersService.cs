using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;

namespace RestoranoSistema.Services.Interfaces
{
    public interface IOrdersService
    {
        void CreateOrder(Table table);
        void UpdateOrder(Order order);
        void DeleteOrder(Guid orderId);
        void AddDishToOrder(Guid orderId, Dish dish);
        void AddBeverageToOrder(Guid orderId, Beverage beverage);
        List<Order> GetOrders();
        Order GetOrderByTableId(int tableId);
        Order GetOrderById(Guid orderId);
        void DeleteDishFromOrder(Guid orderId, Dish dish);
        void DeleteBeverageFromOrder(Guid orderId, Beverage beverage);
    }
}
