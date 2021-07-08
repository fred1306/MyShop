using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAcess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext() :base ("DefaultConnection")
        {

        }

        //tell the DataContext Which models to use
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
