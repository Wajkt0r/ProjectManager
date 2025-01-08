using AutoMapper;
using Humanizer.Localisation.DateToOrdinalWords;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ProjectManager.Application.Project.Commands.CreateProject;
using ProjectManager.Application.Project.Commands.DeleteProject;
using ProjectManager.Application.Project.Commands.EditProject;
using ProjectManager.Application.Project.Queries.GetAllUserProjects;
using ProjectManager.Application.Project.Queries.GetProjectByEncodedName;
using ProjectManager.Application.ProjectRole.Commands.CreateProjectRole;
using ProjectManager.Application.ProjectRole.Commands.RemoveProjectRole;
using ProjectManager.Application.ProjectRole.Queries.GetProjectRoles;
using ProjectManager.Application.ProjectTask.Queries.GetProjectTasks;
using ProjectManager.Application.ProjectTask.Queries.GetUserProjectTasks;
using ProjectManager.MVC.Extensions;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectManager.MVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var projectsDto = await _mediator.Send(new GetAllUserProjectsQuery());
            return View(projectsDto);
        }


        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Project {command.Name} successfully created");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/Edit")]
        public async Task<IActionResult> Edit(string projectEncodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(projectEncodedName));

            if (!projectDto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditProjectCommand project = _mapper.Map<EditProjectCommand>(projectDto);
            return View(project);
        }

        [HttpPost]
        [Route("Project/Edit")]
        public async Task<IActionResult> UpdateProject(EditProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/Details")]
        public async Task<IActionResult> Details(string projectEncodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(projectEncodedName));
            return View(projectDto);
        }

        public async Task<IActionResult> Delete(string projectEncodedName)
        {
            await _mediator.Send(new DeleteProjectCommand(projectEncodedName));

            this.SetNotification("error", $"Project {projectEncodedName} has been deleted");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/Tasks")]
        public async Task<IActionResult> Tasks(string projectEncodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(projectEncodedName));
            return View("Tasks", projectDto);
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/ProjectRoles")]
        public async Task<IActionResult> ProjectRoles(string projectEncodedName)
        {
            var projectRoles = await _mediator.Send(new GetProjectRolesQuery() { ProjectEncodedName = projectEncodedName});
            return Ok(projectRoles);
        }

        [HttpPost]
        [Route("Project/AddProjectRole")]
        public async Task<IActionResult> AddProjectRole([FromBody] CreateProjectRoleCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("Project/RemoveProjectRole")]
        public async Task<IActionResult> RemoveProjectRole([FromBody] RemoveProjectRoleCommand command)
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
