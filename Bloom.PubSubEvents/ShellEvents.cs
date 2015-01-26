using System;
using Bloom.Controls;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    /// <summary>
    /// Add the passed user control to the shell as a new tab.
    /// </summary>
    public class AddTabEvent : PubSubEvent<Tab> { }

    /// <summary>
    /// Create a new library control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewLibraryTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Create a new artist control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewArtistTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Create a new person control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPersonTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Create a new album control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewAlbumTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Create a new song control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewSongTabEvent : PubSubEvent<object> { }

    /// <summary>
    /// Create a new playlist control and pass it to the <see cref="AddTabEvent"/>.
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
