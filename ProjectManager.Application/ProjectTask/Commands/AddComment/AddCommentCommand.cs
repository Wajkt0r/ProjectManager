using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectTask.Commands.AddComment
{
    public class AddCommentCommand : TaskComment, IRequest<CommandResult>
    {        
    }
}
