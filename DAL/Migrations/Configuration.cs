namespace DAL.Migrations
{
    using DAL.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.AppDbContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));

            // Seed a default user
            if (userManager.FindByName("admin") == null)
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    LastAction = DateTime.Now
                };
                userManager.Create(user, "Password123!");
            }

        }
    }
}
