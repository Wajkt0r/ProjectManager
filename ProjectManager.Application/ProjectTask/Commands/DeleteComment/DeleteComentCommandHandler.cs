using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager.Application.Common;
using ProjectManager.Domain.Contracts.Repositories;

namespace ProjectManager.Application.ProjectTask.Commands.DeleteCommand
{
    public class DeleteComentCommandHandler : IRequestHandler<DeleteComentCommand, CommandResult>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public DeleteComentCommandHandler(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<CommandResult> Handle(DeleteComentCommand request, CancellationToken cancellationToken)
        {
            var taskComment = await _projectTaskRepository.GetCommentById(request.CommentId);

            if (taskComment == null) return CommandResult.Failure("Task comment not found", 404);

            await _projectTaskRepository.DeleteComment(taskComment);

            return CommandResult.Success("Comment deleted successfully");

            
        }
    }
}
