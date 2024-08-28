using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public int Seats { get; set; }
        public bool IsOccupied { get; set; }
    }
}
