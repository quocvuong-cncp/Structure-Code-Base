using Domain.Persistence;

namespace Domain.API.Middleware;

public class MiddleWareTest
{
    private readonly RequestDelegate _next;

    public MiddleWareTest(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, ApplicationDBContext applicationDBContext)
    {
        await _next(context);
    }
}
