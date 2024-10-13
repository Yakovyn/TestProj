using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public AppDbContext() : base("DbConnectionString")
        {
            Database.SetInitializer(new AppDbInitializer());

        }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
