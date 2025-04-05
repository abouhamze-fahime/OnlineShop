using Application.CQRS.ApplicationPermissionCommadnQuery.Command;
using Application.CQRS.ApplicationPermissionCommadnQuery.Query;
using Application.CQRS.RoleCommandQuery.Query;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers; 

[Route("api/[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper _mapper;
    public PermissionController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await mediator.Send(new GetAllPermissionQuery());
        return Ok(permissions);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewPermission(CreatePermissionCommand newPermission)
    {
        var result = await mediator.Send(newPermission);
        return Ok(result);  

    }
}
