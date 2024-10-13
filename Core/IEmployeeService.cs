using Core.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IEmployeeService
    {
        IEnumerable<IEmployee> GetEmployees(string searchString);
        IEnumerable<EmployeeViewModel> GetEmployeeVMs(string searchString);
        List<string> GetEmployeeNames(string term);
        void AddEmployee(IEmployee employee, IEnumerable<int> languagesIds = null);
        IEmployee GetEmployeeById(int id);
        EmployeeViewModel GetEmployeeVmById(int id);
        void UpdateEmployee(IEmployee employee, IEnumerable<int> languagesIds = null);
        void DeleteEmployee(int id);
        IEnumerable<IDepartment> GetDepartments();
        IEnumerable<IProgrammingLanguage> GetProgrammingLanguages();
    }
}
