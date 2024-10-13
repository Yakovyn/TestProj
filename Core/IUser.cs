using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        DateTime LastAction { get; set; }
    }
}
