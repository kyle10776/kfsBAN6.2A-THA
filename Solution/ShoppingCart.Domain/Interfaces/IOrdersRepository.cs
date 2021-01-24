using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        Order GetOrder(Guid id);

        Guid CreateOrder(Order o);

        void Checkout(string user);
    }
}
