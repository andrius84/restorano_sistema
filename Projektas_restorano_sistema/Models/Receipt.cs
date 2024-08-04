using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Models
{
    internal class Receipt
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PaymentTime { get; set; }
    }
}
