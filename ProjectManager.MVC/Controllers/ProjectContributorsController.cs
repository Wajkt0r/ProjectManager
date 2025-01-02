using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.ProjectContributors.Commands.RemoveContributor;
using ProjectManager.Application.ProjectContributors.Queries.GetAllProjectContributors;
using ProjectManager.Application.ProjectContributors.Queries.GetContributorRoles;
using ProjectManager.Application.Project.Queries.GetProjectByEncodedName;
using ProjectManager.Application.ProjectContributors.Queries.GetUserProjectRoles;
using ProjectManager.Application.ProjectContributors.Commands.AddContributor;
using ProjectManager.Application.ProjectContributors.Commands.EditContributorRoles;

namespace ProjectManager.MVC.Controllers
{
    public class ProjectContributorsController : Controller
    {
        private readonly IMediator _mediator;

        public ProjectContributorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/ProjectContributors/Get")]
        public async Task<IActionResult> GetProjectContributors(string projectEncodedName)
        {
            var contributors = await _mediator.Send(new GetAllProjectContributorsQuery(projectEncodedName));
            return Ok(contributors);
        }

        [HttpPost]        
        [Route("Project/ProjectContributors/Add")]
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
        [Route("Project/{projectEncodedName}/ProjectContributors/{userEmail}/Remove")]
        public async Task<IActionResult> RemoveContributor(string projectEncodedName, string userEmail)
        {
            await _mediator.Send(new RemoveContributorCommand() { ProjectEncodedName = projectEncodedName, UserEmail = userEmail });

            return Ok();
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/ProjectContributors/")]
        public async Task<IActionResult> Contributors(string projectEncodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(projectEncodedName));

            return View("Contributors", projectDto);
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/ProjectContributors/{userEmail}/ProjectRoles")]
        public async Task<IActionResult> GetUserProjectRoles(string projectEncodedName, string userEmail)
        {
            var userRoles = await _mediator.Send(new GetUserProjectRolesQuery() { ProjectEncodedName = projectEncodedName, UserEmail = userEmail });

            return Ok(userRoles);
        }

        [HttpGet]
        [Route("Project/{projectEncodedName}/ProjectContributors/{userEmail}/EditRolesForm")]
        public async Task<IActionResult> EditRolesForm(string projectEncodedName, string userEmail)
        {
            var model = await _mediator.Send(new GetContributorRolesQuery() { ProjectEncodedName = projectEncodedName, UserEmail = userEmail });
            return PartialView("_EditRolesForm", model);
        }

        [HttpPost]
        [Route("Project/ProjectContributors/UpdateRoles")]
        public async Task<IActionResult> UpdateRoles([FromBody] EditContributorRolesCommand command)
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
