using DigitalFamilyCookbook.Core.Configuration;
using Microsoft.IdentityModel.Tokens;
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

    public async Task Invoke(HttpContext context, IUserAccountRepository userAccountRepository, ITokenService jwtService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            await AttachUserToContext(context, userAccountRepository, jwtService, token);
        }

        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IUserAccountRepository userAccountRepository, ITokenService jwtService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret);

            var userId = jwtService.ValidateJwtToken(token);

            if (userId is not null)
            {
                var user = await userAccountRepository.GetUserAccountById(userId, true);

                // attach user to context on successful jwt validation
                context.Items["User"] = UserAccountApiModel.FromDomainModel(user);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            context.Items["TokenError"] = "expired";
        }
        catch
        {
            context.Items["TokenError"] = "unknown";
        }
    }
}