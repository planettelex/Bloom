using Prism.Events;

namespace Bloom.Events
{
    /// <summary>
    /// This event is published when an application has loaded.
    /// </summary>
    public class ApplicationLoadedEvent : PubSubEvent<object> { }
}
