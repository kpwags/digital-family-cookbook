using DigitalFamilyCookbook.Handlers.Commands.System;
using DigitalFamilyCookbook.Handlers.Queries.System;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace DigitalFamilyCookbook.Controllers;

[Route("system")]
[ApiController]
public class SystemController : Controller
{
    private readonly IMediator _mediatr;

    public SystemController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost("addrole")]
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
    public async Task<IReadOnlyCollection<RoleTypeDto>> GetRoles(GetRoleTypes.Query query, CancellationToken cancellationToken)
    {
        var roles = await _mediatr.Send(query, cancellationToken);

        return roles;
    }

    [HttpPost("deleterole")]
    public async Task<ActionResult> DeleteRole(DeleteRoleType.Command command, CancellationToken cancellationToken)
    {
        var role = await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("getsitesettings")]
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
