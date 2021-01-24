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
        [Route("Cart")]
        public IActionResult Index()
        {
            //get all the items in cart for the logged in user
            //to get the logged in user: string user = User.Identity.Name;

            string user = User.Identity.Name;

            var list = _cartService.GetCartForUser(user);
            return View("Index", list);
        }

 
        [Authorize]
        [Route("Cart/AddToCart")]
        public IActionResult AddToCart(Guid productId, int qty)
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
                        ProductId = product.Id,
                        Quantity = qty
                    };

                    _cartService.AddToCart(cart);
                }
                TempData["feedback"] = "Product added successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Error: Product could not be added to cart, please try again later";
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [Route("Cart/RemoveFromCart")]
        public IActionResult RemoveFromCart(Guid Id)
        {         
            try
            {
                _cartService.DeleteFromCart(Id);
                
                TempData["feedback"] = "Product removed successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Error: Product could not be removed from cart, please try again later";
            }

            return RedirectToAction("Index");
        }
    }
}
