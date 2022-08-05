using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigitalFamilyCookbook.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private List<string> _roles;

    public AuthorizeAttribute()
    {
        _roles = new List<string>();
    }

    public AuthorizeAttribute(string roles)
    {
        _roles = roles.Split(",").Select(r => r.Trim()).ToList();
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var user = (UserAccountApiModel?)context.HttpContext.Items["User"];

        if (user is null)
        {
            string? tokenError = context.HttpContext.Items["TokenError"] as string;

            if (tokenError is not null && tokenError == "EXPIRED")
            {
                context.Result = new JsonResult(new { message = "TOKEN_EXPIRED" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                context.Result = new JsonResult(new { message = "UNAUTHORIZED" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        if (user is not null && _roles.Count > 0)
        {
            var userRoles = user.Roles.Select(r => r.Name.ToLower());
            var roles = userRoles.Intersect(_roles.Select(r => r.ToLower()));

            if (!roles.Any())
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}