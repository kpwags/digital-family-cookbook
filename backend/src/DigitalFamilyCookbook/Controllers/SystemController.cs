using DigitalFamilyCookbook.Handlers.Commands.System;
using DigitalFamilyCookbook.Handlers.Queries.System;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace DigitalFamilyCookbook.Controllers;

[Route("system")]
[ApiController]
[Authorize]
public class SystemController : Controller
{
    private readonly IMediator _mediatr;

    public SystemController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost("saverole")]
    public async Task<ActionResult> AddRole(AddRoleType.Command command, CancellationToken cancellationToken)
    {
        await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("updaterole")]
    public async Task<ActionResult> UpdateRole(UpdateRoleType.Command command, CancellationToken cancellationToken)
    {
        await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("getroles")]
    public async Task<IReadOnlyCollection<RoleTypeDto>> GetRoles(CancellationToken cancellationToken)
    {
        var roles = await _mediatr.Send(new GetRoleTypes.Query(), cancellationToken);

        return roles;
    }

    [HttpGet("getrolebyid")]
    public async Task<RoleTypeApiModel> GetRoleTypeById(string id, CancellationToken cancellationToken)
    {
        var role = await _mediatr.Send(new GetRoleTypeById.Query { Id = id }, cancellationToken);

        return role;
    }

    [HttpPost("deleterole")]
    public async Task<ActionResult> DeleteRole(DeleteRoleType.Command command, CancellationToken cancellationToken)
    {
        var role = await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("getsitesettings")]
    [AllowAnonymous]
    public async Task<ActionResult<SiteSettingsApiModel>> GetSiteSettings(CancellationToken cancellationToken)
    {
        var settings = await _mediatr.Send(new GetSiteSettings.Query(), cancellationToken);

        if (!settings.IsSuccessful)
        {
            return BadRequest(settings.ErrorMessage);
        }

        if (settings.Value == null)
        {
            return BadRequest("Unable to load system settings");
        }

        return settings.Value;
    }
}
