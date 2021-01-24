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
    public class OrderDetailsService : IOrderDetailsService
    {
        private IOrderDetailsRepository _orderDetailsRepo;
        private IMapper _autoMapper;
        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepo, IMapper autoMapper)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _autoMapper = autoMapper;
        }
    }
}
