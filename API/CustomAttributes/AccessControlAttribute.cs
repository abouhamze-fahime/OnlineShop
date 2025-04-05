using Application.CQRS.ApplicationPermissionCommadnQuery.Query;
using Application.CQRS.RoleCommandQuery.Query;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;

namespace API.CustomAttributes;

public class AccessControlAttribute : ActionFilterAttribute
{

    public string Permission { get; set; }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userId = context.HttpContext.User.Claims.FirstOrDefault(u => u.Type == "userId").Value;
       // var roles = GetRoleQuery(userId);
       // var permission = GetAllPermissionQuery(roles);
        base.OnActionExecuting(context);
    }
}
