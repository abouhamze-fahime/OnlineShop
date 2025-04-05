using Application.CQRS.ApplicationPermissionCommadnQuery.Command;
using Application.CQRS.UserRoleCommandQuery.Command;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public UserRoleController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddNewPermission(CreateUserRoleCommand  createUserRole)
        {
            var result = await mediator.Send(createUserRole);
            return Ok(result);

        }
    }
}
