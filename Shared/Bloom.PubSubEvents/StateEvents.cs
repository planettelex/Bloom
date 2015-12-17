using System;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    public class ConnectionAddedEvent : PubSubEvent<LibraryConnection> { }

    public class ConnectionRemovedEvent : PubSubEvent<Guid> { }
}
