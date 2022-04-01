using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;


namespace RestApi.ErrorServices
{
    internal sealed class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, $"Something went wrong: {ex}", ex.StackTrace);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var code = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case NotFoundException :
                    code = HttpStatusCode.NotFound;
                    break;
                case BadRequestException :
                    code = HttpStatusCode.BadRequest;
                    break;

                case { }:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = (int)code,
                Message = ex.Message,


            }.ToString()); ; ;
        }
    }
}