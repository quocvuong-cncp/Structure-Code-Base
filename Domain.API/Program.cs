using Domain.API.DependencyInjection.Extensions;
using Domain.API.Middleware;
using Domain.Application.Behaviors;
using Domain.Application.DependencyInjection.Extensions;
using Domain.Application.Usecases.V1.Commands.Product;
using Domain.Infrastructure.DependencyInjection.Extensions;
using Domain.Persistence.DependencyInjection.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExtensionAPI(builder.Configuration);
builder.Services.AddServicesApplicationCollecttion();
builder.Services.AddServicesPersistence();
builder.Services.AddExtensionsInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
