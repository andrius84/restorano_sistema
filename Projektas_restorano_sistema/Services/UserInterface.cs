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
    public class UserInterface(List<Table> tables, TableService tableservice, OrderService orderservice) : IUserInterface
    {
        private readonly List<Table> _tables = tables;
        private readonly TableService _tableservice = tableservice;
        private readonly OrderService _orderservice = orderservice;

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Pasirinkite norima veiksma:");
            Console.WriteLine("1. Kurti uzsakyma");
            Console.WriteLine("2. Apmoketi");
            Console.WriteLine("3. Isjungti programa");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Kurti uzsakyma");
                    ChooseTable();
                    _orderservice.CreateOrder();

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
            Console.WriteLine("Pasirinkite staliuka: ");
            Console.WriteLine($"Staliukas 1 yra {(_tables[0].IsOccupied ? "uzimtas" : "laisvas")}");
            Console.WriteLine($"Staliukas 2 yra {(_tables[1].IsOccupied ? "uzimtas" : "laisvas")}");
            Console.WriteLine($"Staliukas 3 yra {(_tables[2].IsOccupied ? "uzimtas" : "laisvas")}");
            Console.WriteLine($"Staliukas 4 yra {(_tables[3].IsOccupied ? "uzimtas" : "laisvas")}");
            Console.WriteLine($"Staliukas 5 yra {(_tables[4].IsOccupied ? "uzimtas" : "laisvas")}");
        }
        public void ChooseTable()
        {
            var choose = Console.ReadLine();
            switch (choose)
            {
                case "1":
                    _tableservice.MarkTableAsOccupied(1);
                    break;
                case "2":
                    _tableservice.MarkTableAsOccupied(2);
                    break;
                case "3":
                    _tableservice.MarkTableAsOccupied(3);
                    break;
                case "4":
                    _tableservice.MarkTableAsOccupied(4);
                    break;
                case "5":
                    _tableservice.MarkTableAsOccupied(5);
                    break;
                default:
                    Console.WriteLine("Neteisingas pasirinkimas");
                    break;
            }
        }
    }
}
