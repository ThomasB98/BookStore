using DataLayer.Exceptions;
using DataLayer.Utilities.Logger;
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
        private readonly ILoggerService _logger;

        public GlobalExceptionHandling(ILoggerService logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (UserAllredyExistsException ex)
            {
                _logger.LogWarning($"User Already Exists Exception: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
            }

            catch(DatabaseOperationException ex)
            {
                _logger.LogError($"Database Operation Exception: {ex.Message}", ex);
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }

            catch(UserNotFoundException ex)
            {
                _logger.LogWarning($"User Not Found Exception: {ex.Message}");
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Unhandled Exception: {ex.Message}", ex);
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            _logger.LogError($"Exception Details: " +
                $"Path: {context.Request.Path}, " +
                $"Method: {context.Request.Method}, " +
                $"Status Code: {statusCode}, " +
                $"Message: {exception.Message}");

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
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
