using Microsoft.EntityFrameworkCore;
using PizzaSalesChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Infrastructure.Database
{
    public class PizzaDatabaseContext : DbContext
    {
        public PizzaDatabaseContext(DbContextOptions<PizzaDatabaseContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaType> PizzaType { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(od => od.OrderDetails)
                .WithOne(o => o.Order)
                .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
