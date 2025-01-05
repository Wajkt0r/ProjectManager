using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers();

            if (users == null)
            {
                return null;
            }

            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = (List<string>)roles
                };
                usersDto.Add(userDto);

            }

            return usersDto;
        }
    }
}
