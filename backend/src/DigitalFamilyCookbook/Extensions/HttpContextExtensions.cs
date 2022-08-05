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


    public static string? GetAccessTokenFromHeaders(this HttpContext context)
    {
        return context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    }

    public static string? GetRefreshTokenFromHeaders(this HttpContext context)
    {
        return context.Request.Headers["RefreshToken"].FirstOrDefault();
    }

    public static string? GetAccessToken(this HttpContext context)
    {
        return context.Items["AccessToken"]?.ToString();
    }

    public static string? GetRefreshToken(this HttpContext context)
    {
        return context.Items["RefreshToken"]?.ToString();
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