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

namespace TestProj.Controllers
{
    [BasicAuthentication]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ActionResult Add()
        {
            return View();
        }
        private readonly ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        // Login action
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid credentials");
            }

            var user = await _userManager.FindAsync(username, password);
            if (user != null)
            {
                // Authentication successful
                var identity = await user.GenerateUserIdentityAsync(_userManager);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, 
                    FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), false, JsonConvert.SerializeObject(identity.Claims.Select(c => new { c.Type, c.Value }).ToList()))))
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddMinutes(30)
                };
                Response.Cookies.Add(authCookie);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        // Logout action
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            // Clear the authentication cookie
            FormsAuthentication.SignOut();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        [HttpPost]
        public ActionResult Add(User user)
        {
            if (ModelState.IsValid)
            {
                user.LastAction = DateTime.Now;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}