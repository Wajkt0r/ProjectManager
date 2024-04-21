using AutoMapper;
using MediatR;
using ProjectManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Queries.GetProjectTaskById
{
    public class GetProjectTaskByIdQueryHandler : IRequestHandler<GetProjectTaskByIdQuery, ProjectTaskDto>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;
        public GetProjectTaskByIdQueryHandler(IMapper mapper, IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTaskDto> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var projectTask = await _projectTaskRepository.GetById(request.Id);
             
            var projectTaskDto = _mapper.Map<ProjectTaskDto>(projectTask);

            return projectTaskDto;
        }
    }
}
