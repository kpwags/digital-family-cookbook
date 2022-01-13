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
    public async Task<IReadOnlyCollection<RoleType>> GetRoles(GetRoleTypes.Query query, CancellationToken cancellationToken)
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
}
