using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class OrderDetailsViewModel
    {
        public  ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        //need to check if these are correct
    }
}
