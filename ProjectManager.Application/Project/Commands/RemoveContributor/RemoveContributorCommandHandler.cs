using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;

namespace ProjectManager.Application.Project.Commands.RemoveContributor
{
    public class RemoveContributorCommandHandler : IRequestHandler<RemoveContributorCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<User> _userManager;

        public RemoveContributorCommandHandler(IProjectRepository projectRepository, UserManager<User> userManager)
        {
            _projectRepository = projectRepository; 
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RemoveContributorCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByEncodedName(request.ProjectEncodedName);
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            var projectUser = new ProjectUser()
            {
                ProjectId = project.Id,
                UserId = user.Id
            };

            await _projectRepository.RemoveContributor(projectUser);

            return Unit.Value;
        }
    }
}
