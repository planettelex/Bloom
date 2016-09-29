using System;
using Bloom.Browser.Common;
using Bloom.Browser.PubSubEvents.EventModels;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.PubSubEvents
{
    /// <summary>
    /// Invokes the create new library modal window.
    /// </summary>
    public class ShowCreateNewLibraryModalEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to show a new add music modal.
    /// </summary>
    public class ShowAddMusicModalEvent : PubSubEvent<object> { }
    
    /// <summary>
    /// Changes the view of a library tab.
    /// </summary>
    public class ChangeLibraryTabViewEvent : PubSubEvent<Tuple<Guid, ViewType>> { }

    /// <summary>
    /// Creates a new add music control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewAddMusicTabEvent : PubSubEvent<AddMusicEventModel> { }

    /// <summary>
    /// This event is published with a libary identifier to save library data.
    /// </summary>
    /// <seealso cref="Guid" />
    public class SaveLibraryEvent : PubSubEvent<Guid> { }
}
