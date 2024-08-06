using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;
using RestoranoSistema.Repositories;

namespace RestoranoSistema.Services
{
    public class OrderService : IOrderService
    {
        private Order _order;
        private readonly IOrdersRepository _orderRepository;
        private readonly IItemsRepository _itemsRepository;

        public OrderService(Order order, IOrdersRepository ordersRepository, IItemsRepository itemsRepository)
        {
            _order = order;
            _orderRepository = ordersRepository;
            _itemsRepository = itemsRepository;
        }
        public void CreateOrder()
        {
            throw new NotImplementedException();
        }
        public Dish AddFoodItemToOrder(int foodId)
        {
            var foodList = _itemsRepository.GetFoodList();
            var foodItem = foodList.FirstOrDefault(x => x.Id == foodId);
            if (foodItem == null)
            {
                throw new Exception("Food item not found");
            }
            _order.Dishes ??= new List<Dish>();
            _order.Dishes.Add(foodItem);
            return foodItem;
        }
        public Beverage AddBeverageItemToOrder(int beverageId)
        {
            var beverageList = _itemsRepository.GetBeverageList();
            var beverageItem = beverageList.FirstOrDefault(x => x.Id == beverageId);
            if (beverageItem == null)
            {
                throw new Exception("Beverage item not found");
            }
            _order.Beverages ??= new List<Beverage>();
            _order.Beverages.Add(beverageItem);
            return beverageItem;
        }
        public void RemoveFoodItemFromOrder(int foodId)
        {
            var foodItem = _order.Dishes.FirstOrDefault(x => x.Id == foodId);
            if (foodItem == null)
            {
                throw new Exception("Food item not found");
            }
            _order.Dishes.Remove(foodItem);
        }
        public void RemoveBeverageItemFromOrder(int beverageId)
        {
            var beverageItem = _order.Beverages.FirstOrDefault(x => x.Id == beverageId);
            if (beverageItem == null)
            {
                throw new Exception("Beverage item not found");
            }
            _order.Beverages.Remove(beverageItem);
        }
    }
}
