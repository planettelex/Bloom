using System;
using Bloom.Common;
using Bloom.Controls;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    /// <summary>
    /// Adds a new tab to the docking control.
    /// </summary>
    public class AddTabEvent : PubSubEvent<TabControl> { }

    /// <summary>
    /// Creates a new home control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewHomeTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Restores a saved home tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreHomeTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Creates a new getting started control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewGettingStartedTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Restores a saved getting started control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreGettingStartedTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Creates a new library control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewLibraryTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// Restores a saved library tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreLibraryTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Creates a new artist control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewArtistTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// Restores a saved artist tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreArtistTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Creates a new person control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPersonTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// Restores a saved person tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestorePersonTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Creates a new album control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewAlbumTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// Restores a saved album tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreAlbumTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Creates a new song control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewSongTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// Restores a saved song tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestoreSongTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Creates a new playlist control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPlaylistTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// Restores a saved playlist tab and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class RestorePlaylistTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Duplicates the currently active tab.
    /// </summary>
    public class DuplicateTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// Closes the specified tab.
    /// </summary>
    public class CloseTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// Closes all tabs.
    /// </summary>
    public class CloseAllTabsEvent : PubSubEvent<object> { }

    /// <summary>
    /// Close all tabs except the currently active tab.
    /// </summary>
    public class CloseOtherTabsEvent : PubSubEvent<object> { }

    /// <summary>
    /// Indicates the selected tab has changed to the provided tab id.
    /// </summary>
    public class SelectedTabChangedEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// Makes the sidebar visible.
    /// </summary>
    public class ShowSidebarEvent : PubSubEvent<object> { }

    /// <summary>
    /// Makes the sidebar hidden.
    /// </summary>
    public class HideSidebarEvent : PubSubEvent<object> { }

    /// <summary>
    /// Indicates the sidebar has toggled.
    /// </summary>
    public class SidebarToggledEvent : PubSubEvent<bool> { }
}
