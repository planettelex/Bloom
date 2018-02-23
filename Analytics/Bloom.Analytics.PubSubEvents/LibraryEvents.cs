using System;
using Bloom.Analytics.Controls;
using Prism.Events;

namespace Bloom.Analytics.PubSubEvents
{
    /// <summary>
    /// Changes the view of a library tab.
    /// </summary>
    public class ChangeLibraryTabViewEvent : PubSubEvent<Tuple<Guid, ViewType>> { }
}
