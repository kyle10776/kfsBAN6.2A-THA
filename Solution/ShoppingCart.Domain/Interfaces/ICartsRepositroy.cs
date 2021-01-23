using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartsRepositroy
    {
        Cart GetCart(Guid id);

        Cart AddProduct(Product p);

        Guid AddCart(Cart c);

        void DeleteCart(Guid id);
    }
}
