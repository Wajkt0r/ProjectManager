using AutoMapper;
using MediatR;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Queries.GetProjectTasks
{
    public class GetProjectTasksQueryHandler : IRequestHandler<GetProjectTasksQuery, IEnumerable<ProjectTaskDto>>
    {
        private readonly IUserContext _userContext;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public GetProjectTasksQueryHandler(IUserContext userContext, IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _userContext = userContext;
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProjectTaskDto>> Handle(GetProjectTasksQuery request, CancellationToken cancellationToken)
        {
            var projectTasks = await _projectTaskRepository.GetAllByEncodedName(request.ProjectEncodedName);
            var projectTasksDto = _mapper.Map<IEnumerable<ProjectTaskDto>>(projectTasks);

            return projectTasksDto;
        }
    }
}
