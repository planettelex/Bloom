using Bloom.Domain.Models;
using Prism.Events;

namespace Bloom.Events
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
    public class ShowLibraryPropertiesModalEvent : PubSubEvent<object> { }
}
