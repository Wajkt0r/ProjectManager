using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;

namespace ProjectManager.Application.ProjectTask.Commands.LogTime
{
    public class LogTimeCommand : LogTimeDto, IRequest<CommandResult>
    {
    }
}
