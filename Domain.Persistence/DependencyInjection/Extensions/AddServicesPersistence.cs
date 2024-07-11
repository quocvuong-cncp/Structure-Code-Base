using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using DemoCICD.Persistence.DependencyInjection.Options;
using Domain.Domain.Abstractions.Interface.Repositories;
using Domain.Domain.Abstractions.Interface.UnitofWorks;
using Domain.Domain.Abstractions.Repositories;
using Domain.Domain.Entities.Identity;
using Domain.Persistence.Interceptors;
using Domain.Persistence.Repositories;
using Domain.Persistence.Repositories.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Domain.Persistence.DependencyInjection.Extensions;
public static class AddServices
{
    public static IServiceCollection AddServicesPersistence(this IServiceCollection services)
    {
        services.AddTransient<IUnitofWorkEF, UnitofWorkEF>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IEventProjectRepository, EventRepositorry>();
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        ///
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var connectionStrings = configuration.GetConnectionString("CICD");
        var options = new SqlServerRetryOptions();
        var provider = services.BuildServiceProvider();
        configuration.GetSection(nameof(SqlServerRetryOptions)).Bind(options);
        services.AddDbContextPool<DbContext, ApplicationDBContext>((provider, builder) =>
        {
            builder.UseSqlServer(
                connectionStrings,
                optionBuilder => optionBuilder.ExecutionStrategy(dependencies => new SqlServerRetryingExecutionStrategy(
                    dependencies,
                    maxRetryCount: options.MaxRetryCount,
                    maxRetryDelay: options.MaxRetryDelay,
                    errorNumbersToAdd: options.ErrorNumbersToAdd))
                .MigrationsAssembly(typeof(ApplicationDBContext).Assembly.GetName().Name)
                ).AddInterceptors(  
                provider.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>(),
                provider.GetRequiredService<UpdateAuditableEntitiesInterceptor>())
            ;
        });
        services.AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDBContext>();
        services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        return services;
    }

}
