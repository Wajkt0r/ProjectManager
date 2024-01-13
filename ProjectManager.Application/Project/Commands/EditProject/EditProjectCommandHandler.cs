using MediatR;
using ProjectManager.Domain.Interfaces;
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

        public EditProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<Unit> Handle(EditProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByEncodedName(request.EncodedName!);

            project.Description = request.Description;
            project.FinishDate = request.FinishDate;

            await _projectRepository.Commit();
            return Unit.Value;
            
        }
    }
}
