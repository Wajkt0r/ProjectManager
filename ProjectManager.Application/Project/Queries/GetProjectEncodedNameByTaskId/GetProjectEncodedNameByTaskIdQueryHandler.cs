using MediatR;
using ProjectManager.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetProjectEncodedNameByTaskId
{
    public class GetProjectEncodedNameByTaskIdQueryHandler : IRequestHandler<GetProjectEncodedNameByTaskIdQuery, string?>
    {
        private readonly IProjectRepository _projectRepository;
        public GetProjectEncodedNameByTaskIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<string?> Handle(GetProjectEncodedNameByTaskIdQuery request, CancellationToken cancellationToken)
        {
            var projectEncodedName = await _projectRepository.GetProjectEncodedNameByTaskId(request.Id);

            if (projectEncodedName == null)
            {
                return null;
            }

            return projectEncodedName;
        }
    }
}
