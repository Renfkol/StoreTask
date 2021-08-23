using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Domain.Entities;
using System.Data.Entity;

namespace Store.Domain.EF
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext():base("StoreContext")
        {
        }

        static ProductDbContext()
        {
            Database.SetInitializer<ProductDbContext>(new ProductDbInitializer());
        }

        public DbSet<Product> Products { get; set; }
    }
}
