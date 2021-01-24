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
    public class OrdersRepository : IOrdersRepository
    {
        ShoppingCartDbContext _context;
        public OrdersRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public Order GetOrder(Guid id)
        {
            return _context.Orders.SingleOrDefault(x => x.Id == id);
        }
        public Guid CreateOrder(Order o)
        {
            _context.Orders.Add(o);
            _context.SaveChanges();

            return o.Id;
        }

        public void Checkout(string user)
        {
            var list = _context.Carts.Include(x => x.Product).Where(x => x.Email.Equals(user));

            // add order
            var o = new Order()
            {
                Email = user,
                DatePlaced = DateTime.Now
            };

            _context.Orders.Add(o);

            foreach (var i in list)
            {
                if (i.Quantity <= i.Product.Stock)
                {
                    // add order details
                    var od = new OrderDetail()
                    {
                        Product = i.Product,
                        Price = i.Product.Price,
                        Quantity = i.Quantity,
                        Order = o
                    };

                    _context.OrderDetails.Add(od);

                    // decrease stock
                    i.Product.Stock = i.Product.Stock - i.Quantity;

                    // clear from cart
                    _context.Carts.Remove(i);
                    _context.SaveChanges();
                };
            }
        }
    }
}
