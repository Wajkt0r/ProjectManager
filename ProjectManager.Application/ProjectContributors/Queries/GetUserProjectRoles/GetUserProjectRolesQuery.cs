using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectManager.Application.ProjectContributors.Queries.GetUserProjectRoles
{
    public class GetUserProjectRolesQuery : IRequest<List<string>>
    {
        public string ProjectEncodedName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
