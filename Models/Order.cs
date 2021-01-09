using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Nohai.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int DrugID { get; set; }
        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }
        public Drug Drug { get; set; }
    }
}
