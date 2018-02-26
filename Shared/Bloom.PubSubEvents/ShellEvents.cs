using System;
using Bloom.Common;
using Bloom.Controls;
using Bloom.Events.EventModels;
using Bloom.State.Domain.Models;
using Prism.Events;

namespace Bloom.Events
{
    /// <summary>
    /// This event is published with a tab control to add a new tab to the docking control.
    /// </summary>
    /// <seealso cref="Bloom.Controls.TabControl" />
    public class AddTabEvent : PubSubEvent<TabControl> { }

    /// <summary>
    /// This event is published with a tab identifier to indicate a new tab has been added.
    /// </summary>
    /// <seealso cref="Guid" />
    public class TabAddedEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event is published with a tab identifier to duplicate that tab.
    /// </summary>
    /// <seealso cref="Guid" />
    public class DuplicateTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event is published with a tab identifier to close that tab.
    /// </summary>
    /// <seealso cref="Guid" />
    public class CloseTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event is published with a tab identifier to indicate a tab has been closed.
    /// </summary>
    /// <seealso cref="Guid" />
    public class TabClosedEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event is published to close all tabs.
    /// </summary>
    public class CloseAllTabsEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to close all tabs except the currently active tab.
    /// </summary>
    public class CloseOtherTabsEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to indicate multiple tabs have been closed.
    /// </summary>
    public class TabsClosedEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published with a tab identifier to indicate that tab is the selected tab.
    /// </summary>
    /// <seealso cref="Guid" />
    public class SelectedTabChangedEvent : PubSubEvent<Guid> { }

    #region Typed Tab Events

    /// <summary>
    /// This event creates a new home control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewHomeTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event restores a saved home tab and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestoreHomeTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event creates a new getting started control and publishes the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewGettingStartedTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event restores a saved getting started control and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestoreGettingStartedTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event is published with a library identifier to create a new library control and publish the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Guid" />
    public class NewLibraryTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// This event restores a saved library tab and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestoreLibraryTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event is published with an entity identifier to create a new artist control and publish the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.Common.Buid" />
    public class NewArtistTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// Restores a saved artist tab and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestoreArtistTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event is published with an entity identifier to create a new person control and publish the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.Common.Buid" />
    public class NewPersonTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// This event restores a saved person tab and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestorePersonTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event is published with an entity identifier to create a new album control and publish the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.Common.Buid" />
    public class NewAlbumTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// This event restores a saved album tab and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestoreAlbumTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event is published with an entity identifier to create a new song control and publish the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.Common.Buid" />
    public class NewSongTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// This event restores a saved song tab and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestoreSongTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event is published with an entity identifier to create a new playlist control and publish the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.Common.Buid" />
    public class NewPlaylistTabEvent : PubSubEvent<Buid> { }

    /// <summary>
    /// This event restores a saved playlist tab and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestorePlaylistTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// This event is published with an event model to create a new add music control and publish the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="AddMusicEventModel" />
    public class NewAddMusicTabEvent : PubSubEvent<AddMusicEventModel> { }

    /// <summary>
    /// This event restores a saved add music tab and publishes the <see cref="AddTabEvent" />.
    /// </summary>
    /// <seealso cref="Bloom.State.Domain.Models.Tab" />
    public class RestoreAddMusicTabEvent : PubSubEvent<Tab> { }

    #endregion

    #region Sidebar Events

    /// <summary>
    /// This event is published to show the sidebar.
    /// </summary>
    public class ShowSidebarEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published to hide the sidebar.
    /// </summary>
    public class HideSidebarEvent : PubSubEvent<object> { }

    /// <summary>
    /// This event is published with a value indicate whether it is visible when the sidebar has been toggled.
    /// </summary>
    /// <seealso cref="bool" />
    public class SidebarToggledEvent : PubSubEvent<bool> { }

    #endregion
}
