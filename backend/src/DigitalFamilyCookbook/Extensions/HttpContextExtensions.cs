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

    public static string? GetUserRefreshToken(this HttpContext context)
    {
        return context.Request.Cookies["refreshToken"];
    }

    public static void SetTokenCookie(this HttpContext context, string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        context.Response.Cookies.Append("refreshToken", token, cookieOptions);
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