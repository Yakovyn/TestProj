using BL;
using Core;
using Core.Presentation;
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

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public ActionResult Index(string searchString)
        {
            var employees = _employeeService.GetEmployeeVMs(searchString);
            return View(employees);
        }

        public JsonResult GetEmployeeNames(string term)
        {
            var result = _employeeService.GetEmployeeNames(term);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            var vm = new AddEmployeeViewModel()
            {
                Departments = _employeeService.GetDepartments().ToList(),
                Languages = _employeeService.GetProgrammingLanguages().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.AddEmployee(employee, employee.LanguagesIds);
                return RedirectToAction("Index");
            }

            var vm = new AddEmployeeViewModel()
            {
                Employee = employee,
                Departments = _employeeService.GetDepartments().ToList(),
                Languages = _employeeService.GetProgrammingLanguages().ToList()
            };
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            var employee = _employeeService.GetEmployeeVmById(id);
            if (employee == null) return HttpNotFound();

            var vm = new AddEmployeeViewModel()
            {
                Employee = employee,
                Departments = _employeeService.GetDepartments().ToList(),
                Languages = _employeeService.GetProgrammingLanguages().ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(employee, employee.LanguagesIds ?? new List<int>());
                return RedirectToAction("Index");
            }
            var vm = new AddEmployeeViewModel()
            {
                Employee = employee,
                Departments = _employeeService.GetDepartments().ToList(),
                Languages = _employeeService.GetProgrammingLanguages().ToList()
            };
            return View(employee);
        }

        public ActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}