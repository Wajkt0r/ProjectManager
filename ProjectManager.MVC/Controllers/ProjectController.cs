using AutoMapper;
using Humanizer.Localisation.DateToOrdinalWords;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ProjectManager.Application.Project.Commands.AddContributor;
using ProjectManager.Application.Project.Commands.CreateProject;
using ProjectManager.Application.Project.Commands.DeleteProject;
using ProjectManager.Application.Project.Commands.EditContributorRoles;
using ProjectManager.Application.Project.Commands.EditProject;
using ProjectManager.Application.Project.Commands.RemoveContributor;
using ProjectManager.Application.Project.Queries.GetAllProjectContributors;
using ProjectManager.Application.Project.Queries.GetAllProjects;
using ProjectManager.Application.Project.Queries.GetAllUserProjects;
using ProjectManager.Application.Project.Queries.GetContributorRoles;
using ProjectManager.Application.Project.Queries.GetProjectByEncodedName;
using ProjectManager.Application.Project.Queries.GetProjectIdByEncodedName;
using ProjectManager.Application.Project.Queries.GetUserProjectRoles;
using ProjectManager.Application.ProjectTask.Commands.CreateProjectTask;
using ProjectManager.Application.ProjectTask.Commands.DeleteProjectTask;
using ProjectManager.Application.ProjectTask.Queries.GetProjectTasks;
using ProjectManager.Application.Users;
using ProjectManager.Application.Users.Queries.GetUserByEmail.GetUserByEmail;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.Repositories;
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

        [Route("Project/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(encodedName));

            if (!projectDto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditProjectCommand project = _mapper.Map<EditProjectCommand>(projectDto);
            return View(project);
        }

        [HttpPost]
        [Route("Project/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("Project/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(encodedName));
            return View(projectDto);
        }

        public async Task<IActionResult> Delete(string encodedName)
        {
            await _mediator.Send(new DeleteProjectCommand(encodedName));

            this.SetNotification("error", $"Project {encodedName} has been deleted");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Project/{encodedName}/GetTasks")] // Trzeba poprawic nie moze byc takiej metody tutaj
        public async Task<IActionResult> GetProjectTasks(string encodedName)
        {
            var data = await _mediator.Send(new GetProjectTasksQuery() { ProjectEncodedName = encodedName });
            return Ok(data);
        }

        [HttpGet]
        [Route("Project/{encodedName}/Tasks")]
        public async Task<IActionResult> Tasks(string encodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(encodedName));
            return View("Tasks", projectDto);
        }

        [HttpGet]
        [Route("Project/{encodedName}/GetContributors")]
        public async Task<IActionResult> GetProjectContributors(string encodedName)
        {
            var contributors = await _mediator.Send(new GetAllProjectContributorsQuery(encodedName));
            return Ok(contributors);
        }

        [HttpPost]
        [Route("Project/Contributors")]
        public async Task<IActionResult> AddContributor(AddContributorCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("Project/{encodedName}/Contributors/{userEmail}/Remove")]
        public async Task<IActionResult> RemoveContributor(string encodedName, string userEmail)
        {
            await _mediator.Send(new RemoveContributorCommand() { ProjectEncodedName = encodedName, UserEmail = userEmail });

            return Ok();
        }

        [HttpGet]
        [Route("Project/{encodedName}/Contributors")]
        public async Task<IActionResult> Contributors(string encodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(encodedName));

            return View("Contributors", projectDto);
        }

        [HttpGet]
        [Route("Project/{encodedName}/Contributors/{userEmail}/ProjectRoles")]
        public async Task<IActionResult> GetUserProjectRoles(string encodedName, string userEmail)
        {
            var userRoles = await _mediator.Send(new GetUserProjectRolesQuery() { ProjectEncodedName = encodedName, UserEmail = userEmail });

            return Ok(userRoles);
        }

        [HttpGet]
        [Route("Project/{encodedName}/Contributors/{userEmail}/EditRolesForm")]
        public async Task<IActionResult> EditRolesForm(string encodedName, string userEmail)
        {
            var model = await _mediator.Send(new GetContributorRolesQuery() { ProjectEncodedName = encodedName, UserEmail = userEmail });
            return PartialView("_EditRolesForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoles([FromBody]EditContributorRolesCommand command)
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
