using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Domain.Application.Abstractions;
using Domain.Contract.Constants;
using Domain.Infrastructure.Authentication;
using Domain.Infrastructure.Caching;
using Domain.Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Domain.Infrastructure.DependencyInjection.Extensions;
public static class ServiceCollecttionExtensions
{
    public static IServiceCollection AddExtensionsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<JwtOption>(configuration.GetSection("JWTOption"));
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        services.AddTransient<ICacheService, CacheService>();
        InitializeCache(services, configuration);
        return services;
    }
    public static void InitializeCache(IServiceCollection services, IConfiguration configuration)
    {
        var cacheOptions = new CacheOption();
        configuration.GetSection("CacheOption").Bind(cacheOptions);
        switch (cacheOptions.Type)
        {
            case CacheType.Memory:
                services.AddDistributedMemoryCache();
                break;
            case CacheType.SqlServer:
                /*
                 * Run this SQL to create cache table: dotnet sql-cache create <connection string> dbo <cache table>
                 */

                if (cacheOptions.SqlServerOptions == null)
                {
                    throw new Exception("Missing option: CachingOptions:SqlServer");
                }
                services.AddDistributedSqlServerCache(options => {
                    options.ConnectionString = configuration.GetConnectionString(cacheOptions.SqlServerOptions.ConnectionStringName);
                    options.TableName = cacheOptions.SqlServerOptions.TableName;
                    //options.SchemaName = cacheOptions.SqlServerOptions.SchemaName;
                });
                break;
            case CacheType.Redis:
                if (cacheOptions.RedisOptions == null)
                {
                    throw new Exception("Missing option: CachingOptions:Redis");
                }
                services.AddStackExchangeRedisCache(options => {
                    options.Configuration = configuration.GetConnectionString(cacheOptions.RedisOptions.ConnectionStringName);
                });
                break;
            default:
                throw new Exception("Unknown cache type");
        }
    }
}
