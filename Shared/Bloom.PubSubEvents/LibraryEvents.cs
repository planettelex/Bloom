using System;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    public class ShowConnectedLibrariesModalEvent : PubSubEvent<object> { }

    public class ShowLibraryPropertiesModalEvent : PubSubEvent<Guid> { }
}
