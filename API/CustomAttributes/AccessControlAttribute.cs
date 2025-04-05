using Application.CQRS.ApplicationPermissionCommadnQuery.Query;
using Application.CQRS.RoleCommandQuery.Query;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;

namespace API.CustomAttributes;

public class AccessControlAttribute : ActionFilterAttribute
{
  
    public string Permission { get; set; }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = Guid.Parse( context.HttpContext.User.Claims.FirstOrDefault(u => u.Type == "userId").Value);

        var permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();


        if (!   await permissionService.CheckPermission(userId, Permission))
        {
            context.Result = new BadRequestObjectResult("No Access");
        }
        else
        {
            await base.OnActionExecutionAsync(context, next);
        }
      
    }
 
}
