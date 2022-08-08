using DigitalFamilyCookbook.Extensions;
using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Utilities.Helpers;

public static class MockSession
{
    public static Mock<IHttpContextAccessor> BuildLoginSession()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();

        var context = new DefaultHttpContext();
        context.Request.Headers.Add("X-Forwarded-For", "127.0.0.1");

        httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

        return httpContextAccessor;
    }

    public static Mock<IHttpContextAccessor> BuildSession(UserAccountApiModel? user)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();

        var context = new DefaultHttpContext();
        context.Items["User"] = user;
        context.Request.Headers.Add("X-Forwarded-For", "127.0.0.1");

        httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

        return httpContextAccessor;
    }
}