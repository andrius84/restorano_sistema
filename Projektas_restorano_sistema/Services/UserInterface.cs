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

namespace RestoranoSistema.Services
{
    public class UserInterface(ITableService tableService, IOrderService orderService, IReceiptService receiptService, Dish dishes, Beverage beverage, Order order) : IUserInterface
    {
        private readonly ITableService _tableService = tableService;
        private readonly IOrderService _orderService = orderService;
        private readonly IReceiptService _receiptService = receiptService;
        private readonly Dish _dishes = dishes;
        private readonly Beverage _beverage = beverage;
        private readonly Order _order = order;
        public void ShowMainMenu()
        {
            Console.Clear();
            PrintHeader();
            Console.WriteLine("Pasirinkite veiksmą:");
            Console.WriteLine("");
            Console.WriteLine("1. Kurti naują užsakymą");
            Console.WriteLine("2. Koreguoti užsakymą");
            Console.WriteLine("3. Apmokėti užsakymą");
            Console.WriteLine("4. Atlaisvinti staliuką");
            Console.WriteLine("5. Išjungti programą");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.Clear();
                    PrintHeader();
                    ShowTablesOccupation();
                    Console.WriteLine("");
                    Console.WriteLine("Pasirinkite staliuką:");
                    var tableId = ChooseTable();
                    _tableService.MarkTableAsOccupied(tableId);
                    _order.Table = _tableService.GetTable(tableId);
                    _order.Id = _orderService.GenerateOrderNumber();
                    _orderService.CreateOrder(_order);
                    Console.WriteLine("Užsakymas sukurtas!");
                    Console.ReadKey();
                    option = "2";
                    break;
                case "2":
                    Console.Clear();
                    PrintHeader();
                    if (_orderService.GetOrders().Count == 0)
                    {
                        Console.WriteLine("Užsakymų nėra, pirmiausia sukurkite užsakymą");
                        Console.ReadKey();
                        break;
                    }
                    ShowOrders();
                    Console.WriteLine("Pasirinkite, kurio staliuko užsakymą koreguosite:");
                    var table = ChooseTable();
                    _order.Table = _tableService.GetTable(table);
                    var order = _orderService.GetOrderByTableId(_order.Table.Id);
                    _order.Id = order.Id;
                    Console.WriteLine("1. Pridėti patiekalą ar gerimą prie užsakymo");
                    Console.WriteLine("2. Pašalinti patiekalą ar gerimą iš užsakymo");    
                    switch (Console.ReadLine())
                    {
                        case "1":
                            while (true)
                            {
                                Console.Clear();
                                PrintHeader();
                                ShowDishesAndBeverages();
                                var menuItems = GetMenuItems();
                                Console.WriteLine("");
                                order = _orderService.GetOrderByTableId(order.Table.Id);
                                ShowOrder(order);
                                var menuItem = Console.ReadLine();
                                if (menuItem == "q")
                                { 
                                    break;
                                }
                                else if (int.TryParse(menuItem, out int number))
                                {
                                    AddFoodOrDrinkToOrder(menuItems.FirstOrDefault(x => x.Id == number)!);
                                }
                                else
                                {
                                    Console.WriteLine("Neteisingas pasirinkimas");
                                }
                            }
                            break;
                        case "2":
                            Console.Clear();
                            PrintHeader();
                            ShowOrder(_order);
                            DeleteFoodOrDrinkFromOrder(order);
                            break;
                        default:
                            Console.WriteLine("Neteisingas pasirinkimas");
                            break;
                    }

                    break;
                case "3":
                    Console.Clear();
                    PrintHeader();
                    if (_orderService.GetOrders().Count == 0)
                    {
                        Console.WriteLine("Užsakymų nėra, pirmiausia sukurkite užsakymą");
                        Console.ReadKey();
                        break;
                    }
                    ShowOrders();
                    Console.WriteLine("Pasirinkite staliuko užsakymą, kurį norite apmokėti:");
                    var tableToPay = ChooseTable();
                    _order.Table = _tableService.GetTable(tableToPay);
                    var orderToPay = _orderService.GetOrderByTableId(_order.Table.Id);
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
                    break;
                case "4":
                    Console.Clear();
                    PrintHeader();
                    ShowTablesOccupation();
                    Console.WriteLine("Pasirinkite staliuką, kurį norite atlaisvinti:");
                    var tableIdf = ChooseTable();
                    _tableService.MarkTableAsFree(tableIdf);
                    Console.WriteLine("Staliukas atlaisvintas");
                    Console.ReadKey();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Neteisingas pasirinkimas");
                    break;
            }
        }
        public void ShowTablesOccupation()
        {
            Console.Clear();
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
            Console.ReadKey();

        }
        public void AddFoodOrDrinkToOrder(MenuItem menuItem)
        {
            if (menuItem == null)
            {
                Console.WriteLine("Neteisingas pasirinkimas");
            }
            else if (menuItem is Dish dish)
            {
                _orderService.AddDishToOrder(order.Id, dish);
                Console.WriteLine($"{dish.Name} pridėtas prie užsakymo");
            }
            else if (menuItem is Beverage beverage)
            {
                _orderService.AddBeverageToOrder(order.Id, beverage);
                Console.WriteLine($"{beverage.Name} pridėtas prie užsakymo");
            }     
        }
        public void DeleteFoodOrDrinkFromOrder(Order order)
        {
            NotImplementedException();
        }
        public List<MenuItem> GetMenuItems()
        {
            var foodList = _dishes.Dishes().OrderBy(x => x.Id).ToList();
            var beverageList = _beverage.Beverages().OrderBy(x => x.Id).ToList();
            var menu = foodList.Cast<MenuItem>().Concat(beverageList.Cast<MenuItem>()).ToList();
            for (int i = 0; i < menu.Count; i++)
            {
                menu[i].Id = i + 1;
            }
            return menu;
        }
        static void PrintHeader()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("|             RESTORANAS                |");
            Console.WriteLine("=========================================");
            Console.WriteLine();
        }
    }
}
