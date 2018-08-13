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
        public IActionResult Index()
        {
            return View(service.GetAll().Result);
        }
    }
}