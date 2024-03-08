using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Commands.DeleteProject
{
    public class DeleteProjectCommand : ProjectDto, IRequest
    {
        public DeleteProjectCommand(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
