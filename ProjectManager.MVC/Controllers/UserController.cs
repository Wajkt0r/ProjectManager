using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManager.Application.ProjectTask.Commands.DeleteProjectTask;
using ProjectManager.Application.Users;
using ProjectManager.Application.Users.Commands.DeleteUser;
using ProjectManager.Application.Users.Commands.EditUserRoles;
using ProjectManager.Application.Users.Queries.GetAllUsers;
using ProjectManager.Domain.Entities;
using ProjectManager.MVC.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectManager.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(IMediator mediator, IMapper mapper, UserManager<User> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles="Admin")]
        [Route("AdminPanel/Index")]
        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            return View("Index", users);
        }

        [HttpPost]
        [Route("AdminPanel/User/{userEmail}/Delete")]
        public async Task<IActionResult> Delete([FromRoute]string userEmail)
        {
            DeleteUserCommand command = new DeleteUserCommand { Email = userEmail };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            this.SetNotification("error", result.Message);
            return Ok();
        }

        [HttpPost]
        [Route("AdminPanel/User/{userEmail}/Roles")]
        public async Task<IActionResult> EditRoles([FromRoute]string userEmail,[FromBody]List<string> roles)
        {
            EditUserRolesCommand command = new EditUserRolesCommand { Email = userEmail, Roles = roles };

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("success", $"{userEmail} roles has been updated");
            return Ok();
        }
    }
}
