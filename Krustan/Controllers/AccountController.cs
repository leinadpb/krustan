using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Krustan.ViewModels;
using System.Security.Claims;
using Krustan.Models;
using Krustan.Services;
namespace Krustan.Controllers
{
    public class AccountController : Controller
    {
        public IConfiguration Configuration { get; }

        private readonly IDogService DogService;
        private readonly IUserService UserService;

        public AccountController(IConfiguration config, IDogService _dogService, IUserService _userService)
        {
            this.Configuration = config;
            this.DogService = _dogService;
            this.UserService = _userService;
        }

        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in the
                // **Allowed Logout URLs** settings for the app.
                RedirectUri = Url.Action("Index", "Home")

            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult Profile([FromQuery] string Info, [FromQuery] bool Success)
        {
            string uniqueId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            AppUser user = UserService.GetUserByUniqueId(uniqueId).Result;
            if (user == null)
            {
                ViewBag.UserNotFound = true;

                string email = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                string profileImg = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;
                string nickname = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value;
                string name = "";
                string current_email = User.Claims.FirstOrDefault(c => c.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                if (!email.Contains('@') && !email.Contains('.'))
                {
                    if(current_email != null)
                    {
                        name = email;
                        email = current_email;
                    }
                    else
                    {
                        name = email;
                        email = nickname + "@gmail.com";
                    }
                }
                var myDogs = DogService.GetDogsByUser(uniqueId).Result;

                return View(new UserProfileViewModel
                {
                    UniqueId = uniqueId,
                    Email = email,
                    ProfileImage = profileImg,
                    Nickname = nickname,
                    Name = name,
                    MyDogs = myDogs,
                    SavedDogs = null
                });
            }
            else
            {
                ViewBag.UserNotFound = false;
                if(Info != null)
                {
                    ViewBag.Info = Info;
                    ViewBag.Success = Success;
                }
                List<Dog> thisUserDogs = new List<Dog>();
                if(user.Dogs != null)
                {
                    thisUserDogs = user.Dogs;
                }
                return View(new UserProfileViewModel {
                    UniqueId = user.UniqueId,
                    Email = user.Email,
                    ProfileImage = user.ProfileImage,
                    Nickname = user.Nickname,
                    Name = user.Name,
                    MyDogs = thisUserDogs,
                    SavedDogs = new List<Dog>()
                });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult CompleteRegistration([FromQuery]string uniqueId, [FromQuery]string email,
            [FromQuery] string name, [FromQuery] string nickname, [FromQuery] string profilePicture)
        {
            AppUser user = new AppUser();
            user.UniqueId = uniqueId;
            if (email != null)
                user.Email = email;
            if (name != null)
                user.Name = name;
            if (nickname != null)
                user.Nickname = nickname;
            if (profilePicture != null)
                user.ProfileImage = profilePicture;

            return View(user);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CompleteRegistration(AppUser user)
        {
            if (ModelState.IsValid)
            {

                AppUser addedUser = UserService.AddUser(user.UniqueId, user.Name, user.Email, user.Description,
                    user.Birthdate, user.Nickname, user.ProfileImage).Result;

                string _info = "Thanks! Your profile is now complete.";
                bool _success = true;
                return RedirectToAction("Profile", new { Info = _info, Success = _success });
            }
            string info = "Model is invalid...";
            bool success = false;
            return RedirectToAction("Profile", new { Info = info, Success = success });
        }
    }
}