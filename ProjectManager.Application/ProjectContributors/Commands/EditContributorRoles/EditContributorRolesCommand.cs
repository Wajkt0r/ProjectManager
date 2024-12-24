using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;

namespace ProjectManager.Application.ProjectContributors.Commands.EditContributorRoles
{
    public class EditContributorRolesCommand : IRequest<CommandResult>
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public List<string> SelectedRoles { get; set; } = new();
    }
}
