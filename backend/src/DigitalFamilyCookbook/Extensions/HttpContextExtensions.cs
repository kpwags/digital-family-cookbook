using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Extensions;

public static class HttpContextExtensions
{
    public static UserAccountApiModel CurrentUser(this HttpContext context)
    {
        if (context.Items.ContainsKey("User"))
        {
            if (context.Items["User"] != null)
            {
                UserAccountApiModel? user = null;

                user = (UserAccountApiModel?)context.Items["User"];

                return user ?? throw new System.Exception("Unable to authenticate user");
            }
        }

        throw new System.Exception("Unable to authenticate user");
    }
}