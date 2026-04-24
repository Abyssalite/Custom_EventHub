using System.Collections.Concurrent;

namespace Avalonia_EventHub;

public sealed class EventHub : IEventHub
{
    private readonly ConcurrentDictionary<Type, List<Delegate>> _handlers = new();

    public void Publish<TEvent>(TEvent evt)
    {
        if (!_handlers.TryGetValue(typeof(TEvent), out var handlers))
            return;

        Delegate[] snapshot;
        lock (handlers)
        {
            snapshot = handlers.ToArray();
        }

        foreach (var handler in snapshot)
        {
            ((Action<TEvent>)handler)(evt);
        }
    }

    public IDisposable Subscribe<TEvent>(Action<TEvent> handler)
    {
        var handlers = _handlers.GetOrAdd(typeof(TEvent), _ => new List<Delegate>());

        lock (handlers)
        {
            handlers.Add(handler);
        }

        return new Subscription(() =>
        {
            lock (handlers)
            {
                handlers.Remove(handler);
            }
        });
    }

    private sealed class Subscription : IDisposable
    {
        private readonly Action _dispose;
        private bool _disposed;

        public Subscription(Action dispose)
        {
            _dispose = dispose;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _dispose();
        }
    }
}