using RestoranoSistema.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestoranoSistema.Services
{
    public class UserInterface(ITableService tableService, IOrderService orderService, Dish dishes, Beverage beverage) : IUserInterface
    {
        private readonly ITableService _tableService = tableService;
        private readonly IOrderService _orderService = orderService;
        private readonly Dish _dishes = dishes;
        private readonly Beverage _beverage = beverage;
        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Pasirinkite veiksma:");
            Console.WriteLine("");
            Console.WriteLine("1. Kurti uzsakyma");
            Console.WriteLine("2. Apmoketi uzsakyma");
            Console.WriteLine("3. Atlaisvinti staliuka");
            Console.WriteLine("4. Isjungti programa");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    ShowTablesOccupation();
                    Console.WriteLine("");
                    Console.WriteLine("Pasirinkite staliuka:");
                    var tableId = ChooseTable();
                    _tableService.GetTable(tableId);
                    _tableService.MarkTableAsOccupied(tableId);
                    Console.WriteLine(">>kurti uzsakyma enter<<");
                    Console.ReadKey();
                    PrintDishes();
                    _orderService.CreateOrder();
                    break;
                case "2":
                    Console.WriteLine("Apmoketi");
                    break;
                case "3":
                    Console.WriteLine("Atsijungti");
                    break;
                default:
                    Console.WriteLine("Neteisingas pasirinkimas");
                    break;
            }
        }
        public void ShowTablesOccupation()
        {
            Console.Clear();
            Console.WriteLine("    +-------------+             +-------------+             +-------------+             +-------------+             +-------------+");
            Console.WriteLine($"    |  Staliukas 1 |             |  Staliukas 2 |             |  Staliukas 3 |             |  Staliukas 4 |             |  Staliukas 5 |");
            Console.WriteLine($"    |     yra      |             |     yra      |             |     yra      |             |     yra      |             |     yra      |");
            Console.WriteLine($"    | {(_tableService.GetTable(1).IsOccupied ? "uzimtas" : "laisvas")}  |             |  {(_tableService.GetTable(2).IsOccupied ? "uzimtas" : "laisvas")}  |             |  {(_tableService.GetTable(3).IsOccupied ? "uzimtas" : "laisvas")}  |             |  {(_tableService.GetTable(4).IsOccupied ? "uzimtas" : "laisvas")}  |             |  {(_tableService.GetTable(5).IsOccupied ? "uzimtas" : "laisvas")}  |");
            Console.WriteLine("    +-------------+             +-------------+             +-------------+             +-------------+             +-------------+");
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
            Console.WriteLine("Pasirinkite patiekala:");
            var foodList = _dishes.Dishes();
            foreach (var food in foodList)
            {
                Console.WriteLine($"{food.Id}. {food.Name} - {food.Price}eur");
            }
        }
    }
}
