using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Table Table { get; set; }
        public List<Beverage>? Beverages { get; set; }
        public List<Dish>? Dishes { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
