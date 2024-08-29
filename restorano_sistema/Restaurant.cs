using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Repositories;
using RestoranoSistema.Entities;
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
            RestoranasDbContext dbContext = new RestoranasDbContext(); 

            ITableRepository tableRepository = new TablesRepository(dbContext); 

            IItemsRepository itemsRepository = new ItemsRepository(dbContext);
            IItemsService itemsService = new ItemsService(itemsRepository);

            ITablesService tableService = new TablesService(tableRepository);

            IOrdersRepository ordersRepository = new OrdersRepository(dbContext);
            IOrdersService orderService = new OrdersService(ordersRepository);

            IReceiptRepository receiptRepository = new ReceiptsRepository("../../../Data/receipts.csv");
            IReceiptsService receiptService = new ReceiptsService(receiptRepository);

            IUserInterface userinterface = new UserInterface(tableService, orderService, receiptService, itemsService);

            while (true)
            {
                userinterface.ShowMainMenu();
            }
        }
    }
}
