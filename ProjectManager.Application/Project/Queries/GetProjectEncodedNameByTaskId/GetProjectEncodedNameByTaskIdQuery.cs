using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetProjectEncodedNameByTaskId
{
    public class GetProjectEncodedNameByTaskIdQuery : IRequest<string?>
    {
        public int Id { get; set; }

        public GetProjectEncodedNameByTaskIdQuery(int id)
        {
            Id = id;
        }
    }
}
