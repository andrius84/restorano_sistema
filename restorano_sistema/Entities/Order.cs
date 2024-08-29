using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoranoSistema.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime OrderTime { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                if (Dishes != null)
                {
                    totalPrice += Dishes.Sum(x => x.Price);
                }
                if (Beverages != null)
                {
                    totalPrice += Beverages.Sum(x => x.Price);
                }
                return totalPrice;
            }
        }
        [ForeignKey("Table")]
        public int TableId { get; set; }
        public Table Table { get; set; }
   
        public List<Beverage>? Beverages { get; set; }
       
        public List<Dish>? Dishes { get; set; }

    }
}
