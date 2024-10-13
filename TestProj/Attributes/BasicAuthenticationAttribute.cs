﻿using DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using BL;

namespace TestProj.Attributes
{

    public class BasicAuthenticationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var credentialBytes = Convert.FromBase64String(encodedCredentials);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

                if (credentials.Length == 2)
                {
                    var username = credentials[0];
                    var password = credentials[1];

                    using (var context = new AppDbContext())
                    {
                        var userManager = new UserManager<User>(new UserStore<User>(context));
                        var user = userManager.Find(username, password);

                        if (user != null)
                        {
                            // User is authenticated
                            return true;
                        }
                    }
                }
            }

            return false; // Authentication failed
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}