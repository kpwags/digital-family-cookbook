using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Extensions;

public static class HttpContextExtensions
{
    public static UserAccountApiModel CurrentUser(this HttpContext context, bool throwError = true)
    {
        if (context.Items.ContainsKey("User"))
        {
            if (context.Items["User"] != null)
            {
                var user = context.Items["User"] as UserAccountApiModel;

                if (user is null)
                {
                    if (throwError)
                    {
                        throw new System.Exception("Unable to authenticate user");
                    }

                    return UserAccountApiModel.None();
                }

                return user;
            }
        }

        if (throwError)
        {
            throw new System.Exception("Unable to authenticate user");
        }

        return UserAccountApiModel.None();
    }


    public static string? GetAccessToken(this HttpContext context)
    {
        return context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    }
    public static void SetRefreshToken(this HttpContext context, string token)
    {
        // append cookie with refresh token to the http response
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7),
            SameSite = SameSiteMode.None,
            Secure = true,
        };

        context.Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    public static string GetRefreshToken(this HttpContext context)
    {
        return context.Request.Cookies["refreshToken"] ?? "";
    }

    public static string GetUserIpAddress(this HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            return context.Request.Headers["X-Forwarded-For"];
        }
        else if (context.Connection is not null && context.Connection.RemoteIpAddress is not null)
        {
            return context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        return "";
    }
}