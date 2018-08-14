using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Krustan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Krustan.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;

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
        public IActionResult Index([FromQuery] string info, [FromQuery] bool search, [FromQuery]string txtName)
        {
            if(search)
            {
                var dogs = service.GetDogsByName(txtName).Result;
                ViewBag.Info = $"{dogs.ToList().Count} dogs found.";
                return View(dogs);
            }
            if (info != null)
            {
                ViewBag.Info = info;
            }
            return View(service.GetAll().Result);
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string Error)
        {
            if(Error != null)
            {
                ViewBag.Error = Error;
            }
            ViewBag.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            string Name, string Sex, float Weight, float Height, string Owner, IFormFile DogPicture,
            string Description, int Age)
        {
            if (Name != null && Sex != null && Owner != null && DogPicture != null && Description != null)
            {
                Dog dog = new Dog() {
                    Name = Name,
                    Sex = Sex,
                    Weight = Weight,
                    Height = Height,
                    DogPicture = DogPicture.FileName,
                    Description = Description,
                    Age = Age,
                    OwnerId = Owner
                };
                // full path to file in temp location
                var filePath = Path.GetTempFileName();
                var stream = new FileStream(filePath, FileMode.Create);
                await DogPicture.CopyToAsync(stream);

                stream.Close();
                var result = await service.UploadPictureToS3Bucket(filePath, Path.GetExtension(DogPicture.FileName));
                if(result != null)
                {
                    //Set AWS S3 url to DogPicture property
                    dog.DogPicture = result;

                    var addedDog = await service.AddDog(dog, Owner);
                    if (addedDog != null)
                        return RedirectToAction("Index", new { info = "Dog was added sucessfully!" });
                }
            }
            string _error = "Please, complete the form below to successfully add a dog.";
            return RedirectToAction("Create", new { Error = _error });
        }

        [HttpGet]
        public IActionResult Search([FromQuery]string byName)
        {
            if(byName != null)
            {
                //var _dogs = service.GetDogsByName(byName).Result;
                return RedirectToAction("Index", new { search = true, txtName = byName });
            }
            return RedirectToAction("Index", new { search = false });
        }
    }
}