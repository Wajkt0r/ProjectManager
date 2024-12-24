using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectContributors.Commands.AddContributor
{
    public class AddContributorCommand : IRequest<CommandResult>
    {
        public string ProjectEncodedName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
