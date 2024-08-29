using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoranoSistema.Entities
{
    public abstract class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public List<Order>? Orders { get; set; }

    }
}
