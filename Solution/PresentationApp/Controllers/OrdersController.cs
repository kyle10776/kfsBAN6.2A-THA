using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationApp.Models;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace PresentationApp.Controllers
{
    public class OrdersController : Controller
    {
        private IOrdersService _orderService;
        private IOrderDetailsService _orderDetailService;
        private IProductsService _prodService;
        private ICartsService _cartService;

        public OrdersController(IProductsService prodService, IOrdersService orderService, ICartsService cartsService, IOrderDetailsService orderDetailsService)
        {
            _prodService = prodService;
            _orderService = orderService;
            _cartService = cartsService;
            _orderDetailService = orderDetailsService;
        }

        [Authorize]
        [Route("Orders/Checkout")]
        public IActionResult Checkout()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string user = User.Identity.Name;
                    //var list = _cartService.GetCartForUser(user);

                    //// add order
                    //var o = new OrderViewModel()
                    //{
                    //    Email = user,
                    //    DatePlaced = DateTime.Now
                    //};

                    //o.Id = _orderService.CreateOrder(o);

                    //foreach (var i in list)
                    //{
                    //    if (i.Quantity <= i.Product.Stock)
                    //    {
                    //        // add order details
                    //        var od = new OrderDetailsViewModel()
                    //        {
                    //            ProductId = i.Product.Id,
                    //            Price = i.Product.Price,
                    //            Quantity = i.Quantity,
                    //            OrderId = o.Id
                    //        };

                    //        _orderDetailService.CreateOrderDetails(od);

                    //        // decrease stock
                    //        _prodService.DecreaseStock(i.Product.Id, i.Quantity);

                    //        // clear from cart
                    //        _cartService.DeleteFromCart(i.Id);
                    //    };
                    //}

                    _orderService.Checkout(user);

                    scope.Complete();
                }

                TempData["feedback"] = "Order successful!";
                ModelState.Clear();

            }
            catch (Exception ex)
            {
                TempData["warning"] = "Error: Order failed, please try again later";
            }

            return Redirect("../Products");
        }
    }
}
