using MediatR;
using ProjectManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.EditProjectTask
{
    public class EditProjectTaskCommandHandler : IRequestHandler<EditProjectTaskCommand>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        public EditProjectTaskCommandHandler(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }
        public async Task<Unit> Handle(EditProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var projectTask = await _projectTaskRepository.GetById(request.Id);

            if (projectTask == null)
            {
                return Unit.Value;
            }

            projectTask.Name = request.Name;
            projectTask.Description = request.Description;
            projectTask.Deadline = request.Deadline;

            await _projectTaskRepository.Commit();

            return Unit.Value;
        }
    }
}
