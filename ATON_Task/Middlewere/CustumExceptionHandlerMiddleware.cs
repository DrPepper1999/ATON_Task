using System;
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Exceptions;

namespace Users.WebApi.Middlewere
{
    public class CustumExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustumExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {

                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch(e)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case NonUniqueFieldException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case DbUpdateException:
                    if (e.InnerException is SqliteException)
                    {
                        code = HttpStatusCode.BadRequest;
                        result = e.InnerException.Message;
                    }
                       
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == String.Empty)
            {
                result = JsonSerializer.Serialize(new { error = e.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
