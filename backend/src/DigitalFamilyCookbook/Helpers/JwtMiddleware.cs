using DigitalFamilyCookbook.Core.Configuration;
using DigitalFamilyCookbook.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DigitalFamilyCookbook.Helpers;

public class JwtMiddleware
{
    private readonly DigitalFamilyCookbookConfiguration _configuration;
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next, DigitalFamilyCookbookConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context, IUserAccountRepository userAccountRepository, TokenValidationParameters tokenValidationParameters)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            await AttachUserToContext(context, userAccountRepository, tokenValidationParameters, token);
        }

        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IUserAccountRepository userAccountRepository, TokenValidationParameters tokenValidationParameters, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret);

            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var userId = jwtToken.Claims.First(x => x.Type == "Id").Value;

            var user = await userAccountRepository.GetUserAccountById(userId);

            // attach user to context on successful jwt validation
            context.Items["User"] = UserAccountApiModel.FromDomainModel(user);
        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
}