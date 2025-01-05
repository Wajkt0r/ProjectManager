using MediatR;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Project.Commands.EditProject
{
    public class EditProjectCommandHandler : IRequestHandler<EditProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;

        public EditProjectCommandHandler(IProjectRepository projectRepository, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (project.CreatedById == user.Id || user.IsInRole("Admin"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            project.Description = request.Description;
            project.FinishDate = request.FinishDate;

            await _projectRepository.Commit();

            return Unit.Value;
        }
    }
}
