using Microsoft.AspNetCore.Http;

namespace DigitalFamilyCookbook.Tests.Utilities.Helpers;

public static class MockSession
{
    public static Mock<IHttpContextAccessor> BuildSession(UserAccountApiModel? user)
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();

        var context = new DefaultHttpContext();
        context.Items["User"] = user;

        httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

        return httpContextAccessor;
    }
}