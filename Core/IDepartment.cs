using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IDepartment
    {
         int Id { get; set; }
         string Name { get; set; }
         int Floor { get; set; }
    }
}
