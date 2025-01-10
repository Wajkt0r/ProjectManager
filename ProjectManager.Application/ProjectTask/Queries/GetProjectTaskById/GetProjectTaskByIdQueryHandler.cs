using AutoMapper;
using MediatR;
using ProjectManager.Domain.Contracts.Repositories;
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

            var taskComments = await _projectTaskRepository.GetTasksComments(request.Id);
            var taskTimeLogs = await _projectTaskRepository.GetTasksTimeLogs(request.Id);

            var commentsDto = taskComments.Select(comment => new CommentDto
            {
                Id = comment.Id,
                Message = comment.Comment,
                CreatedAt = comment.CommentTime,
                CreatedByEmail = comment.CreatedBy.Email,
                IsLogTime = false,
                TimeSpent = null
            });

            var timeLogsDto = taskTimeLogs.Select(timeLog => new CommentDto
            {
                Id = timeLog.Id,
                Message = timeLog.CommitMessage,
                CreatedAt = timeLog.LoggedAt,
                CreatedByEmail = timeLog.LoggedBy.Email,
                IsLogTime = true,
                TimeSpent = timeLog.TimeSpent
            });

            projectTaskDto.TaskComments = commentsDto.Concat(timeLogsDto)
                                                .OrderByDescending(c => c.CreatedAt)
                                                .ToList();

            return projectTaskDto;
        }
    }
}
