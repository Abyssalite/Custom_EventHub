namespace Avalonia_EventHub;

public interface IEventHub
{
    void Publish<TEvent>(TEvent evt);
    IDisposable Subscribe<TEvent>(Action<TEvent> handler);
}