using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Project.Commands.CreateProject;
using ProjectManager.Application.Project.Queries.GetAllProjects;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
