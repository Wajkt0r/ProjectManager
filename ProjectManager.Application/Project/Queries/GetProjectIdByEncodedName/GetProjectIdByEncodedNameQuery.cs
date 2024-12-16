using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectManager.Application.Project.Queries.GetProjectIdByEncodedName
{
    public class GetProjectIdByEncodedNameQuery : IRequest<int>
    {
        public string ProjectEncodedName { get; set; } = default;
    }
}
