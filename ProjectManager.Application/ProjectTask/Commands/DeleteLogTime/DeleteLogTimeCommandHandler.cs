using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteLogTime
{
    public class DeleteLogTimeCommandHandler : IRequestHandler<DeleteLogTimeCommand, CommandResult>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public DeleteLogTimeCommandHandler(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }
        public async Task<CommandResult> Handle(DeleteLogTimeCommand request, CancellationToken cancellationToken)
        {
            var timeLog = await _projectTaskRepository.GetTimeLogById(request.LogTimeId);

            if (timeLog == null) return CommandResult.Failure("Time Log hasn't been found");

            await _projectTaskRepository.DeleteTimeLog(timeLog);
            return CommandResult.Success("Deleted time log");
        }
    }
}
