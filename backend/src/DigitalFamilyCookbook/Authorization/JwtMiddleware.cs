using DigitalFamilyCookbook.Core.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace DigitalFamilyCookbook.Authorization;

public class JwtMiddleware
{
    private readonly DigitalFamilyCookbookConfiguration _configuration;
    private readonly RequestDelegate _next;
    private readonly ILogger<JwtMiddleware> _logger;

    public JwtMiddleware(RequestDelegate next, DigitalFamilyCookbookConfiguration configuration, ILogger<JwtMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context, IUserAccountRepository userAccountRepository, ITokenService tokenService, IAuthService authService)
    {
        var accessToken = context.GetAccessTokenFromHeaders();

        if (accessToken is not null)
        {
            await AttachUserToContext(context, userAccountRepository, tokenService, accessToken);
        }

        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IUserAccountRepository userAccountRepository, ITokenService tokenService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret);

            var (userId, error) = tokenService.ValidateJwtToken(token);

            if (userId is not null)
            {
                var user = await userAccountRepository.GetUserAccountById(userId, true);

                // attach user to context on successful jwt validation
                context.Items["User"] = UserAccountApiModel.FromDomainModel(user);

                if (context.Items.ContainsKey("TokenError"))
                {
                    context.Items.Remove("TokenError");
                }
            }
            else if (error is not null)
            {
                context.Items["TokenError"] = error;
            }
            else
            {
                context.Items["TokenError"] = "unknown";
            }
        }
        catch
        {
            context.Items["TokenError"] = "unknown";
        }
    }
}