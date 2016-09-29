using System;
using Bloom.Browser.Common;
using Bloom.Browser.PubSubEvents.EventModels;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.PubSubEvents
{
    /// <summary>
    /// This event is published to show a new library modal.
    /// </summary>
    public class ShowCreateNewLibraryModalEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to show a new add music modal.
    /// </summary>
    public class ShowAddMusicModalEvent : PubSubEvent<object> { }
    
    /// <summary>
    /// This event is published with a tab identifier and view type to change the view of a library tab.
    /// </summary>
    public class ChangeLibraryTabViewEvent : PubSubEvent<Tuple<Guid, ViewType>> { }

    /// <summary>
    /// This event creates a new add music control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewAddMusicTabEvent : PubSubEvent<AddMusicEventModel> { }
}
