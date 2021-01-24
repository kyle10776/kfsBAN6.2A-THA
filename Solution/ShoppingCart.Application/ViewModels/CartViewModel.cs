using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public ProductViewModel Product { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public string Email { get; set; }
    }
}
