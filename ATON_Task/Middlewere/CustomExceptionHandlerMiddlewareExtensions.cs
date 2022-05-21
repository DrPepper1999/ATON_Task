using System;
using Microsoft.AspNetCore.Builder;

namespace Users.WebApi.Middlewere
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustumExceptionHandlerMiddleware>();
        }
    }
}
