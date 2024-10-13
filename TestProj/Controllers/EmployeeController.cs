using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProj.Attributes;

namespace TestProj.Controllers
{
    [BasicAuthentication]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ActionResult Index(string searchString)
        {
            var employees = _context.Employees.Include("Department").Where(e => !e.IsDeleted);

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));
            }

            return View(employees.ToList());
        }

        public ActionResult Add()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.ProgrammingLanguages = new SelectList(_context.ProgrammingLanguages, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.ProgrammingLanguages = new SelectList(_context.ProgrammingLanguages, "Id", "Name");
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return HttpNotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.ProgrammingLanguages = new SelectList(_context.ProgrammingLanguages, "Id", "Name");
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.ProgrammingLanguages = new SelectList(_context.ProgrammingLanguages, "Id", "Name");
            return View(employee);
        }

        public ActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return HttpNotFound();

            employee.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}