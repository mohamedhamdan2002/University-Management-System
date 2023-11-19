
using EMS.DataAccess.Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace EMS.Extensions
{
    public static class ExceptionMiddelwareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Use(async (context, next) =>
                {
                    
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        // logging process should  be hear
                    }
                    await next(context);
                });
            });
        }
    }
}
