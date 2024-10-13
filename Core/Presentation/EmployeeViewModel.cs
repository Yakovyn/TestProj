using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation
{
    public class EmployeeViewModel : IEmployee
    {
        public EmployeeViewModel(IEmployee employee, IEnumerable<IProgrammingLanguage> languages, IDepartment department) {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Id = employee.Id;
            Age = employee.Age;
            Gender = employee.Gender;
            DepartmentId = employee.DepartmentId;
            IsDeleted = employee.IsDeleted;
            LanguagesIds = languages.Select(x => x.Id);
            Languages = languages;
            Department = department;
        }
        public EmployeeViewModel() { }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<int> LanguagesIds { get; set; }
        public IDepartment Department { get; set; }
        public IEnumerable<IProgrammingLanguage> Languages { get; set; }
        public bool IsDeleted { get; set; }
    }
}
