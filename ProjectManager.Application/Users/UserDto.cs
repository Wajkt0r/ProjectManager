using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Users
{
    public class UserDto
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
