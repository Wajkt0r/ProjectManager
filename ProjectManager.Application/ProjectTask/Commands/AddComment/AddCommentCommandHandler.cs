using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.ProjectTask.Commands.AddComment
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, CommandResult>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public AddCommentCommandHandler(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }
        public async Task<CommandResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            if (request.Comment.Length == 0) return CommandResult.Failure("Comment cannot be empty");

            TaskComment taskComment = new TaskComment()
            {
                Comment = request.Comment,
                CommentTime = DateTime.Now,
                CreatedById = request.CreatedById,
                ProjectTaskId = request.ProjectTaskId
            };

            await _projectTaskRepository.AddComment(taskComment);

            return CommandResult.Success("Added new comment");
        }
    }
}
