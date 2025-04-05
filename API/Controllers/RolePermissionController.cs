using Application.CQRS.ApplicationPermissionCommadnQuery.Command;
using Application.CQRS.RolePermissionCommandQuery.Command;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolePermissionController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper _mapper;
    public RolePermissionController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddNewPermission(CreateRolePermissionCommand  createRolePermission)
    {
        var result = await mediator.Send(createRolePermission);
        return Ok(result);

    }
}
