using System.ComponentModel;

namespace Custom_EventHub;

public sealed class PropertyChangeObserver : IPropertyChangeObserver
{
    public event EventHandler<ObservedPropertyChangedEventArgs>? Observed;

    public IDisposable Watch(INotifyPropertyChanged source, string sourceName)
    {
        PropertyChangedEventHandler handler = (_, e) =>
        {
            Observed?.Invoke(
                this,
                new ObservedPropertyChangedEventArgs(source, sourceName, e.PropertyName));
        };

        source.PropertyChanged += handler;

        return new Subscription(() => source.PropertyChanged -= handler);
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