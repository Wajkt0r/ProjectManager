using MediatR;
using Microsoft.AspNetCore.Identity;
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

        public DeleteUserCommandHandler(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return Unit.Value;
            }

            await _userManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
