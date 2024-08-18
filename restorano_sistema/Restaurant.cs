using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Repositories;
using RestoranoSistema.Models;
using RestoranoSistema.Services;
using RestoranoSistema.UI;
using RestoranoSistema.Services.Interfaces;
using RestoranoSistema.Repositories.Interfaces;
using RestoranoSistema.UI.Interfaces;

namespace RestoranoSistema
{
    public class Restaurant : IRestaurant
    {
        public void Start()
        {
            while (true)
            {
                IItemsRepository itemsRepository = new ItemsRepository("../../../Data/food.csv", "../../../Data/drinks.csv");
                IItemsService itemsService = new ItemsService(itemsRepository);

                ITableRepository tableRepository = new TablesRepository("../../../Data/tables.csv");
                ITablesService tableService = new TablesService(tableRepository);

                IOrdersRepository ordersRepository = new OrdersRepository("../../../Data/orders.json");
                IOrdersService orderService = new OrdersService(ordersRepository);

                IReceiptRepository receiptRepository = new ReceiptsRepository("../../../Data/receipts.csv");
                IReceiptsService receiptService = new ReceiptsService(receiptRepository);

                IUserInterface userinterface = new UserInterface(tableService, orderService, receiptService, itemsService);

                userinterface.ShowMainMenu();
            }
        }

    }
}
