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
    public async Task<ActionResult> SaveRole(SaveRoleType.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpGet("getroles")]
    public async Task<ActionResult<IReadOnlyCollection<RoleTypeApiModel>>> GetRoles(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new GetRoleTypes.Query(), cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
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
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

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
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpPost("refreshinvitationcode")]
    public async Task<ActionResult<SiteSettingsApiModel>> RefreshInvitationCode(CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(new RefreshInvitationCode.Command(), cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpGet("getusers")]
    public async Task<ActionResult<IReadOnlyCollection<UserAccountApiModel>>> GetUsers(bool includeRoles, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(
            new GetAllUsers.Query { IncludeRoles = includeRoles },
            cancellationToken
        );

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }

    [HttpPost("deleteuser")]
    public async Task<ActionResult> DeleteUser(DeleteUserAccount.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpPost("addusertorole")]
    public async Task<ActionResult> AddUserToRole(AddUserToRole.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }

    [HttpPost("deleterolefromuser")]
    public async Task<ActionResult> DeleteRoleFromUser(DeleteRoleFromUser.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok();
    }
}
