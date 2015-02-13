using System;
using Bloom.Analytics.Common;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.PubSubEvents
{
    /// <summary>
    /// Changes the view of a library tab.
    /// </summary>
    public class ChangeLibraryTabViewEvent : PubSubEvent<Tuple<Guid, LibraryViewType>> { }
}
