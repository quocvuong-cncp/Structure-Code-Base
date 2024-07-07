using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using Domain.Domain.Exceptions;
using MediatR;

namespace Domain.API.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        try
        {
 
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }
    private  async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = GetCodeStatus(exception);
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            Detail = exception.Message,
            Title = GetTitle(exception),
            Status = httpContext.Response.StatusCode,
            Errors = GetErrors(exception)
        })); 
    }
    private object GetErrors(Exception exception)
    {
        if (exception is ValidationException)
        {
            var Request = (exception as ValidationException).Request ?? null;
            var NameRequest = Request.GetType().Name??null;
            return new
            {
                Request,
                NameRequest,
                Error = (exception as ValidationException).Errors

            };
        }
        return null;
    }
    private  string GetTitle(Exception exception)
        => exception switch
        {
            DomainException except => except.Title,
            _ => "Server Error!"
        };
    private  int GetCodeStatus(Exception exception)
    {
        return exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    }

 

}
