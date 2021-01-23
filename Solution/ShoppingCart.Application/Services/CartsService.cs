using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartsService : ICartsService
    {
        private ICartsRepository _cartsRepo;
        private IMapper _autoMapper;
        public CartsService(ICartsRepository cartsRepo, IMapper autoMapper)
        {
            _cartsRepo = cartsRepo;
            _autoMapper = autoMapper;
        }

        public void AddProduct(CartViewModel model)
        {
            _cartsRepo.AddCart(_autoMapper.Map<Cart>(model));
        }

        public void DeleteProduct(Guid id)
        {
            _cartsRepo.DeleteCart(id);
        }
        /*
        public CartViewModel GetCart()
        {
            var p = _cartsRepo.GetCart(id);
            if (p == null) return null;
            else
            {
                var result = _autoMapper.Map<CartViewModel>(p);
                return result;
            }
        }*/
    }
}
