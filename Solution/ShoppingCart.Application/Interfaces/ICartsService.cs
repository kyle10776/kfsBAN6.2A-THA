using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartsService
    {
        void AddToCart(CartViewModel model);
        void DeleteFromCart(Guid id);
        IQueryable<CartViewModel> GetCartForUser(string email);
    }
}
