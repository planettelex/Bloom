using System;
using Bloom.Common;
using Bloom.Controls;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    /// <summary>
    /// This event is published with a tab control to add a new tab to the docking control.
    /// </summary>
    public class AddTabEvent : PubSubEvent<TabControl> { }

    /// <summary>
    /// This event creates a new home control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewHomeTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event restores a saved home tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreHomeTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event creates a new getting started control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewGettingStartedTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event restores a saved getting started control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreGettingStartedTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event creates a new library control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewLibraryTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event restores a saved library tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreLibraryTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event creates a new artist control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewArtistTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// Restores a saved artist tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreArtistTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event creates a new person control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPersonTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// This event restores a saved person tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestorePersonTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event creates a new album control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewAlbumTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// This event restores a saved album tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreAlbumTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event creates a new song control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewSongTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// This event restores a saved song tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreSongTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event creates a new playlist control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPlaylistTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// This event restores a saved playlist tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestorePlaylistTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event is published with a tab identifier to duplicate that tab.
    /// </summary>
    public class DuplicateTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event is published with a tab identifier to close that tab.
    /// </summary>
    public class CloseTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event is published to close all tabs.
    /// </summary>
    public class CloseAllTabsEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to close all tabs except the currently active tab.
    /// </summary>
    public class CloseOtherTabsEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published with a tab identifier to indicate that tab is the selected tab.
    /// </summary>
    public class SelectedTabChangedEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event is published to show the sidebar.
    /// </summary>
    public class ShowSidebarEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to hide the sidebar.
    /// </summary>
    public class HideSidebarEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published when the sidebar has been toggled.
    /// </summary>
    public class SidebarToggledEvent : PubSubEvent<bool> { }
}
