using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation
{
    public class AddEmployeeViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public List<IProgrammingLanguage> Languages { get; set; }
        public List<IDepartment> Departments { get; set; }
    }
}
