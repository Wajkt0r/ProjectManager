using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Users.Commands.EditUserRoles
{
    public class EditUserRolesCommand : IRequest
    {
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
