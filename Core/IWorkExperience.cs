using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IWorkExperience
    {
        int Id { get; set; }
        int EmployeeId { get; set; }
        int ProgrammingLanguageId { get; set; }
    }
}
