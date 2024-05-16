using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Users;
using ProjectManager.Application.Users.Queries.GetAllUsers;
using ProjectManager.Domain.Entities;

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

        [Route("AdminPanel/Index")]
        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            return View("Index", users);
        }
    }
}
