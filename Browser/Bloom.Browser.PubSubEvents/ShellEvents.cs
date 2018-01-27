using System;
using Bloom.Browser.Common;
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
    public class ChangeTabViewEvent : PubSubEvent<Tuple<Guid, ViewType>> { }
}
