using Microsoft.Extensions.Configuration;

namespace APIGateway.DenpendencyInjection.Extensions;

public static class ServeicesCollection
{
    public static IServiceCollection AddServicesAPIGateway(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy()
        .LoadFromConfig(configuration.GetSection("ReverseProxy"));
        return services;
    }
}
