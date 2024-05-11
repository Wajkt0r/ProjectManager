using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Users.Queries.GetAllUsers;

namespace ProjectManager.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("AdminPanel/Users")]
        public async Task<IActionResult> Users()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return View("Users", users);
        }
    }
}
