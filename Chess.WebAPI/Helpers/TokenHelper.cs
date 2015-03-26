using System;
using System.Web;

namespace Chess.WebAPI.Helpers
{
    public static class TokenHelper
    {
        public static Guid GetCurrentUserToken(HttpContext httpContext)
        {
            var token = HttpContext.Current.Request.Headers.Get("Authorization");
            return Guid.Parse(token);
        }
    }
}