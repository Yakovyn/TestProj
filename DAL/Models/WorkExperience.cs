using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WorkExperience : IWorkExperience
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
