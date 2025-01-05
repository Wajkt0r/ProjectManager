using MediatR;
using ProjectManager.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteProjectTask
{
    public class DeleteProjectTaskCommandHandler : IRequestHandler<DeleteProjectTaskCommand>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        public DeleteProjectTaskCommandHandler(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository  = projectTaskRepository;
        }
        public async Task<Unit> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var projectTask = await _projectTaskRepository.GetById(request.Id);

            if (projectTask == null)
            {
                return Unit.Value;
            }

            await _projectTaskRepository.DeleteTask(projectTask);

            return Unit.Value;
        }
    }
}
