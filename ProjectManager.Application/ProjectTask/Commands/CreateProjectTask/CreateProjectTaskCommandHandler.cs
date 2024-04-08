using MediatR;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommandHandler : IRequestHandler<CreateProjectTaskCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;

        public CreateProjectTaskCommandHandler(IUserContext userContext, IProjectRepository projectRepository, IProjectTaskRepository projectTaskRepository)
        {
            _userContext = userContext;
            _projectRepository = projectRepository;
            _projectTaskRepository = projectTaskRepository;
        }
        public async Task<Unit> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByEncodedName(request.ProjectEncodedName);

            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && (project.CreatedById == user.Id || user.IsInRole("Admin"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            var projectTask = new Domain.Entities.ProjectTask()
            {
                Name = request.Name,
                Description = request.Description,
                Deadline = request.Deadline,
                ProjectId = project.Id
            };

            await _projectTaskRepository.Create(projectTask);

            return Unit.Value;
        }
    }
}
