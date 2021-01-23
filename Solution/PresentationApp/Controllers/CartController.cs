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

            return View();
        }

 

        [HttpPost][Authorize]
        public IActionResult AddtoCart(Guid productId, int qty)
        {
            string user = User.Identity.Name;

            //validation
            try
            {
                //1. get product from db
                //2. check if qty is available in stock
                //3. add to cart
                //4. get cart from db
                _prodService.AddProduct(data.Product);

                TempData["feedback"] = "Product added successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not added to cart";
            }

            var list = _catService.GetCategories();
            data.Categories = list.ToList();

            return View(data);

            return RedirectToAction("Index");
        }
    }
}
