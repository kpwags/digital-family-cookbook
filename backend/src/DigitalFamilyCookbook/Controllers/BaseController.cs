using Microsoft.AspNetCore.Mvc;

namespace DigitalFamilyCookbook.Controllers;

public class BaseController : Controller
{
    // protected (string? AccessToken, string? RefreshToken) GetTokens()
    // {
    //     var accessToken = HttpContext.GetAccessToken();
    //     var refreshToken = HttpContext.GetRefreshToken();

    //     return (accessToken, refreshToken);
    // }
}