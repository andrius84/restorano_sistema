using RestoranoSistema.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RestoranoSistema.Repositories;
using System.ComponentModel;
using RestoranoSistema.Services;
using System.Runtime.InteropServices;
using RestoranoSistema.Services.Interfaces;
using RestoranoSistema.UI.Interfaces;

namespace RestoranoSistema.UI
{
    public class UserInterface(ITableService tableService, IOrderService orderService, IReceiptService receiptService, IItemsService itemService) : IUserInterface
    {
        private readonly ITableService _tableService = tableService;
        private readonly IOrderService _orderService = orderService;
        private readonly IReceiptService _receiptService = receiptService;
        private readonly IItemsService _itemService = itemService;
        public void ShowMainMenu()
        {
            Console.Clear();
            PrintHeader();
            Console.WriteLine("1. Kurti naują užsakymą");
            Console.WriteLine("2. Pildyti/koreguoti užsakymą");
            Console.WriteLine("3. Apmokėti užsakymą");
            Console.WriteLine("4. Ištrinti užsakymą");
            Console.WriteLine("5. Atlaisvinti staliuką");
            Console.WriteLine("6. Išjungti programą");
            Console.WriteLine("");
            Console.Write("Pasirinkite veiksmą: ");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    var orderId = CreateOrder();
                    UpdateOrder(orderId);
                    break;
                case "2":
                    var order = ChooseOrder();
                    UpdateOrder(order.Id);
                    break;
                case "3":
                    PayForOrder();
                    break;
                case "4":
                    DeleteOrder();
                    break;
                case "5":
                    MakeTableFree();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Neteisingas pasirinkimas");
                    break;
            }
        }
        private Order ChooseOrder()
        {
            Console.Clear();
            PrintHeader();
            if (_orderService.GetOrders().Count == 0)
            {
                Console.WriteLine("Užsakymų nėra, pirmiausia sukurkite užsakymą");
                Console.ReadKey();
                return null;
            }
            ShowOrders();
            Console.WriteLine("Pasirinkite, kurio staliuko užsakymą koreguosite:");
            Console.WriteLine("");
            var tableId = ChooseTable();
            var table = _tableService.GetTable(tableId);
            var order = _orderService.GetOrderByTableId(table.Id);
            return order;
        }
        private Guid CreateOrder()
        {
            Console.Clear();
            PrintHeader();
            ShowTablesOccupation();
            Console.WriteLine("");
            Console.Write("Pasirinkite staliuką: ");
            var tableId = ChooseTable();
            _tableService.MarkTableAsOccupied(tableId);
            var table = _tableService.GetTable(tableId);
            _orderService.CreateOrder(table);
            var order = _orderService.GetOrderByTableId(tableId);
            Console.WriteLine("Užsakymas sukurtas!");
            Console.Write("Spauskite bet kurį mygtuką, kad tęsti...");
            Console.ReadKey();
            return order.Id;
        }
        private void UpdateOrder(Guid orderId)
        {
            Console.Clear();
            PrintHeader();
            Console.WriteLine("1. Pridėti patiekalą ar gėrimą prie užsakymo");
            Console.WriteLine("2. Pašalinti patiekalą ar gėrimą iš užsakymo");
            Console.WriteLine("");
            Console.Write("Pasirinkite veiksmą: ");
            switch (Console.ReadLine())
            {
                case "1":
                    while (true)
                    {
                        Console.Clear();
                        PrintHeader();
                        ShowDishesAndBeverages();
                        Console.WriteLine("");
                        var order = _orderService.GetOrderById(orderId);
                        ShowOrder(order);
                        Console.WriteLine("");
                        Console.Write("Įveskite ką norite pridėti arba 'q' jei norite grįžti): ");
                        var menuItem = Console.ReadLine();
                        if (menuItem == "q")
                        {
                            break;
                        }
                        else if (int.TryParse(menuItem, out int number))
                        {
                            AddFoodOrDrinkToOrder(orderId, GetMenuItems().FirstOrDefault(x => x.Id == number)!);
                        }
                        else
                        {
                            Console.WriteLine("Neteisingas pasirinkimas");
                        }
                    }
                    break;
                case "2":
                    while (true)
                    {
                        Console.Clear();
                        PrintHeader();
                        var order = _orderService.GetOrderById(orderId);
                        ShowOrder(order);
                        Console.WriteLine("");
                        Console.Write("Įveskite ką norite pašalinti arba 'q' jei norite grįžti: ");
                        var menuItemToDelete = Console.ReadLine();
                        if (menuItemToDelete == "q")
                        {
                            break;
                        }
                        else if (int.TryParse(menuItemToDelete, out int number))
                        {
                            DeleteFoodOrDrinkFromOrder(orderId, GetMenuItems().FirstOrDefault(x => x.Id == number)!);
                        }
                        else
                        {
                            Console.WriteLine("Neteisingas pasirinkimas");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Neteisingas pasirinkimas");
                    break;
            }
        }
        private void PayForOrder()
        {
            Console.Clear();
            PrintHeader();
            if (_orderService.GetOrders().Count == 0)
            {
                Console.WriteLine("Užsakymų nėra, pirmiausia sukurkite užsakymą");
                Console.ReadKey();
                return;
            }
            ShowOrders();
            Console.WriteLine("Pasirinkite staliuko užsakymą, kurį norite apmokėti:");
            var tableToPay = ChooseTable();
            var tableObj = _tableService.GetTable(tableToPay);
            var orderToPay = _orderService.GetOrderByTableId(tableObj.Id);
            _receiptService.GenerateRestaurantReceipt(orderToPay);
            _tableService.MarkTableAsFree(tableToPay);
            Console.WriteLine("Užsakymas apmokėtas");
            Console.WriteLine("Ar norite išspausdinti kliento čekį? (taip/ne)");
            var answer = Console.ReadLine();
            if (answer == "taip")
            {
                _receiptService.GenerateClientReceipt(orderToPay);
            }
            if (answer == "ne")
            {
                Console.WriteLine("Čekis nebuvo išspausdintas");
            }
            _orderService.DeleteOrder(orderToPay.Id);
            Console.ReadKey();
        }
        private void DeleteOrder()
        {
            Console.Clear();
            PrintHeader();
            if (_orderService.GetOrders().Count == 0)
            {
                Console.WriteLine("Užsakymų nėra, pirmiausia sukurkite užsakymą");
                Console.ReadKey();
                return;
            }
            ShowOrders();
            Console.WriteLine("Pasirinkite staliuką, kurio užsakymą norite ištrinti:");
            var tableToDelete = ChooseTable();
            var tableOb = _tableService.GetTable(tableToDelete);
            var orderToDelete = _orderService.GetOrderByTableId(tableOb.Id);
            _orderService.DeleteOrder(orderToDelete.Id);
            Console.WriteLine("Užsakymas ištrintas");
            Console.ReadKey();
        }
        private void MakeTableFree()
        {
            Console.Clear();
            PrintHeader();
            ShowTablesOccupation();
            Console.WriteLine("Pasirinkite staliuką, kurį norite atlaisvinti:");
            var tableIdf = ChooseTable();
            _tableService.MarkTableAsFree(tableIdf);
            Console.WriteLine("Staliukas atlaisvintas");
            Console.ReadKey();
        }
        private void ShowTablesOccupation()
        {
            Console.Clear();
            PrintHeader();
            Console.WriteLine("     +-------------+      +-------------+      +-------------+      +-------------+      +-------------+");
            Console.WriteLine($"    |  Staliukas 1  |    |  Staliukas 2  |    |  Staliukas 3  |    |  Staliukas 4  |    |  Staliukas 5  |");
            Console.WriteLine($"    |      yra      |    |      yra      |    |      yra      |    |      yra      |    |      yra      |");
            Console.WriteLine($"    |    {GTS(1)}    |    |    {GTS(2)}    |    |    {GTS(3)}    |    |    {GTS(4)}    |    |    {GTS(5)}    |");
            Console.WriteLine("     +-------------+      +-------------+      +-------------+      +-------------+      +-------------+");
        }
        public string GTS(int tableId)
        {
            var table = _tableService.GetTable(tableId);
            var status = table.IsOccupied ? "užimtas" : "laisvas";
            return status;
        }
        public int ChooseTable()
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out int tableId))
            {
                return tableId;
            }
            else
            {
                Console.WriteLine("Neteisingai įvestas staliuko numeris");
                return ChooseTable();
            }
        }
        public void ShowDishesAndBeverages()
        {
            Console.WriteLine("MENIU:");
            var menu = GetMenuItems();
            int count = 0;
            int maxLineLength = 42;
            foreach (var m in menu)
            {
                string line = $"{m.Id}. {m.Name} - {m.Price}eur";
                int gapSize = maxLineLength - line.Length;
                Console.Write(line);
                Console.Write(new string(' ', gapSize));
                count++;
                if (count % 5 == 0)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }
        public void ShowOrders()
        {
            Console.Clear();
            Console.WriteLine("UŽSAKYMAI:");
            var orders = _orderService.GetOrders();
            foreach (var order in orders)
            {
                Console.WriteLine($"Užsakymo numeris: {order.Id}");
                Console.WriteLine($"Staliuko numeris: {order.Table.Id}");
                Console.WriteLine($"Kaina: {order.TotalPrice}");
                Console.WriteLine("=======================================");
            }
        }
        public void ShowOrder(Order order)
        {
            Console.WriteLine("UŽSAKYMAS:");
            Console.WriteLine($"Užsakymo numeris: {order.Id}");
            Console.WriteLine($"Staliuko numeris: {order.Table.Id}");
            Console.WriteLine($"Kaina: {order.TotalPrice}");
            Console.WriteLine("");
            Console.WriteLine("Patiekalai:");
            if (order.Dishes != null && order.Dishes.Count > 0)
            {
                foreach (var dish in order.Dishes)
                {
                    Console.WriteLine($"{dish.Id}. {dish.Name} - {dish.Price}eur");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Gėrimai:");
            if (order.Beverages != null && order.Beverages.Count > 0)
            {
                foreach (var beverage in order.Beverages)
                {
                    Console.WriteLine($"{beverage.Id}. {beverage.Name} - {beverage.Price}eur");
                }
            }
        }
        public void AddFoodOrDrinkToOrder(Guid orderId, MenuItem menuItem)
        {
            if (menuItem == null)
            {
                Console.WriteLine("Neteisingas pasirinkimas");
            }
            else if (menuItem is Dish dish)
            {
                _orderService.AddDishToOrder(orderId, dish);
                Console.WriteLine($"{dish.Name} pridėtas prie užsakymo");
            }
            else if (menuItem is Beverage beverage)
            {
                _orderService.AddBeverageToOrder(orderId, beverage);
                Console.WriteLine($"{beverage.Name} pridėtas prie užsakymo");
            }
        }
        public void DeleteFoodOrDrinkFromOrder(Guid orderId, MenuItem menuItem)
        {
            if (menuItem == null)
            {
                Console.WriteLine("Neteisingas pasirinkimas");
            }
            else if (menuItem is Dish dish)
            {
                _orderService.DeleteDishFromOrder(orderId, dish);
                Console.WriteLine($"{dish.Name} pašalintas iš užsakymo");
            }
            else if (menuItem is Beverage beverage)
            {
                _orderService.DeleteBeverageFromOrder(orderId, beverage);
                Console.WriteLine($"{beverage.Name} pašalintas iš užsakymo");
            }
        }
        public List<MenuItem> GetMenuItems()
        {
            var foodList = _itemService.Dishes().OrderBy(x => x.Id).ToList();
            var beverageList = _itemService.Beverages().OrderBy(x => x.Id).ToList();
            var menu = foodList.Cast<MenuItem>().Concat(beverageList.Cast<MenuItem>()).ToList();
            for (int i = 0; i < menu.Count; i++)
            {
                menu[i].Id = i + 1;
            }
            return menu;
        }
        static void PrintHeader()
        {
            Console.WriteLine("=======================================================================");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("|                            RESTORANAS                               |");
            Console.ResetColor();
            Console.WriteLine("=======================================================================");
            Console.WriteLine();
        }
    }
}
