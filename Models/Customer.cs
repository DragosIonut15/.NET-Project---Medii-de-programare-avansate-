using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proiect_Nohai.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }
        [Display(Name = "Name and surname")]
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Sex { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }
        public string Disease { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
