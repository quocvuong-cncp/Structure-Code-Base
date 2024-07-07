using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Domain.Application.DependencyInjection.Extensions;
public static class ServiceCollecttionExtensions
{
    public static IServiceCollection AddServicesApplicationCollecttion(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
        services.AddAutoMapper(AssemblyReference.Assembly);
        return services;
    }
}
