using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee : IEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
        public static Employee Map(IEmployee employee)
        {
            var dalTransaction = new Employee();
            return MapValues(dalTransaction, employee);
        }
        public static Employee MapValues(Employee dalEmployeeForEditing, IEmployee employee)
        {
            dalEmployeeForEditing.FirstName = employee.FirstName;
            dalEmployeeForEditing.LastName = employee.LastName;
            dalEmployeeForEditing.Id = employee.Id;
            dalEmployeeForEditing.Age = employee.Age;
            dalEmployeeForEditing.Gender = employee.Gender;
            dalEmployeeForEditing.DepartmentId = employee.DepartmentId;
            dalEmployeeForEditing.IsDeleted = employee.IsDeleted;
            return dalEmployeeForEditing;
        }
    }
}
