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
        [Route("ProjectContributors/{encodedName}/Get")]
        public async Task<IActionResult> GetProjectContributors(string encodedName)
        {
            var contributors = await _mediator.Send(new GetAllProjectContributorsQuery(encodedName));
            return Ok(contributors);
        }

        [HttpPost]        
        [Route("ProjectContributors/Add")]
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
        [Route("ProjectContributors/{userEmail}/Project/{projectEncodedName}/Remove")]
        public async Task<IActionResult> RemoveContributor(string projectEncodedName, string userEmail)
        {
            await _mediator.Send(new RemoveContributorCommand() { ProjectEncodedName = projectEncodedName, UserEmail = userEmail });

            return Ok();
        }

        [HttpGet]
        [Route("ProjectContributors/Project/{projectEncodedName}")]
        public async Task<IActionResult> Contributors(string projectEncodedName)
        {
            var projectDto = await _mediator.Send(new GetProjectByEncodedNameQuery(projectEncodedName));

            return View("Contributors", projectDto);
        }

        [HttpGet]
        [Route("ProjectContributors/{userEmail}/Project/{projectEncodedName}/ProjectRoles")]
        public async Task<IActionResult> GetUserProjectRoles(string projectEncodedName, string userEmail)
        {
            var userRoles = await _mediator.Send(new GetUserProjectRolesQuery() { ProjectEncodedName = projectEncodedName, UserEmail = userEmail });

            return Ok(userRoles);
        }

        [HttpGet]
        [Route("ProjectContributors/{userEmail}/Project/{projectEncodedName}/EditRolesForm")]
        public async Task<IActionResult> EditRolesForm(string projectEncodedName, string userEmail)
        {
            var model = await _mediator.Send(new GetContributorRolesQuery() { ProjectEncodedName = projectEncodedName, UserEmail = userEmail });
            return PartialView("_EditRolesForm", model);
        }

        [HttpPost]
        [Route("ProjectContributors/UpdateRoles")]
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
