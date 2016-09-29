using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    /// <summary>
    /// This event is published when an application has loaded.
    /// </summary>
    public class ApplicationLoadedEvent : PubSubEvent<object> { }
}
