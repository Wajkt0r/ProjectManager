using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteLogTime
{
    public class DeleteLogTimeCommand : IRequest<CommandResult>
    {
        public int LogTimeId { get; set; }
    }
}
