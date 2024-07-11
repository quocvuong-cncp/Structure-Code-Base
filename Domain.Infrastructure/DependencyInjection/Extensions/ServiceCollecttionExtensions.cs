using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Domain.Application.Abstractions;
using Domain.Contract.Constants;
using Domain.Infrastructure.Authentication;
using Domain.Infrastructure.Caching;
using Domain.Infrastructure.DependencyInjection.Options;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Newtonsoft.Json;
using Quartz;
using Domain.Infrastructure.BackgroudJobs;
using Domain.Infrastructure.Consumer;

namespace Domain.Infrastructure.DependencyInjection.Extensions;
public static class ServiceCollecttionExtensions
{
    public static IServiceCollection AddExtensionsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<JwtOption>(configuration.GetSection("JWTOption"));
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        services.AddTransient<ICacheService, CacheService>();
        InitializeCache(services, configuration);
        AddQuartzInfrastructure(services);
        ConfigurationMasstransitRabbitMQ(services, configuration);
        
        return services;
    }
    public static void AddQuartzInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithInterval(TimeSpan.FromMilliseconds(100))
                                        .RepeatForever()));

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
    }
    public static async void ConfigurationMasstransitRabbitMQ(IServiceCollection services, IConfiguration configuration)
    {
        var masstransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);
        var messageBusOption = new MessageBusOptions();
        configuration.GetSection(nameof(MessageBusOptions)).Bind(messageBusOption);

        services.AddMassTransit( cfg =>
        {
            // ===================== Setup for Consumer =====================
           cfg.AddConsumers(Assembly.GetExecutingAssembly()); // Add all of consumers to masstransit instead above command
            //cfg.AddConsumer<ProductConsumer.ProductCreatedConsumer>();
            // ?? => Configure endpoint formatter. Not configure for producer Root Exchange
            cfg.SetKebabCaseEndpointNameFormatter(); // ??
    
            cfg.UsingRabbitMq((context, bus) =>
            {
                bus.Host(masstransitConfiguration.Host, masstransitConfiguration.Port, masstransitConfiguration.VHost, h =>
                {
                    h.Username(masstransitConfiguration.UserName);
                    h.Password(masstransitConfiguration.Password);
                });

                bus.UseMessageRetry(retry
                => retry.Incremental(
                           retryLimit: messageBusOption.RetryLimit,
                           initialInterval: messageBusOption.InitialInterval,
                           intervalIncrement: messageBusOption.IntervalIncrement));

                //bus.UseNewtonsoftJsonSerializer();

                //bus.ConfigureNewtonsoftJsonSerializer(settings =>
                //{
                //    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                //    settings.Converters.Add(new DateOnlyJsonConverter());
                //    settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                //    return settings;
                //});

                //bus.ConfigureNewtonsoftJsonDeserializer(settings =>
                //{
                //    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                //    settings.Converters.Add(new DateOnlyJsonConverter());
                //    settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                //    return settings;
                //});

                //bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                //bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                //bus.ConnectPublishObserver(new LoggingPublishObserver());
                //bus.ConnectSendObserver(new LoggingSendObserver());

                // Rename for Root Exchange and setup for consumer also
                bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());

                // ===================== Setup for Consumer =====================
                //bus.ReceiveEndpoint("order-created-queue", e =>
                //{
                //    e.ConfigureConsumer<ProductConsumer.ProductDeletedConsumer>(context);
                //});
                // Importantce to create Echange and Queue
                bus.ConfigureEndpoints(context);
            });

        });
        //var provider = services.BuildServiceProvider();
        //var _buscontrol = provider.GetRequiredService<IBusControl>();
        //await _buscontrol.StartAsync();


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
