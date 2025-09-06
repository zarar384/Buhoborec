using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Buhoborec.Application.Mappings;

public static class MapsterConfiguration
{
    public static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, Mapper>(); 
        return services;
    }
}
