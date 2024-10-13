using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DAL
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<AppDbContext>   
    {

        protected override void Seed(AppDbContext context)
        {
            // Seed initial data here if necessary
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(new List<Department>
            {
                new Department { Name = "IT", Floor = 1 },
                new Department { Name = "HR", Floor = 2 },
                new Department { Name = "BA", Floor = 2 }
            });
            }

            if (!context.ProgrammingLanguages.Any())
            {
                context.ProgrammingLanguages.AddRange(new List<ProgrammingLanguage>
            {
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "JavaScript" },
                new ProgrammingLanguage { Name = "Java" }
            });
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}