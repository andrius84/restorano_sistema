using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories;
using RestoranoSistema.Services;

namespace RestoranoSistema
{
    public class RestoranasDbContext : DbContext
    {
        public RestoranasDbContext()
        {
        }
        public RestoranasDbContext(DbContextOptions<RestoranasDbContext> options) : base(options)
        {
        }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=Restoranas;Trusted_Connection=True;TrustServerCertificate=True;");

            }
        }
    }
}
