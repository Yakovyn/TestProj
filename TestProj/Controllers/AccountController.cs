using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProj.Attributes;
using BL;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using System.Threading.Tasks;
using System.Web.Security;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Threading;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace TestProj.Controllers
{
    [BasicAuthentication]
    public class AccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, AppDbContext context)
        {
            _userManager = userManager;
        }
        // Login action
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string username, string password, string returnUrl)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid credentials");
            }

            var user = await _userManager.FindAsync(username, password);
            if (user != null)
            {

                var identity = await user.GenerateUserIdentityAsync(_userManager);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                    AllowRefresh = true
                };
                HttpContext.GetOwinContext().Authentication.SignIn(authProperties, identity);

                Thread.CurrentPrincipal = new GenericPrincipal(identity, null);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Employee");
            }

            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string username, string email, string password)
        {
            var user = new User
            {
                UserName = username,
                Email = email,
                LastAction = DateTime.Now
            };
            var result = _userManager.Create(user, password);
            return RedirectToAction("Index", "Employee");
        }
    }
}