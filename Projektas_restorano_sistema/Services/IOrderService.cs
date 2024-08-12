using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;

namespace RestoranoSistema.Services
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        decimal CalculateOrderTotalPrice(Guid orderId);
        void UpdateOrder(Order order);
        void DeleteOrder(Guid orderId);
        void AddDishToOrder(Guid orderId, Dish dish);
        void AddBeverageToOrder(Guid orderId, Beverage beverage);
        List<Order> GetOrders();
        Guid GenerateOrderNumber();
        Order GetOrderByTableId(int tableId);
        void DeleteDishFromOrder(Guid orderId, Dish dish);
        void DeleteBeverageFromOrder(Guid orderId, Beverage beverage);


    }
}
