using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteCommand
{
    public class DeleteComentCommand : IRequest<CommandResult>
    {
        public int CommentId { get; set; }
    }
}
