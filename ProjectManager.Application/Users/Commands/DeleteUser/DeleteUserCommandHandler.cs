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

namespace ProjectManager.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserContext _userContext;

        public DeleteUserCommandHandler(IUserRepository userRepository, UserManager<User> userManager, IUserContext userContext)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return Unit.Value;
            }

            var adminUser = _userContext.GetCurrentUser();

            if (!adminUser.IsInRole("Admin")) 
            {
                //Wyświetl powiadomienia, że nie ma roli admina i nie może zmienić ról użytkownika
                return Unit.Value;
            }
            await _userManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
