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
            Console.WriteLine("1. Kurti uzsakyma");
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
                    AddDishToOrder(_order);
                    _order.TotalPrice = _orderService.CalculateOrderTotalPrice(_order.Id);
                    Console.WriteLine("Uzsakymas sukurtas!");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Koreguoti uzsakyma");
                    Console.WriteLine("Pasirinkite uzsakyma, kuri norite koreguoti:");
                    ShowOrders();
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
            //var color = table.IsOccupied ? ConsoleColor.Red : ConsoleColor.Green;
            //Console.ForegroundColor = color;
            //Console.ResetColor();
            return status;
        }
        public int ChooseTable()
        {
            Console.WriteLine("Pasirinkite staliuka:");
            var tableId = int.Parse(Console.ReadLine());
            return tableId;
        }
        public void PrintDishes()
        {
            Console.Clear();
            Console.WriteLine("PATIEKALAI:");
            var foodList = _dishes.Dishes().OrderBy(x => x.Id).ToList();
            int count = 0;
            foreach (var food in foodList)
            {
                Console.Write($"{food.Id}. {food.Name} - {food.Price}eur\t\t");
                count++;
                if (count % 5 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
        public void PrintBeverages()
        {
            Console.Clear();
            Console.WriteLine("GERIMAI:");
            var beverageList = _beverage.Beverages().OrderBy(x => x.Id).ToList();
            int count = 0;
            foreach (var beverage in beverageList)
            {
                Console.Write($"{beverage.Id}. {beverage.Name} - {beverage.Price}eur\t\t");
                count++;
                if (count % 5 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
        public void ShowOrders()
        {
            var orders = _orderService.GetOrders();
            foreach (var order in orders)
            {
                Console.WriteLine($"Uzsakymo numeris: {order.Id}");
                Console.WriteLine($"Staliuko numeris: {_order.Table.Id}");
                Console.WriteLine($"Kaina: {order.TotalPrice}");
                Console.WriteLine("");
            }
        }
        public void AddDishToOrder(Order order)
        {
            while (true)
            {
                PrintDishes();
                Console.WriteLine("Pasirinkite patiekala:");
                var dishId = int.Parse(Console.ReadLine());
                var dish = _dishes.Dishes().FirstOrDefault(x => x.Id == dishId);
                _orderService.AddDishToOrder(_order.Id, dish);
                Console.WriteLine("Patiekalas pridetas");
                Console.WriteLine("Ar norite prideti dar viena patiekala? (ne - iseiti)");
                var answer = Console.ReadLine();
                if (answer == "ne")
                {
                    break;
                }
            }
        }
    }
}
