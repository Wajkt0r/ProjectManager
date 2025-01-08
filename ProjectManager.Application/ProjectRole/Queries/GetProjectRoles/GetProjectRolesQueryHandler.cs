using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProjectManager.Domain.Contracts.Repositories;

namespace ProjectManager.Application.ProjectRole.Queries.GetProjectRoles
{
    public class GetProjectRolesQueryHandler : IRequestHandler<GetProjectRolesQuery, List<ProjectRoleDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectRolesQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectRoleDto>> Handle(GetProjectRolesQuery request, CancellationToken cancellationToken)
        {
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);

            var projectRoles = await _projectRepository.GetProjectRoles(projectId);

            var projectRolesDto = _mapper.Map<List<ProjectRoleDto>>(projectRoles);

            return projectRolesDto;
        }        
    }
}
