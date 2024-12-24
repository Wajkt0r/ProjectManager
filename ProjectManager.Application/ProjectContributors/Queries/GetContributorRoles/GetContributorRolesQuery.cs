using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectManager.Application.ProjectContributors.Queries.GetContributorRoles
{
    public class GetContributorRolesQuery : IRequest<ContributorRolesDto>
    {
        public string ProjectEncodedName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
