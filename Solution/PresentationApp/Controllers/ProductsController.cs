﻿using System;
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
    
    public class ProductsController : Controller
    {
        private IProductsService _prodService;
        private ICategoriesService _catService;
        private IWebHostEnvironment _env;
        public ProductsController(IProductsService prodService, ICategoriesService categoryService, IWebHostEnvironment env)
        {
            _prodService = prodService;
            _catService = categoryService;
            _env = env;
        }

        [Route("Products")]
        public IActionResult Index(string keyword)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    var list = _prodService.GetProducts();
                    return View(list);
                }
                else
                {
                    var list = _prodService.GetProducts(keyword);
                    return View(list);
                }
                
            }
            catch(Exception ex)
            {
                TempData["Warning"] = "Failed to load the products. please try again later";
                return RedirectToAction("Error", "Home");
            }
        }

        //public IActionResult Next()
        //{
        //    int index = 0;
        //    List<ProductViewModel> list = new List<ProductViewModel>();
        //    string page = HttpContext.Session.GetString("batchPosition");
        //    if (page == null)
        //    {
        //        index = 0;
        //        list = _prodService.GetNextProducts(5, index).ToList();
        //    }
        //    else
        //    {
        //        index = Convert.ToInt32(HttpContext.Session.GetString("batchPosition"));
        //        index += 5;
        //        list = _prodService.GetNextProducts(5, index).ToList();

        //        HttpContext.Session.SetString("batchPosition", index.ToString());
        //    }
        //    return View("Index", list);
        //}


        [Route("Products/Details")]
        public IActionResult Details(Guid id)
        {
            var p = _prodService.GetProduct(id);
            return View(p);
        }


        [HttpGet] //this will be called before loading the Create page
        [Authorize(Roles ="Admin")]
        [Route("Products/Create")]
        public IActionResult Create()
        {
            CreateModel model = new CreateModel();
            
            var list = _catService.GetCategories();
            model.Categories = list.ToList();

            return View(model);
        }

        [HttpPost] //2nd method will  be triggered when the user clicks on the submit button!
        [Authorize(Roles = "Admin")]
        [Route("Products/Create")]
        public IActionResult Create(CreateModel data)
        {
            //validation
            try
            {
                if (data.File != null)
                {
                    if(data.File.Length > 0)
                    {
                        string newFilename = @"/Images/" + Guid.NewGuid() + System.IO.Path.GetExtension(data.File.FileName);
                        string absolutePath = _env.WebRootPath;

                        using (var stream = System.IO.File.Create(absolutePath + newFilename))
                        {
                            data.File.CopyTo(stream);
                        }

                        data.Product.ImageUrl = newFilename;
                    }
                }

                _prodService.AddProduct(data.Product);

                TempData["feedback"] = "Product added successfully";
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                TempData["warning"] = "Product was not added successfully. Check your inputs";
            }
            
            var list = _catService.GetCategories();
            data.Categories = list.ToList();

            return View(data);
        }

        [Authorize(Roles = "Admin")]
        [Route("Products/Delete")]
        public IActionResult Delete(Guid id)
        {
            _prodService.DeleteProduct(id);

            TempData["feedback"] = "Product was deleted successfully";  //ViewData should be changed to TempData
           
            return RedirectToAction("Index");
        }




    }
}
