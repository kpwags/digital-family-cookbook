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
    public async Task<ActionResult> AddRole(SaveRoleType.Command command, CancellationToken cancellationToken)
    {
        await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("getroles")]
    public async Task<IReadOnlyCollection<RoleTypeApiModel>> GetRoles(CancellationToken cancellationToken)
    {
        var roles = await _mediatr.Send(new GetRoleTypes.Query(), cancellationToken);

        return roles;
    }

    [HttpGet("getrolebyid")]
    public async Task<ActionResult<RoleTypeApiModel>> GetRoleTypeById(string id, CancellationToken cancellationToken)
    {
        var role = await _mediatr.Send(new GetRoleTypeById.Query { Id = id }, cancellationToken);

        if (role.RoleTypeId == string.Empty)
        {
            return BadRequest("Unable to find role");
        }

        return Ok(role);
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

    [HttpPost("savesitesettings")]
    public async Task<IActionResult> SaveSiteSettings(SaveSiteSettings.Command command, CancellationToken cancellationToken)
    {
        try
        {
            await _mediatr.Send(command, cancellationToken);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("refreshinvitationcode")]
    public async Task<ActionResult<SiteSettingsApiModel>> RefreshInvitationCode(CancellationToken cancellationToken)
    {
        try
        {
            var settings = await _mediatr.Send(new RefreshInvitationCode.Command(), cancellationToken);

            return Ok(settings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getusers")]
    public async Task<IReadOnlyCollection<UserAccountApiModel>> GetUsers(bool includeRoles, CancellationToken cancellationToken)
    {
        var users = await _mediatr.Send(
            new GetAllUsers.Query { IncludeRoles = includeRoles },
            cancellationToken
        );

        return users;
    }

    [HttpPost("deleteuser")]
    public async Task<ActionResult> DeleteUser(DeleteUserAccount.Command command, CancellationToken cancellationToken)
    {
        await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("addusertorole")]
    public async Task<ActionResult> AddUserToRole(AddUserToRole.Command command, CancellationToken cancellationToken)
    {
        await _mediatr.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("deleterolefromuser")]
    public async Task<ActionResult> DeleteRoleFromUser(DeleteRoleFromUser.Command command, CancellationToken cancellationToken)
    {
        await _mediatr.Send(command, cancellationToken);

        return Ok();
    }
}
