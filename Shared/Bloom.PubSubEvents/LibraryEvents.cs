using System;
using Bloom.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    public class CreateNewLibraryEvent : PubSubEvent<Library> { }

    public class ShowConnectedLibrariesModalEvent : PubSubEvent<object> { }

    public class ShowAddMusicModalEvent : PubSubEvent<object> { }

    public class ShowLibraryPropertiesModalEvent : PubSubEvent<Guid> { }

    public class SaveLibraryEvent : PubSubEvent<Guid> { }
}
