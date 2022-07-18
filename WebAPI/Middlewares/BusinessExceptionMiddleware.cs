using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public class BusinessExceptionMiddleware
    {

        private readonly ILogger<BusinessExceptionMiddleware> Log;
        private readonly RequestDelegate _next;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public BusinessExceptionMiddleware(RequestDelegate next, ILogger<BusinessExceptionMiddleware> logger)
        {
            _next = next;
            Log = logger;

            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is BusinessException bex)
                {
                    Log.LogWarning(bex, "Business Exception : {}", bex.Code);
                    BuildResponse(bex, context.Response);
                }
                else
                {
                    throw;
                }
            }
        }
        private async void BuildResponse(BusinessException exception, HttpResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new DTO.Error
            {
                Code = exception.Code
            };

            response.ContentType = "application/json";
            string json = JsonSerializer.Serialize(error, jsonSerializerOptions);
            await response.WriteAsync(json);
        }
    }

    public static class BusinessExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseBusinessExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BusinessExceptionMiddleware>();
        }
    }
}
