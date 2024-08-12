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

            //modelBuilder.Entity<Person>()
            //    .HasOne(p => p.User)
            //    .WithOne(p => p.Person)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Person>()
            //.HasMany(f => f.Address)
            //.WithOne(f => f.Person)
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
