using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.Domain.Entities;
using System.Threading.Tasks;

namespace Store.Domain.Interfaces
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
