using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditUserRolesCommandHandler(IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(EditUserRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return Unit.Value;
            }

            // Pobierz role użytkownika
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in request.Roles)
            {
                // Sprawdź, czy rola istnieje
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    // Jeśli rola nie istnieje, utwórz ją
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                // Sprawdź, czy użytkownik już ma tę rolę
                if (!userRoles.Contains(role))
                {
                    // Dodaj rolę użytkownikowi
                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            // Usuń użytkownikowi role, których nie ma w liście request.Roles
            foreach (var role in userRoles)
            {
                if (!request.Roles.Contains(role))
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
            }

            return Unit.Value;
        }
    }

}
