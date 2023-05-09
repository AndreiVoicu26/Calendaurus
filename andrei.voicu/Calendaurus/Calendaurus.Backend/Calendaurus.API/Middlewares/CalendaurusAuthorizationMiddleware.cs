using Calendaurus.Models.Model;
using System.Net;

namespace Calendaurus.API.Middlewares
{
    public class CalendaurusAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CalendaurusAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CalendaurusContext calendaurusContext)
        {
            // 1 - if the request is not authenticated, nothing to do
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var userEmail = context.User.Identity.Name;
            if (userEmail is null)
            {
                // user is authenticated but we can't make out it's identity
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("invalid token");
                return;
            }

            var user = calendaurusContext.Users.SingleOrDefault(u => u.Email == userEmail);
            if (user is null)
            {
                // user is authenticated but it's email is not in our database
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("unknown user");
                return;
            }

            await _next(context);
        }
    }
}