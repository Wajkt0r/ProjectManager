using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;

namespace ProjectManager.Application.Project.Commands.AddContributor
{
    public class AddContributorCommandHandler : IRequestHandler<AddContributorCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AddContributorCommandHandler(IProjectRepository projectRepository, UserManager<User> userManager, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<CommandResult> Handle(AddContributorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserEmail == null) return CommandResult.Failure("The user's email address must be entered");

                var user = (await _userManager.FindByEmailAsync(request.UserEmail));
                if (user == null)
                {
                    return CommandResult.Failure("User not found", 404);
                }

                var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);

                if (await _projectRepository.IsUserContributor(projectId, user.Id)) return CommandResult.Failure("User is already a contributor", 409);

                var projectUser = new ProjectUser
                {
                    ProjectId = projectId,
                    UserId = user.Id
                };

                await _projectRepository.AddContributorToProject(projectUser);

                return CommandResult.Success("User added successfully");
            }
            catch (Exception ex)
            {
                return CommandResult.Failure("Unexpected error occurred", 500);
            }
            

        }
    }
}
