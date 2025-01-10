using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectTask.Commands.LogTime
{
    public class LogTimeCommandHandler : IRequestHandler<LogTimeCommand, CommandResult>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public LogTimeCommandHandler(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<CommandResult> Handle(LogTimeCommand request, CancellationToken cancellationToken)
        {
            var logTime = new TimeLog()
            {
                CommitMessage = request.CommitMessage,
                TimeSpent = TimeSpan.FromHours(request.TimeSpent),
                LoggedInTaskId = request.LoggedInTaskId,
                LoggedById = request.LoggedById
            };

            await _projectTaskRepository.LogTime(logTime);
            return CommandResult.Success("Logged time");
        }
    }
}
