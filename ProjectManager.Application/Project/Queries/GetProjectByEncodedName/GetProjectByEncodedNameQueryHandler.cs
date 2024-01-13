using AutoMapper;
using MediatR;
using ProjectManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Queries.GetProjectByEncodedName
{
    public class GetProjectByEncodedNameQueryHandler : IRequestHandler<GetProjectByEncodedNameQuery, ProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectByEncodedNameQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByEncodedName(request.EncodedName);
            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }
    }
}
