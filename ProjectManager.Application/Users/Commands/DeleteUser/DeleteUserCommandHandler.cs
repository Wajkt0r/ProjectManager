using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandResult>
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
        public async Task<CommandResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetUserByEmail(request.Email);

            if (userToDelete == null)
            {
                return CommandResult.Failure("User not found", 404);
            }

            var currentUser = _userContext.GetCurrentUser();

            if (!currentUser.IsInRole("Admin")) 
            {
                return CommandResult.Failure("You do not have permission to perform this action", 403);
            }
            await _userManager.DeleteAsync(userToDelete);

            return CommandResult.Success($"User {request.Email} deleted successfully");
        }
    }
}
