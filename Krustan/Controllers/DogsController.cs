using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Krustan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Krustan.Services;

namespace Krustan.Controllers
{

    [Authorize]
    public class DogsController : Controller
    {
        private readonly IDogService service;

        public DogsController(IDogService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string info)
        {
            if(info != null)
            {
                ViewBag.Info = info;
            }
            return View(service.GetAll().Result);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Dog dog)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { info = "Dog was added sucessfully!" });
            }
            ViewBag.Error = "Please, complete the form below to successfully add a dog.";
            return View();
        }
    }
}