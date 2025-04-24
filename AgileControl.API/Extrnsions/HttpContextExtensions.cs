using System.Security.Claims;

namespace AgileControl.API.Extrnsions;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext httpContext)
    {
        return Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}