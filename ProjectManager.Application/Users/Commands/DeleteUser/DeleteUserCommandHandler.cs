using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Application.ApplicationUser;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Contracts.Services;
using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandResult>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITaskManagmentService _taskManagmentService;
        private readonly ICommentsService _commentsService;
        private readonly UserManager<User> _userManager;
        private readonly IUserContext _userContext;

        public DeleteUserCommandHandler(IProjectRepository projectRepository, IUserRepository userRepository, ITaskManagmentService taskManagmentService, ICommentsService commentsService, UserManager<User> userManager, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _taskManagmentService = taskManagmentService;
            _commentsService= commentsService;
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

            await _projectRepository.DeleteAllUserProjects(userToDelete.Id);
            await _taskManagmentService.UnassignTaskForUser(userToDelete.Id);
            await _commentsService.DeleteUserComments(null, userToDelete.Id);

            await _userManager.DeleteAsync(userToDelete);

            return CommandResult.Success($"User {request.Email} deleted successfully");
        }
    }
}
