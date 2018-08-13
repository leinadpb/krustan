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

namespace Krustan.Controllers
{
    public class AccountController : Controller
    {
        public IConfiguration Configuration { get; }

        public AccountController(IConfiguration config)
        {
            this.Configuration = config;
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
        public IActionResult Userinfo()
        {
            //Get authorization code
            String auth_code = @User.Claims.ToArray()[0].ToString();
            int c = 0;
            foreach(char ch in auth_code)
            {
                if (ch.Equals('|'))
                {
                    break;
                }
                c++;
            }
            string code = auth_code.Substring(c+1);

            var client = new RestClient($"https://{Configuration["Auth0:Domain"]}/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"grant_type\":\"authorization_code\",\"client_id\": \"" + Configuration["Auth0:ClientId"] + "\",\"client_secret\": \"" + Configuration["Auth0:ClientSecret"] + "\",\"code\": \"" + code + "\",\"redirect_uri\": \"https://localhost:44378/\"}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            ViewBag.Response = response.Content;

            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {

            string email = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            string uniqueId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string profileImg = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

            string nickname = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value;

            string name = "Not specified yet...";

            string current_email = User.Claims.FirstOrDefault(c => c.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (!email.Contains('@') && !email.Contains('.'))
            {
                name = email;
                email = current_email;
            }

            string asd = "asd";

            return View(new UserProfileViewModel {
                UniqueId = uniqueId,
                Email = email,
                ProfileImage = profileImg,
                Nickname = nickname,
                Name = name
            });
        }
    }
}