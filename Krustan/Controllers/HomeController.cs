using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Krustan.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index([FromQuery]string code)
        {
            if(code != null)
            {
                ViewBag.Code = code;
            }
            return View();
        }

        [Authorize]
        public IActionResult PrivateView()
        {
            return View();
        }
    }
}
