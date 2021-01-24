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
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepo;
        private IMapper _autoMapper;
        public OrdersService(IOrdersRepository ordersRepo, IMapper autoMapper)
        {
            _ordersRepo = ordersRepo;
            _autoMapper = autoMapper;
        }
        public void Checkout()
        {

        }
    }
}
