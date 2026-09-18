using System.ComponentModel;

namespace Custom_EventHub;

public interface IPropertyChangeObserver
{
    event EventHandler<ObservedPropertyChangedEventArgs>? Observed;

    IDisposable Watch(INotifyPropertyChanged source, string sourceName);
}