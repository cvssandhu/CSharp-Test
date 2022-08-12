using CSharpTest.Model;
using Microsoft.AspNetCore.Diagnostics;
using ServiceContracts;
using System.Net;
using Helper;
namespace CSharpTest.Extentions
{
    public static class ExceptionMiddlewareExtensions
    {
        //public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        //{
        //    app.UseMiddleware<ExceptionMiddleware>();
        //}
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}
