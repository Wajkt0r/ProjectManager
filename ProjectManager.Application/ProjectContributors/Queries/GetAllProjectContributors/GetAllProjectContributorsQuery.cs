using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Users;

namespace ProjectManager.Application.ProjectContributors.Queries.GetAllProjectContributors
{
    public class GetAllProjectContributorsQuery : IRequest<IEnumerable<UserDto>>
    {
        public string ProjectEncodedName { get; set; } = default!;

        public GetAllProjectContributorsQuery(string encodedName)
        {
            ProjectEncodedName = encodedName;
        }
    }
}
