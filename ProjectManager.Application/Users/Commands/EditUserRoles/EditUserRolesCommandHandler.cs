using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Users.Commands.EditUserRoles
{
    public class EditUserRolesCommandHandler : IRequestHandler<EditUserRolesCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public EditUserRolesCommandHandler(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(EditUserRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return Unit.Value;  
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in request.Roles)
            {
                if (role in userRoles) {
                    // Jak jest to skip fora
                }

                // powyzszy if sie nie wykona wiec dodajemy role
                var result = await _userManager.AddToRoleAsync(user, role); // cos z tym mozna pokmninic
            }

            
        }
    }
}
