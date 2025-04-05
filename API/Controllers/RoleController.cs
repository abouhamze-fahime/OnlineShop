using Application.CQRS.RoleCommandQuery.Command;
using Application.CQRS.RoleCommandQuery.Query;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers; 

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper _mapper;
    public RoleController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
       var result = await mediator.Send(new GetRoleQuery());
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> AddNewRole(CreateRoleCommand newRole)
    {
        var result = await mediator.Send(newRole);
        return Ok(result);
    }

}
