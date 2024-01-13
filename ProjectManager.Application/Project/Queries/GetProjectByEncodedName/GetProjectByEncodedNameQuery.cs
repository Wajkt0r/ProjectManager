using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetProjectByEncodedName
{
    public class GetProjectByEncodedNameQuery : IRequest<ProjectDto>
    {
        public string EncodedName { get; set; }

        public GetProjectByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
