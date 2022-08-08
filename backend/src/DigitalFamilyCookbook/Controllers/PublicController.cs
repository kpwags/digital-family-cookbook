using DigitalFamilyCookbook.Handlers.Queries.System;
using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

[Route("public")]
[ApiController]
public class PublicController : Controller
{
    private readonly IMediator _mediatr;

    public PublicController(IMediator mediatr)
    {
        _mediatr = mediatr;
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
