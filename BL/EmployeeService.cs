using Core;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Presentation;

namespace BL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IEmployee> GetEmployees(string searchString)
        {
            var employees = _context.Employees.Include(e => e.WorkExperiences)
                                               .Include(e => e.Department)
                                               .Where(e => !e.IsDeleted);
            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));
            }

            return employees.ToList();
        }
        public IEnumerable<EmployeeViewModel> GetEmployeeVMs(string searchString)
        {
            var employees = _context.Employees.Include(e => e.WorkExperiences.Select(we => we.ProgrammingLanguage))
                                               .Include(e => e.Department)
                                               .Where(e => !e.IsDeleted);
            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));
            }
            return employees.ToList().Select(employee =>
                new EmployeeViewModel(employee, employee.WorkExperiences.Select(we => we.ProgrammingLanguage), employee.Department));
        }
        public List<string> GetEmployeeNames(string term)
        {
            return _context.Employees
                           .Where(e => e.FirstName.ToLower().StartsWith(term.ToLower()))
                           .Select(e => e.FirstName).Take(100)
                           .ToList();
        }

        public void AddEmployee(IEmployee employee, IEnumerable<int> languagesIds = null)
        {
            var dalEmployee = employee is Employee
                    ? employee as Employee
                    : Employee.Map(employee);
            _context.Employees.Add(dalEmployee);
            if (languagesIds != null && languagesIds.Any())
            {
                var workExperiences = languagesIds.Select(x => new WorkExperience() { EmployeeId = employee.Id, ProgrammingLanguageId = x });
                _context.WorkExperiences.AddRange(workExperiences);
            }
            _context.SaveChanges();
        }

        public IEmployee GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }
        public EmployeeViewModel GetEmployeeVmById(int id)
        {
            var employee = _context.Employees.Include(e => e.WorkExperiences).Include(e => e.Department).FirstOrDefault(x => x.Id.Equals(id)) ?? new Employee();
            return new EmployeeViewModel(employee, employee.WorkExperiences.Select(we => we.ProgrammingLanguage), employee.Department);
        }
        public void UpdateEmployee(IEmployee employee, IEnumerable<int> languagesIds = null)
        {
            var dalEmployee = _context.Employees.Include(e => e.WorkExperiences).Include(e => e.Department).FirstOrDefault(x => x.Id.Equals(employee.Id));
            if (dalEmployee != null)
            {
                Employee.MapValues(dalEmployee, employee);
                if (languagesIds != null)
                {
                    var dalExperiences = dalEmployee.WorkExperiences;
                    var experiencesToRemove = dalExperiences.Where(x => !languagesIds.Contains(x.ProgrammingLanguageId));
                    var idsToAdd = languagesIds.Where(x => !dalExperiences.Select(d=>d.ProgrammingLanguageId).Contains(x));
                    var workExperiences = idsToAdd.Select(x => new WorkExperience() { EmployeeId = employee.Id, ProgrammingLanguageId = x });
                    _context.WorkExperiences.AddRange(workExperiences);
                    _context.WorkExperiences.RemoveRange(experiencesToRemove);
                }
                _context.SaveChanges();
            }
        }

        public void DeleteEmployee(int id)
        {
            var employee = GetEmployeeById(id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                _context.SaveChanges();
            }
        }

        public IEnumerable<IDepartment> GetDepartments()
        {
            return _context.Departments.ToList();
        }

        public IEnumerable<IProgrammingLanguage> GetProgrammingLanguages()
        {
            return _context.ProgrammingLanguages.ToList();
        }
    }
}

