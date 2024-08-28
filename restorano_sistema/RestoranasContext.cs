using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories;
using RestoranoSistema.Services;

namespace RestoranoSistema
{
    internal class RestoranasContext : DbContext
    {
        public RestoranasContext()
        {
        }
        public RestoranasContext(DbSet<Table> tables, DbSet<Dish> dishes, DbSet<Beverage> beverages, DbSet<Order> orders, DbSet<MenuItem> menuItems)
        {
            Tables = tables;
            Dishes = dishes;
            Beverages = beverages;
            Orders = orders;
            MenuItems = menuItems;
        }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Restoranas;Trusted_Connection=True;TrustServerCertificate=True;");

            }
        }
    }
}
