using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartsRepository
    {
        Cart GetCart(Guid id);
        IQueryable<Cart> GetCarts();

        Guid AddCart(Cart c);

        void DeleteCart(Guid id);
    }
}
