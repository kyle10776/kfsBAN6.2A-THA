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
    public class CartsRepository : ICartsRepository
    {
        ShoppingCartDbContext _context;
        public CartsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public Cart GetCart(Guid id)
        {
            return _context.Carts.SingleOrDefault(x => x.Id == id);
        }

        public Guid AddCart(Cart c)
        {
            _context.Carts.Add(c);
            _context.SaveChanges();

            return c.Id;
        }

        public void DeleteCart(Guid id)
        {
            Cart c = GetCart(id);
            _context.Carts.Remove(c);
            _context.SaveChanges();
        }

        public IQueryable<Cart> GetCarts()
        {
            return _context.Carts.Include(x => x.Product);
        }
    }
}
