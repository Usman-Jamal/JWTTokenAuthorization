using Microsoft.AspNetCore.Mvc.Filters;
using Shared;
using System.Net;
namespace PracticeApp.Middleware
{
    public class Authorize : Attribute, IAsyncActionFilter
    {
        public Authorize() { }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var auth = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (auth.ValidateToken()) //you can implement your custom logic
                await next();
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

        }
    }
}
