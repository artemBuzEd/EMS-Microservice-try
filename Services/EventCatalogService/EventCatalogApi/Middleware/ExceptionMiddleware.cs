using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Check.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            
            HttpStatusCode statusCode;
            string title;
            string detail;
            
            (statusCode, title, detail) = ex switch
            {
                NotFoundException => (HttpStatusCode.NotFound, "Not Found", ex.Message),
                ValidationException => (HttpStatusCode.BadRequest, "Bad Request", ex.Message),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error", ex.Message)
            };
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            
            
            
            var response = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = "Error occured while processing request",
                Detail = ex.Message,
                Instance = ex.StackTrace
            };
            
            var json = JsonSerializer.Serialize(response);
            
            await context.Response.WriteAsync(json);
        }
    }
}