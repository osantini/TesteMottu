using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mottu.Infra;

namespace Mottu.API.Application.Configuration
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    ConflictException e => (int)HttpStatusCode.Conflict,// conflict error
                    UnauthorizedAccessException e => (int)HttpStatusCode.Unauthorized,// unauthorized error
                    BadRequestException e => (int)HttpStatusCode.BadRequest,// badrequest error
                    NotFoundException e => (int)HttpStatusCode.NotFound,// notfound error
                    _ => (int)HttpStatusCode.InternalServerError,// unhandled error
                };

                var result = JsonSerializer.Serialize(new { message = "Ocorreu uma falha inesperada." });
                await response.WriteAsync(result);

                _logger.LogError(error, $"Ocorreu uma falha inesperada.");
            }
        }
    }
}