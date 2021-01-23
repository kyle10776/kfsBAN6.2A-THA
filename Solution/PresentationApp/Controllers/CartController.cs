using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationApp.Models;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace PresentationApp.Controllers
{
    public class CartController : Controller
    {
        private ICartsService _cartService;
        private IProductsService _prodService;

        public CartController(IProductsService prodService, ICartsService cartService)
        {
            _prodService = prodService;
            _cartService = cartService;
        }

        [Authorize]
        public IActionResult Index()
        {
            //get all the items in cart for the logged in user
            //to get the logged in user: string user = User.Identity.Name;

            string user = User.Identity.Name;

            var list = _cartService.GetCartForUser(user);
            return View("Index", list);
        }

 
        [HttpPost][Authorize]
        public IActionResult AddtoCart(Guid productId, int qty)
        {
            //validation
            try
            {
                string user = User.Identity.Name;

                //1. get product from db
                var product = _prodService.GetProduct(productId);

                //2. check if qty is available in stock
                if (product.Stock >= qty)
                {
                    //3. add to cart
                    var cart = new CartViewModel()
                    {
                        Email = user,
                        Product = product,
                        Quantity = qty
                    };

                    _cartService.AddToCart(cart);
                }

                TempData["feedback"] = "Product added successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not added to cart";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveFromCart(Guid cartId)
        {         
            try
            {
                _cartService.DeleteFromCart(cartId);
                
                TempData["feedback"] = "Product removed successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not removed from cart";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout()
        {
            try
            {
                // 1. get user
                // 2. get all items in cart for user
                // 3. create order

                    // 4. for each item, check stock
                    // 5. add order detail to order
                    // 6. remove from cart.

                TempData["feedback"] = "Product removed successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not removed from cart";
            }

            return RedirectToAction("Index");
        }
    }
}
