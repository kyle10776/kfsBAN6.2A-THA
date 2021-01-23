using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Linq;

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

        public void AddToCart(CartViewModel item)
        {
            _cartsRepo.AddCart(_autoMapper.Map<Cart>(item));
        }

        public void DeleteFromCart(Guid id)
        {
            _cartsRepo.DeleteCart(id);
        }

        public IQueryable<CartViewModel> GetCartForUser(string email)
        {
            return _cartsRepo.GetCarts().Where(p => p.Email.Equals(email))
                .ProjectTo<CartViewModel>(_autoMapper.ConfigurationProvider);
        }

    }
}
