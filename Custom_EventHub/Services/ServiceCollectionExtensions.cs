using Microsoft.Extensions.DependencyInjection;

namespace Custom_EventHub;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomEventHub(this IServiceCollection services)
    {
        services.AddSingleton<IEventHub, EventHub>();
        services.AddSingleton<IPropertyChangeObserver, PropertyChangeObserver>();
        return services;
    }
}