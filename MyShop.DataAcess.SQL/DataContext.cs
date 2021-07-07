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
        protected DataContext() :base ("DefaultConnection")
        {

        }
    }
}
