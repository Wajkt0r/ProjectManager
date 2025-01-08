using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectManager.Application.ProjectRole.Queries.GetProjectRoles
{
    public class GetProjectRolesQuery : IRequest<List<ProjectRoleDto>>
    {
        public string ProjectEncodedName { get; set; } = default!;
    }
}
