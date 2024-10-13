using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Department : IDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
    }
}
