//using Entities.ErrorModel;
//using Entities.Exceptions;
//using Microsoft.AspNetCore.Diagnostics;

//namespace EMS.Extensions
//{
//    public static class ExceptionMiddelwareExtensions
//    {
//        public static void ConfigureExceptionHandler(this WebApplication app)
//        {
//            app.UseExceptionHandler(appError =>
//            {
//                appError.Run(async context =>
//                {
//                    context.Response.ContentType = "application/json";
//                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
//                if (contextFeature is not null)
//                {
//                    context.Response.StatusCode = contextFeature.Error switch
//                    {
//                        NotFoundException => StatusCodes.Status404NotFound,
//                        _ => StatusCodes.Status500InternalServerError
//                    };
//                        // logging process should  be hear

//                    await context.Response.WriteAsync(new ErrorDetails()
//                    {
//                        StatusCode = context.Response.StatusCode,
//                        Message = contextFeature.Error.Message,
//                    }.ToString());
//                }

//                });
//            });
//        }
//    }
//}
