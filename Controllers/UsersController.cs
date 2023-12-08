using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ProductsValidation.Models;
using ProductsValidation.Services;

namespace ProductsValidation.Controllers
{
    public class UsersController : Controller
    {
        private List<User> users;

        public UsersController(Data data)
        {
            users = data.Users;
        }
        
        public IActionResult Index(string id)
        {
            return View("Index", users);
        }
        [HttpGet]
        public IActionResult Create(int id, string name, string email, string role)
        {
            var newUser = new User
            {
                Id = id,
                Name = name,
                Email = email,
                Role = role
            };

            return View(newUser);
        }

        [HttpPost]
        public IActionResult Create(User newUser)
        {

            users.Add(newUser);


            return RedirectToAction("Index");
        }
    }
}
