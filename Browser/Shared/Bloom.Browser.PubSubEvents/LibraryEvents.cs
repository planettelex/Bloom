using System;
using Bloom.Browser.Common;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.PubSubEvents
{
    /// <summary>
    /// Invokes the create new library modal window.
    /// </summary>
    public class ShowCreateNewLibraryModalEvent : PubSubEvent<object> { }
    
    /// <summary>
    /// Changes the view of a library tab.
    /// </summary>
    public class ChangeLibraryTabViewEvent : PubSubEvent<Tuple<Guid, ViewType>> { }
}
