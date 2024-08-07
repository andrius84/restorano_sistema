using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Repositories;
using RestoranoSistema.Models;

namespace RestoranoSistema.Services
{
    public class Restaurant : IRestaurant
    {
        public void Start()
        {
            while (true)
            {
                //var waiters = new List<Waiter>();
                //var waitersRepository = new WaitersRepository(waiters, "../../../Repositories/waiters.csv");
                //var waiterslist = waitersRepository.LoadWaitersList();

                var table = new Table();
                var order = new Order();

                IItemsRepository itemsRepository = new ItemsRepository("../../../Repositories/food.csv", "../../../Repositories/drinks.csv");

                var dish = new Dish(itemsRepository);
                var beverage = new Beverage(itemsRepository);

                ITableRepository tableRepository = new TablesRepository(table, "../../../Repositories/tables.csv");
                ITableService tableService = new TableService(table, tableRepository);

                IOrdersRepository ordersRepository = new OrdersRepository(order, "../../../Repositories/orders.json");
                IOrderService orderService = new OrderService(order, ordersRepository, itemsRepository);

                IUserInterface userinterface = new UserInterface(tableService, orderService, dish, beverage, order);

                userinterface.ShowMainMenu();

                Console.ReadKey();



            }
        }

    }
}
