using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ProductsValidation.Models;
using ProductsValidation.Services;
using static ProductsValidation.Models.Product;

namespace ProductsValidation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly List<Product> myProducts;

        public ProductsController(Data data)
        {
            myProducts = data.Products;
        }

        public IActionResult Index(int filterId, string filtername)
        {
            return View(myProducts);
        }

        public IActionResult View(int id)
        {
            Product prod = myProducts.Find(prod => prod.Id == id);
            if (prod != null)
            {
                return View(prod);
            }

            return View("NotExists");
        }

		#region Edit action
        [HttpGet] 
		public IActionResult Edit(int id)
        {
            Product prod = myProducts.Find(prod => prod.Id == id);
            if (prod != null)
            {
                GetCategories();
                return View(prod);
            }
            return View("NotExists");
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                myProducts[myProducts.FindIndex(prod => prod.Id == product.Id)] = product;
                return View("View", product);
            }
            GetCategories();
            return View(product);
        }

        public IActionResult EditPricesByCategory(Product.Category selectedCategory)
        {
            if (selectedCategory != null)
            {
                List<Product> filteredProducts = myProducts.Where(p => p.Type == selectedCategory).ToList();
                return View(filteredProducts);
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult EditPricesByCategory(List<Product> products)
        {
            if (ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    var index = myProducts.FindIndex(p => p.Id == product.Id);
                    if (index != -1)
                    {
                        myProducts[index].Price = product.Price;
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(products);
            }
        }
		#endregion

		#region Create Action
		[HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                myProducts.Add(product);
                return View("View", product);
            }
            GetCategories();
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
            GetCategories();
            return View(new Product() { Id = myProducts.Last().Id + 1 });
        }
		#endregion

		public IActionResult Delete(int id)
        {
            myProducts.Remove(myProducts.Find(product => product.Id == id));
            return View("Index", myProducts);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void GetCategories()
        {
            Category[] categories = (Category[])Enum.GetValues(typeof(Category));
            var categoriesWithNames = from value in categories
                                      select new { Type = value, Name = value.ToString() };
            ViewBag.Types = new SelectList(categoriesWithNames, "Type", "Name");
        }
    }
}
