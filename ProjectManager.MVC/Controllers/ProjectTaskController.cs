using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Project.Queries.GetProjectByEncodedName;
using ProjectManager.Application.Project.Queries.GetProjectEncodedNameByTaskId;
using ProjectManager.Application.ProjectContributors.Queries.GetAllProjectContributors;
using ProjectManager.Application.ProjectTask.Commands.AddComment;
using ProjectManager.Application.ProjectTask.Commands.CreateProjectTask;
using ProjectManager.Application.ProjectTask.Commands.DeleteProjectTask;
using ProjectManager.Application.ProjectTask.Commands.EditProjectTask;
using ProjectManager.Application.ProjectTask.Commands.DeleteCommand;
using ProjectManager.Application.ProjectTask.Queries.GetProjectTaskById;
using ProjectManager.Application.ProjectTask.Queries.GetProjectTasks;
using ProjectManager.Application.ProjectTask.Queries.GetUserProjectTasks;
using ProjectManager.MVC.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ProjectManager.Application.ProjectTask.Commands.LogTime;
using ProjectManager.Application.ProjectTask.Commands.DeleteLogTime;

namespace ProjectManager.MVC.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectTaskController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("Project/Tasks/Create")]
        public async Task<IActionResult> CreateProjectTask(CreateProjectTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);

            return Ok();
        }

        [Route("Project/{projectEncodedName}/ProjectTask/{taskId}/Edit")]
        public async Task<IActionResult> EditProjectTask(string projectEncodedName, int taskId, bool isEditable)
        {
            var task = await _mediator.Send(new GetProjectTaskByIdQuery(taskId));
            var projectContributors = await _mediator.Send(new GetAllProjectContributorsQuery(projectEncodedName));
            var command = new EditProjectTaskCommand
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                TaskProgressStatus = task.TaskProgressStatus,
                AssignedUserEmail = task.AssignedUserEmail,
                Deadline = task.Deadline,
                ProjectContributors = projectContributors,
                IsEditable = isEditable
            };
            return View("EditProjectTask", command);
            
        }

        [HttpPost]
        [Route("ProjectTask/UpdateProjectTask")]
        public async Task<IActionResult> UpdateProjectTask(EditProjectTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProjectTask", command);
            }
            await _mediator.Send(command);
            this.SetNotification("success", $"Task edited");
            var projectEncodedName = await _mediator.Send(new GetProjectEncodedNameByTaskIdQuery(command.Id));
            return RedirectToAction("Details", new { projectEncodedName = projectEncodedName, taskId = command.Id, isEditable = command.IsEditable });
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/Tasks/{taskId}/Details")]
        public async Task<IActionResult> Details(int taskId, string projectEncodedName, bool isEditable)
        {
            var task = await _mediator.Send(new GetProjectTaskByIdQuery(taskId));            

            ViewData["ProjectEncodedName"] = projectEncodedName;
            ViewData["IsEditable"] = isEditable;

            return View(task);
        }

        [HttpPost]
        [Route("ProjectTask/{id}/Delete")]
        public async Task<IActionResult> DeleteTask([FromRoute]int id)
        {
            DeleteProjectTaskCommand command = new DeleteProjectTaskCommand { Id = id };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/Tasks/GetTasks")]
        public async Task<IActionResult> GetProjectTasks(string projectEncodedName)
        {
            var data = await _mediator.Send(new GetProjectTasksQuery() { ProjectEncodedName = projectEncodedName });
            return Ok(data);
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/Tasks/GetTasks/{userEmail}")]
        public async Task<IActionResult> GetUserProjectTasks(string projectEncodedName, string userEmail)
        {
            var data = await _mediator.Send(new GetUserProjectTasksQuery() { ProjectEncodedName = projectEncodedName, UserEmail = userEmail });
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody]AddCommentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEntity(int entityId, string entityType)
        {
            Application.Common.CommandResult result;
            if (entityType == "comment")
            {
                result = await _mediator.Send(new DeleteComentCommand() { CommentId = entityId });
            } else if (entityType == "logtime")
            {
                result = await _mediator.Send(new DeleteLogTimeCommand() { LogTimeId = entityId });
            } else
            {
                return BadRequest("Invalid entity type");
            }
            return result.IsSuccess ? Ok(result) : StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> LogTime([FromBody]LogTimeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }
    }
}
