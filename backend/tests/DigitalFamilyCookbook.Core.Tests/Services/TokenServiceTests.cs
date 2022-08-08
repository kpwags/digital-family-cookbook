using DigitalFamilyCookbook.Configuration;
using DigitalFamilyCookbook.Core.Configuration;
using DigitalFamilyCookbook.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DigitalFamilyCookbook.Core.Tests.Services;

public class TokenServiceTests
{
    private DigitalFamilyCookbookConfiguration _configuration;
    private TokenValidationParameters _tokenValidationParameters;
    private Mock<IRefreshTokenRepository> _refreshTokenRepository;

    public TokenServiceTests()
    {
        _configuration = new DigitalFamilyCookbookConfiguration
        {
            Auth = new AuthConfiguration
            {
                JwtLifespan = 30,
                JwtSecret = "NotSoMuchASecretButThisIsOnlyForTests",
            },
        };

        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_configuration.Auth.JwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.Zero,
        };

        _refreshTokenRepository = new Mock<IRefreshTokenRepository>();
    }

    #region GeenrateAccessToken
    [Fact]
    public void ItGeneratesAnAccessToken()
    {
        var user = MockUser.GenerateUserDto();

        var claims = new List<Claim>();

        claims.Add(new Claim("id", user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

        var tokenService = new TokenService(_configuration, _tokenValidationParameters, _refreshTokenRepository.Object);

        var token = tokenService.GenerateAccessToken(claims);

        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }
    #endregion GeenrateAccessToken

    #region ValidateAccessToken
    [Fact]
    public void ItValidatesAValidToken()
    {
        /*
            Token Expires: 8/8/2028
            User ID: b86ec704-176b-11ed-861d-0242ac120002
            Email: test@digitalfamilycookbook.com
        */
        var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJEaWdpdGFsIEZhbWlseSBDb29rYm9vayIsImlhdCI6MTY1OTk5ODYxNiwiZXhwIjoxODQ5Mzg3NDM2LCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIiwic3ViIjoidGVzdEBkaWdpdGFsZmFtaWx5Y29va2Jvb2suY29tIiwiRW1haWwiOiJ0ZXN0QGRpZ2l0YWxmYW1pbHljb29rYm9vay5jb20iLCJpZCI6ImI4NmVjNzA0LTE3NmItMTFlZC04NjFkLTAyNDJhYzEyMDAwMiJ9.SlPbfYMZ4z5FujoFt_aqOx1MZLVWiYvWBC9JG_oSmQ0";

        var tokenService = new TokenService(_configuration, _tokenValidationParameters, _refreshTokenRepository.Object);

        var result = tokenService.ValidateAccessToken(token);

        Assert.Equal("b86ec704-176b-11ed-861d-0242ac120002", result.userId);
        Assert.Null(result.error);
    }

    [Fact]
    public void ItSendsAnInvalidTokenErrorIfItCantBeValidated()
    {
        var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJEaWdpdGFsIEZhbWlseSBDb29rYm9vayIsImlhdCI6MTY1OTk5ODYxNiwiZXhwIjoxODQ5Mzg3NDM2LCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIiwic3ViIjoidGVzdEBkaWdpdGFsZmFtaWx5Y29va2Jvb2suY29tIiwiRW1haWwiOiJ0ZXN0QGRpZ2l0YWxmYW1pbHljb29rYm9vay5jb20iLCJpZCI6ImI4NmVjNzA0LTE3NmItMTFlZC04NjFkLTAyNDJhYzEyMDAwMiJ9.R8Y_UH5NzeLxvNpZlhyWfZmsFFlDrU4hFAXjt-WU1lo";

        var tokenService = new TokenService(_configuration, _tokenValidationParameters, _refreshTokenRepository.Object);

        var result = tokenService.ValidateAccessToken(token);

        Assert.Null(result.userId);
        Assert.Equal("INVALID", result.error);
    }

    [Fact]
    public void ItReturnsAnExpiredErrorIfTokenIsExpired()
    {
        /*
            Already expired
            User ID: b86ec704-176b-11ed-861d-0242ac120002
            Email: test@digitalfamilycookbook.com
        */
        var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJEaWdpdGFsIEZhbWlseSBDb29rYm9vayIsImlhdCI6MTY1OTk5ODYxNiwiZXhwIjoxNjI4NDYyNjM2LCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIiwic3ViIjoidGVzdEBkaWdpdGFsZmFtaWx5Y29va2Jvb2suY29tIiwiRW1haWwiOiJ0ZXN0QGRpZ2l0YWxmYW1pbHljb29rYm9vay5jb20iLCJpZCI6ImI4NmVjNzA0LTE3NmItMTFlZC04NjFkLTAyNDJhYzEyMDAwMiJ9.hfjYj5OJrgNHSN7CV9Rz727qhi_2qZgSlPnXU1IHHTw";

        var tokenService = new TokenService(_configuration, _tokenValidationParameters, _refreshTokenRepository.Object);

        var result = tokenService.ValidateAccessToken(token);

        Assert.Null(result.userId);
        Assert.Equal("EXPIRED", result.error);
    }
    #endregion ValidateAccessToken

    #region GenerateRefreshToken
    [Fact]
    public async Task ItGeneratesARefreshToken()
    {
        var user = MockUser.GenerateUser();
        var ip = "127.0.0.1";

        var tokenService = new TokenService(_configuration, _tokenValidationParameters, _refreshTokenRepository.Object);

        var refreshToken = await tokenService.GenerateRefreshToken(ip, user);

        Assert.NotNull(refreshToken);
    }
    #endregion GenerateRefreshToken
}