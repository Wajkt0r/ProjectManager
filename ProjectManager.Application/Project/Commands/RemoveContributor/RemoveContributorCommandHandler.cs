using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Domain.Interfaces;

namespace ProjectManager.Application.Project.Commands.RemoveContributor
{
    public class RemoveContributorCommandHandler : IRequestHandler<RemoveContributorCommand>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveContributorCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;        
        }

        public async Task<Unit> Handle(RemoveContributorCommand request, CancellationToken cancellationToken)
        {
            await _projectRepository.RemoveContributor(request.ProjectUser);

            return Unit.Value;
        }
    }
}
