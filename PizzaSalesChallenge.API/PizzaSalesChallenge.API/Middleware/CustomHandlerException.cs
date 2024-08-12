using Microsoft.AspNetCore.Mvc;

namespace PizzaSalesChallenge.API.Middleware
{
    public class CustomHandlerException
    {
        private readonly RequestDelegate _next;
        public CustomHandlerException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = !string.IsNullOrEmpty(ex.Message) ? ex.Message : "Server Error"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
