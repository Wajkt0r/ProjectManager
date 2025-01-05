using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.ProjectTask.Queries.GetProjectTasks;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectTask.Queries.GetUserProjectTasks
{
    public class GetUserProjectTasksQueryHandler : IRequestHandler<GetUserProjectTasksQuery, IEnumerable<ProjectTaskDto>>
    {
        private readonly IUserContext _userContext;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserProjectTasksQueryHandler(IUserContext userContext, IProjectTaskRepository projectTaskRepository, UserManager<User> userManager, IMapper mapper)
        {
            _userContext = userContext;
            _projectTaskRepository = projectTaskRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectTaskDto>> Handle(GetUserProjectTasksQuery request, CancellationToken cancellationToken)
        {
            var userId = (await _userManager.FindByEmailAsync(request.UserEmail)).Id;

            if (userId == null)
            {
                return new List<ProjectTaskDto>();
            }

            var projectTasks = await _projectTaskRepository.GetUserProjectTasks(request.ProjectEncodedName, userId);
            var projectTasksDto = _mapper.Map<IEnumerable<ProjectTaskDto>>(projectTasks);

            return projectTasksDto;
        }
    }
}
