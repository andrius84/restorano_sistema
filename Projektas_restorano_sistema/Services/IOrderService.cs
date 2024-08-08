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
        void AddDishToOrder(Guid orderId, Dish dish);
        void AddBeverageToOrder(Guid orderId, Beverage beverage);
        List<Order> GetOrders();
        Guid GenerateOrderNumber();
        void UpdateOrder(Order order);
        Order GetOrderByTableId(int tableId);
        void DeleteDishFromOrder(Guid orderId, Dish dish);
        void DeleteBeverageFromOrder(Guid orderId, Beverage beverage);


    }
}
