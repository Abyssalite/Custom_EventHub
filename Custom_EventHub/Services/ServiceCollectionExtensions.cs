using Microsoft.Extensions.DependencyInjection;

namespace Custom_EventHub;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAvaloniaEventHub(this IServiceCollection services)
    {
        services.AddSingleton<IEventHub, EventHub>();
        services.AddSingleton<IPropertyChangeObserver, PropertyChangeObserver>();
        return services;
    }
}