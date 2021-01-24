using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        ShoppingCartDbContext _context;
        public OrderDetailsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public OrderDetail GetOrderDetails(Guid id)
        {
            return _context.OrderDetails.SingleOrDefault(x => x.Id == id);
        }

        public Guid CreateOrderDetails(OrderDetail od)
        {
            _context.OrderDetails.Add(od);
            _context.SaveChanges();

            return od.Id;
        }
    }
}
