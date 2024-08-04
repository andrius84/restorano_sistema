using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Models
{
    internal class Tables
    {
        public int Id { get; set; }
        public int Seats { get; set; }
        public bool IsReserved { get; set; }
        public int WaiterId { get; set; }
    }
}
