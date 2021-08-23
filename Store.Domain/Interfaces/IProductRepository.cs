using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Domain.Entities;


namespace Store.Domain.Interfaces
{
    public interface IProductRepository 
    {
        IEnumerable<Product> Products { get;}
        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
