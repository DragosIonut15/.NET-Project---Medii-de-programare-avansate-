using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proiect_Nohai.Models
{
    public class Drug
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string AgeLimit{ get; set; }
        public string TypeOfDrug { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
