using System;
using Bloom.Controls;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    /// <summary>
    /// Adds a new tab to the docking control.
    /// </summary>
    public class AddTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Creates a new library control and publish the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewLibraryTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Creates a new artist control and publish the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewArtistTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Creates a new person control and publish the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPersonTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Creates a new album control and publish the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewAlbumTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Creates a new song control and publish the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewSongTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Creates a new playlist control and publish the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPlaylistTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Duplicates the currently active tab.
    /// </summary>
    public class DuplicateTabEvent : PubSubEvent<Guid> { }

    /// <summary>
    /// Closes all tabs.
    /// </summary>
    public class CloseAllTabsEvent : PubSubEvent<object> { }

    /// <summary>
    /// Close all tabs except the currently active tab.
    /// </summary>
    public class CloseOtherTabsEvent : PubSubEvent<object> { }
}
