using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Diplom_TEST.Models;

namespace WPF_Diplom_TEST
{
    class AppDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Link> Product_Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        }
    }
}
