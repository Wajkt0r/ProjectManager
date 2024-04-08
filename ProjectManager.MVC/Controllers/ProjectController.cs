using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ProjectManager.Application.Project.Commands.CreateProject;
using ProjectManager.Application.Project.Commands.DeleteProject;
using ProjectManager.Application.Project.Commands.EditProject;
using ProjectManager.Application.Project.Queries.GetAllProjects;
using ProjectManager.Application.Project.Queries.GetProjectByEncodedName;
using ProjectManager.Application.ProjectTask.Commands.CreateProjectTask;
using ProjectManager.Infrastructure.Repositories;
using ProjectManager.MVC.Extensions;
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
            var projectsDto = await _mediator.Send(new GetAllProjectsQuery());
            return View(projectsDto);
        }
       

        [Authorize(Roles = "User")]
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

        [HttpPost]
        [Authorize]
        [Route("Project/ProjectTask")]
        public async Task<IActionResult> CreateProjectTask(CreateProjectTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Console.Out.WriteLineAsync("Tu bylismy");
            await _mediator.Send(command);

            return Ok();
        }

    }
}
