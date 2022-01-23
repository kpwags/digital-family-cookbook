using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Extensions;

public static class HttpContextExtensions
{
    public static UserAccountApiModel CurrentUser(this HttpContext context, bool throwError = true)
    {
        if (context.Items.ContainsKey("User"))
        {
            Console.WriteLine("USER EXISTS");
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
}