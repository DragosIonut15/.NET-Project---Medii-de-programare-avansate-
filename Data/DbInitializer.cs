using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect_Nohai.Models;

namespace Proiect_Nohai.Data
{
    public class DbInitializer
    {
        public static void Initialize(PharmacyContext context)
        {
            context.Database.EnsureCreated();
            if (context.Drugs.Any())
            {
                return; // BD a fost creata anterior
            }
            var drugs = new Drug[]
            { 
                new Drug{Name="Secom Memory Blen", Manufacturer="Volary", AgeLimit="3", TypeOfDrug="Capsula", Price=Decimal.Parse("35")},
              
                new Drug{Name="Memo Plus Energizer", Manufacturer="Walmark", AgeLimit="3", TypeOfDrug="Capsula", Price=Decimal.Parse("28")},
              
                new Drug{Name="Himalaya Mentat", Manufacturer="Himalaya", AgeLimit="3", TypeOfDrug="Capsula", Price=Decimal.Parse("32")},
              
                new Drug{Name="Theraflu Max raceala si gripa", Manufacturer="GSK", AgeLimit="14", TypeOfDrug="Plic", Price=Decimal.Parse("25")},

                new Drug{Name="Nurofen Junior", Manufacturer="Reckitt Benckiser", AgeLimit="7", TypeOfDrug="Capsula", Price=Decimal.Parse("15")},

                new Drug{Name="Coldrex Maxgrip Lemon", Manufacturer="GSK", AgeLimit="14", TypeOfDrug="Plic", Price=Decimal.Parse("18")},

                new Drug{Name="Osciloccocinum", Manufacturer="Boiron", AgeLimit="7", TypeOfDrug="Pastila", Price=Decimal.Parse("16")},

                new Drug{Name="ACC 600 MG", Manufacturer="Sandoz", AgeLimit="14", TypeOfDrug="Comprimate", Price=Decimal.Parse("20")},

                new Drug{Name="Strepsils Intensive Miere si Lamaie", Manufacturer="Reckitt Benckiser", AgeLimit="7", TypeOfDrug="Tableta", Price=Decimal.Parse("22")},

                new Drug{Name="Parasinus Penta", Manufacturer="GSK", AgeLimit="7", TypeOfDrug="Comprimate", Price=Decimal.Parse("26")},

                new Drug{Name="Ibusinus 200MG", Manufacturer="Solacium Pharma", AgeLimit="7", TypeOfDrug="Comprimate", Price=Decimal.Parse("13")}

            };
            foreach (Drug b in drugs)
            {
                context.Drugs.Add(b);
            }
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer{CustomerID=1050, Name="Diaconescu Andrei", Adress="Str. Florilor, nr.23", Sex = "M", BirthDate=DateTime.Parse("1998-10-22"), Disease = "-"},

                new Customer{CustomerID=1055, Name="Ion Florin", Adress="Str. Margaretelor, nr.44", Sex = "M", BirthDate=DateTime.Parse("1988-09-03"), Disease = "Astm bronsic"},

                new Customer{CustomerID=1060, Name="Vlaicu Mihai-Toader", Adress="Str. Mihai Eminescu, bloc A, ap.2", Sex = "M", BirthDate=DateTime.Parse("1985-03-08"), Disease = "-"},

                new Customer{CustomerID=1065, Name="Florea Vasile", Adress="Str. Ses, nr.55", Sex = "M", BirthDate=DateTime.Parse("1947-04-10"), Disease = "Zona zoster"},

                new Customer{CustomerID=1070, Name="Crina Maria Ignat", Adress="Str. Teodor Mihali, nr.55", Sex = "F", BirthDate=DateTime.Parse("1993-05-12"), Disease = "-"},

                new Customer{CustomerID=1075, Name="Adam Catalin-Ioan", Adress="Str. Luminii, bloc C, ap.20", Sex = "M", BirthDate=DateTime.Parse("1994-06-06"), Disease = "Anemie"},

                new Customer{CustomerID=1080, Name="Lung Monica Loredana", Adress="Str. Tineretului, bloc D, ap.22", Sex = "M", BirthDate=DateTime.Parse("2000-07-09"), Disease = "-"}



 };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
 
                new Order{DrugID=1,CustomerID=1050, OrderDate=DateTime.Parse("2021-01-05 13:31:00 PM")},
 
                new Order{DrugID=3,CustomerID=1055,  OrderDate=DateTime.Parse("2021-01-06 12:43:00 PM")},
 
                new Order{DrugID=1,CustomerID=1060,  OrderDate=DateTime.Parse("2021-01-07 13:49:00 PM")},
 
                new Order{DrugID=2,CustomerID=1050,  OrderDate=DateTime.Parse("2021-01-03 14:11:00 PM")},
                new Order{DrugID=4,CustomerID=1055,  OrderDate=DateTime.Parse("2021-01-03 15:15:00 PM")},
                new Order{DrugID=5,CustomerID=1060,  OrderDate=DateTime.Parse("2021-01-03 16:21:00 PM")},
                new Order{DrugID=4,CustomerID=1065,  OrderDate=DateTime.Parse("2021-01-03 12:32:00 PM")},
                new Order{DrugID=5,CustomerID=1065,  OrderDate=DateTime.Parse("2021-01-03 13:45:00 PM")},
                new Order{DrugID=3,CustomerID=1050,  OrderDate=DateTime.Parse("2021-01-03 17:56:00 PM")},
                new Order{DrugID=6,CustomerID=1080,  OrderDate=DateTime.Parse("2021-01-03 19:59:00 PM")},
                new Order{DrugID=6,CustomerID=1070,  OrderDate=DateTime.Parse("2021-01-03 13:23:00 PM")},
                new Order{DrugID=7,CustomerID=1080,  OrderDate=DateTime.Parse("2021-01-03 14:11:00 PM")},
                new Order{DrugID=2,CustomerID=1075,  OrderDate=DateTime.Parse("2021-01-03 15:34:00 PM")},
                new Order{DrugID=7,CustomerID=1065,  OrderDate=DateTime.Parse("2021-01-03 12:02:00 PM")},
                new Order{DrugID=8,CustomerID=1080,  OrderDate=DateTime.Parse("2021-01-03 11:00:00 PM")},
                new Order{DrugID=6,CustomerID=1065,  OrderDate=DateTime.Parse("2021-01-03 10:44:00 PM")},
                new Order{DrugID=1,CustomerID=1060,  OrderDate=DateTime.Parse("2021-01-03 10:57:00 PM")},
                new Order{DrugID=8,CustomerID=1070,  OrderDate=DateTime.Parse("2021-01-03 20:59:00 PM")},
                new Order{DrugID=10,CustomerID=1070,  OrderDate=DateTime.Parse("2021-01-03 21:32:00 PM")},
                new Order{DrugID=9,CustomerID=1075,  OrderDate=DateTime.Parse("2021-01-03 13:21:00 PM")},
                new Order{DrugID=10,CustomerID=1060,  OrderDate=DateTime.Parse("2021-01-03 15:44:00 PM")},
                new Order{DrugID=8,CustomerID=1065,  OrderDate=DateTime.Parse("2021-01-03 15:10:00 PM")},
                new Order{DrugID=10,CustomerID=1055,  OrderDate=DateTime.Parse("2021-01-03 16:09:00 PM")},
                new Order{DrugID=1,CustomerID=1075,  OrderDate=DateTime.Parse("2021-01-03 16:02:00 PM")},
                new Order{DrugID=10,CustomerID=1080,  OrderDate=DateTime.Parse("2021-01-03 12:11:00 PM")}

            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
        }
    }
}
