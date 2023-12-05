using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace StudentAPI.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)

        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, logger);
            }
        }
        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex, ILogger<ExceptionHandlerMiddleware> logger)
        {
              var errorId = Guid.NewGuid();
                //Log this exception
                logger.LogError(ex, $"{errorId} : {ex.Message}"); //will log exceptions in format-> errorId : errorMessage
                //return custom error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are looking into this issue..."
                };

                return httpContext.Response.WriteAsJsonAsync(error);
        }
    }
}