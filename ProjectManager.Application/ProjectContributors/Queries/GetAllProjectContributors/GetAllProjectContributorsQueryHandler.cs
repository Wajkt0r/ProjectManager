using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.Users;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectContributors.Queries.GetAllProjectContributors
{
    public class GetAllProjectContributorsQueryHandler : IRequestHandler<GetAllProjectContributorsQuery, IEnumerable<UserDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectContributorsRepository _contributorsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public GetAllProjectContributorsQueryHandler(IProjectRepository projectRepository, IProjectContributorsRepository contributorsRepository, IMapper mapper, UserManager<User> userManager)
        {
            _projectRepository = projectRepository;
            _contributorsRepository = contributorsRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IEnumerable<UserDto>> Handle(GetAllProjectContributorsQuery request, CancellationToken cancellationToken)
        {
            var projectId = await _projectRepository.GetProjectId(request.ProjectEncodedName);

            var projectContributors = await _contributorsRepository.GetProjectContributors(projectId);

            var projectContributorsDto = new List<UserDto>();

            foreach (var contributor in projectContributors)
            {
                var user = _userManager.Users.Where(u => u.Id == contributor.UserId).FirstOrDefault();
                if (user != null)
                {
                    var userRoles = await _contributorsRepository.GetUserProjectRoles(projectId, user.Id);
                    var contributorDto = new UserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Roles = userRoles
                    };
                    projectContributorsDto.Add(contributorDto);
                }
            }

            return projectContributorsDto;

        }
    }
}
