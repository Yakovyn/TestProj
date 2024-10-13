using DAL.Models;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TestProj.Filters
{
    public class UserActionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = filterContext.HttpContext.User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                _ = Task.Run(() =>
                {
                    try
                    {
                        using (var context = new AppDbContext())
                        {
                            var user = context.Users.Find(userId);
                            if (user != null)
                            {
                                user.LastAction = DateTime.Now;
                                context.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }).ConfigureAwait(false);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}