using MediatR;
using ProjectManager.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<CommandResult>
    {
        public string Email { get; set; } = default!;
    }
}
