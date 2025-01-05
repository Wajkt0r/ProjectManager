using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Domain.Contracts.Repositories;

namespace ProjectManager.Application.Project.Queries.GetProjectIdByEncodedName
{
    public class GetProjectIdByEncodedNameQueryHandler : IRequestHandler<GetProjectIdByEncodedNameQuery, int>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectIdByEncodedNameQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<int> Handle(GetProjectIdByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);

            return projectId;
        }
    }
}
