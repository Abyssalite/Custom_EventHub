namespace Custom_EventHub;

public sealed class ObservedPropertyChangedEventArgs : EventArgs
{
    public object Source { get; }
    public string SourceName { get; }
    public string? PropertyName { get; }

    public ObservedPropertyChangedEventArgs(
        object source,
        string sourceName,
        string? propertyName)
    {
        Source = source;
        SourceName = sourceName;
        PropertyName = propertyName;
    }
}