using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Extensions
{
    public static class UserDetailsHttpContextExtension
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
