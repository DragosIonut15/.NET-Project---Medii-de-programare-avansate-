using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect_Nohai.Models;

namespace Proiect_Nohai.Data
{
    public class PharmacyContext:DbContext
    {
        public PharmacyContext(DbContextOptions<PharmacyContext> options) :base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Drug> Drugs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Drug>().ToTable("Drug");
        }
    }

}
