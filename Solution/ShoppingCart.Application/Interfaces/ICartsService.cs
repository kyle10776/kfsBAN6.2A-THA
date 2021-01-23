using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartsService
    {
        CartViewModel GetCart();
        void AddProduct(CartViewModel model);
        void DeleteProduct(Guid id);
    }
}
