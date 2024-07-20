using APIGateway.DenpendencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddServicesAPIGateway(builder.Configuration);
var app = builder.Build();
app.MapReverseProxy();

app.Run();
