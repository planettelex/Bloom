using System;
using Bloom.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    /// <summary>
    /// This event is published with library information to create a new library database and file system structure.
    /// </summary>
    /// <seealso cref="Bloom.Domain.Models.Library" />
    public class CreateNewLibraryEvent : PubSubEvent<Library> { }

    /// <summary>
    /// This event is published to show a connected libraries modal.
    /// </summary>
    public class ShowConnectedLibrariesModalEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published with a libary identifier to show a new library properties modal.
    /// </summary>
    /// <seealso cref="Guid" />
    public class ShowLibraryPropertiesModalEvent : PubSubEvent<Guid> { }
}
