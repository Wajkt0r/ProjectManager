using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Project.Queries.GetProjectByEncodedName;
using ProjectManager.Application.Project.Queries.GetProjectEncodedNameByTaskId;
using ProjectManager.Application.ProjectTask.Commands.CreateProjectTask;
using ProjectManager.Application.ProjectTask.Commands.DeleteProjectTask;
using ProjectManager.Application.ProjectTask.Commands.EditProjectTask;
using ProjectManager.Application.ProjectTask.Queries.GetProjectTaskById;
using ProjectManager.Application.ProjectTask.Queries.GetProjectTasks;
using ProjectManager.MVC.Extensions;

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
        [Route("Project/Tasks")]
        public async Task<IActionResult> CreateProjectTask(CreateProjectTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);

            return Ok();
        }

        [Route("ProjectTask/{taskId}/Edit")]
        public async Task<IActionResult> EditProjectTask(int taskId)
        {
            var task = await _mediator.Send(new GetProjectTaskByIdQuery(taskId));
            var command = new EditProjectTaskCommand
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Deadline = task.Deadline
            };
            return View("EditProjectTask", command);
            
        }

        [HttpPost]
        [Route("ProjectTask/{taskId}/Edit")]
        public async Task<IActionResult> EditProjectTask(int taskId, EditProjectTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            this.SetNotification("success", $"Task edited");
            var projectEncodedName = await _mediator.Send(new GetProjectEncodedNameByTaskIdQuery(taskId));
            return RedirectToAction("Tasks", "Project", new { encodedName = projectEncodedName });
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
    }
}
