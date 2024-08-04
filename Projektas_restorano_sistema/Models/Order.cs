using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int WaiterId { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime ServeTime { get; set; }
        public bool IsServed { get; set; }
        public bool IsPaid { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
