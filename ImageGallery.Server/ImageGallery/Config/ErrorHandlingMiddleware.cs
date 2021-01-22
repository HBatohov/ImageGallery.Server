using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using ImageGallery.Exceptions;

namespace ImageGallery
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string message = exception.Message;

            int errorCode = StatusCodes.Status500InternalServerError;
            if (exception is AppBaseException)
                errorCode = (exception as AppBaseException).ErrorCode;

            var result = JsonConvert.SerializeObject(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorCode;
            return context.Response.WriteAsync(result);
        }
    }
}
