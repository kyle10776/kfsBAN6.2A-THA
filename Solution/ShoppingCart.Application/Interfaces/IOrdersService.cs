using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IOrdersService
    {
        void Checkout(string user);
        Guid CreateOrder(OrderViewModel model);
    }
}
