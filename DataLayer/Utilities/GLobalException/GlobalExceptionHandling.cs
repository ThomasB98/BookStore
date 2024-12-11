using DataLayer.Exceptions;
using DataLayer.Utilities.ResponseBody;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataLayer.Utilities.GLobalException
{
    public class GlobalExceptionHandling : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (UserAllredyExistsException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
            }

            catch(DatabaseOperationException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }

            catch(UserNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new ResponseBody<object>
            {
                Success = false,
                Message = exception.Message,
                StatusCode = statusCode,
                Data = null
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(JsonSerializer.Serialize(jsonResponse));
        }
    }
}
