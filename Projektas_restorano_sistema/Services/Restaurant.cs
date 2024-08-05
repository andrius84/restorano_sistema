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
                var waiters = new List<Waiter>();
                var waiterRepository = new WaitersRepository(waiters, "../../../Repositories/waiters.csv");
                var waiterslist = waiterRepository.LoadWaitersList();

                var tables = new List<Table>();
                var tablesrepository = new TablesRepository(tables, "../../../Repositories/tables.csv");
                var tableslist = tablesrepository.LoadTablesList();

                var tableservice = new TableService(tableslist);
                
                var orders = new List<Order>();
                var orderservice = new OrderService(orders);

                var userinterface = new UserInterface(tableslist, tableservice, orderservice);
                userinterface.ShowTablesOccupation();
                Console.ReadKey();
                userinterface.ShowMenu();
                userinterface.ChooseTable();
                userinterface.ShowTablesOccupation();

                Console.ReadKey();



            }
        }

    }
}
