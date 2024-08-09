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
    public class UserInterface(ITableService tableService, IOrderService orderService, Dish dishes, Beverage beverage, Order order) : IUserInterface
    {
        private readonly ITableService _tableService = tableService;
        private readonly IOrderService _orderService = orderService;
        private readonly Dish _dishes = dishes;
        private readonly Beverage _beverage = beverage;
        private readonly Order _order = order;
        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Pasirinkite veiksma:");
            Console.WriteLine("");
            Console.WriteLine("1. Kurti nauja uzsakyma");
            Console.WriteLine("2. Koreguoti uzsakyma");
            Console.WriteLine("3. Apmoketi uzsakyma");
            Console.WriteLine("4. Atlaisvinti staliuka");
            Console.WriteLine("5. Isjungti programa");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    ShowTablesOccupation();
                    Console.WriteLine("");
                    Console.WriteLine("Pasirinkite staliuka:");
                    var tableId = ChooseTable();
                    _tableService.MarkTableAsOccupied(tableId);
                    _order.Table = _tableService.GetTable(tableId);
                    _order.Id = _orderService.GenerateOrderNumber();
                    _orderService.CreateOrder(_order);
                    Console.WriteLine("Uzsakymas sukurtas!");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    ShowOrders();
                    Console.WriteLine("Pasirinkite kurio staliuko uzsakyma koreguosite:");
                    var table = ChooseTable();
                    _order.Table = _tableService.GetTable(table);
                    var order = _orderService.GetOrderByTableId(_order.Table.Id);
                    _order.Id = order.Id;
                    Console.WriteLine("1. Pridėti patiekalą ar gerimą prie užsakymo");
                    Console.WriteLine("2. Pašalinti patiekalą ar gerimą iš užsakymo");    
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.Clear();
                            ShowOrder(_order);
                            AddFoodOrDrinkToOrder(order);
                            Console.ReadKey();
                            break;
                        case "2":
                            Console.Clear();
                            ShowOrder(_order);
                            DeleteFoodOrDrinkFromOrder(order);
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Neteisingas pasirinkimas");
                            break;
                    }

                    break;
                case "3":
                    Console.WriteLine("Apmoketi");
                    break;
                case "4":
                    Console.Clear();
                    ShowTablesOccupation();
                    Console.WriteLine("Pasirinkite staliuka, kuri norite atlaisvinti:");
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
            var status = table.IsOccupied ? "uzimtas" : "laisvas";
            return status;
        }
        public int ChooseTable()
        {
            var tableId = int.Parse(Console.ReadLine());
            return tableId;
        }
        public void ShowDishesAndBeverages()
        {
            Console.Clear();
            Console.WriteLine("MENIU:");
            var menu = GetMenuItems();
            int count = 0;
            int maxLineLength = 42; // Variable to store the maximum line length
            foreach (var m in menu)
            {
                string line = $"{m.Id}. {m.Name} - {m.Price}eur";
                int gapSize = maxLineLength - line.Length; // Calculate the gap size
                Console.Write(line);
                Console.Write(new string(' ', gapSize)); // Add the gap at the end of the line
                count++;
                if (count % 5 == 0)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("\t"); // Add a tab separator between columns
                }
            }
        }
        public void ShowOrders()
        {
            Console.Clear();
            Console.WriteLine("UZSAKYMAI:");
            var orders = _orderService.GetOrders();
            foreach (var order in orders)
            {
                Console.WriteLine($"Uzsakymo numeris: {order.Id}");
                Console.WriteLine($"Staliuko numeris: {order.Table.Id}");
                Console.WriteLine($"Kaina: {order.TotalPrice}");
                Console.WriteLine("");
            }
        }
        public void ShowOrder(Order order)
        {
            Console.Clear();
            Console.WriteLine("UZSAKYMAS:");
            Console.WriteLine($"Uzsakymo numeris: {order.Id}");
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
            Console.WriteLine("Gerimai:");
            if (order.Beverages != null && order.Beverages.Count > 0)
            {
                foreach (var beverage in order.Beverages)
                {
                    Console.WriteLine($"{beverage.Id}. {beverage.Name} - {beverage.Price}eur");
                }
            }
            Console.ReadKey();

        }
        public void AddFoodOrDrinkToOrder(Order order)
        {
            while (true)
            {
                ShowDishesAndBeverages();
                Console.WriteLine("Pasirinkite patiekala ar gerima kuri norite prideti prie uzsakymo:");
                var menuItemId = int.Parse(Console.ReadLine());
                var menuItem = GetMenuItems();
                var menuItemToAdd = menuItem.FirstOrDefault(x => x.Id == menuItemId);
                if (menuItemToAdd == null)
                {
                    Console.WriteLine("Neteisingas pasirinkimas");
                    continue;
                }
                if (menuItemToAdd is Dish dish)
                {
                    _orderService.AddDishToOrder(order.Id, dish);
                }
                else if (menuItemToAdd is Beverage beverage)
                {
                    _orderService.AddBeverageToOrder(order.Id, beverage);
                }
            }
        }
        public void DeleteFoodOrDrinkFromOrder(Order order)
        {
            while (true)
            {
                ShowOrder(order);
                Console.WriteLine("Pasirinkite patiekala ar gerima kuri norite istrinti is uzsakymo:");
                var menuItemId = int.Parse(Console.ReadLine());
                var menuItem = _dishes.Dishes().FirstOrDefault(x => x.Id == menuItemId) as MenuItem ?? _beverage.Beverages().FirstOrDefault(x => x.Id == menuItemId) as MenuItem;
                if (menuItem == null)
                {
                    Console.WriteLine("Neteisingas pasirinkimas");
                    continue;
                }
                if (menuItem is Dish dish)
                {
                    _orderService.DeleteDishFromOrder(order.Id, dish);
                }
                else if (menuItem is Beverage beverage)
                {
                    _orderService.DeleteBeverageFromOrder(order.Id, beverage);
                }
                Console.WriteLine("Patiekalas ar gerimas istrintas is uzsakymo");
                Console.ReadKey();
            }
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
    }
}
