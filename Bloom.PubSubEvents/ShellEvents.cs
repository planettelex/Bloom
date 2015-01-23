using System.Windows.Controls;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.PubSubEvents
{
    /// <summary>
    /// Add the passed user control to the shell as a new tab.
    /// </summary>
    public class AddTabEvent : PubSubEvent<UserControl> { }

    /// <summary>
    /// Create a new library control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewLibraryTab : PubSubEvent<object> { }

    /// <summary>
    /// Create a new artist control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewArtistTab : PubSubEvent<object> { }

    /// <summary>
    /// Create a new person control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPersonTab : PubSubEvent<object> { }

    /// <summary>
    /// Create a new album control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewAlbumTab : PubSubEvent<object> { }

    /// <summary>
    /// Create a new song control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewSongTab : PubSubEvent<object> { }

    /// <summary>
    /// Create a new playlist control and pass it to the <see cref="AddTabEvent"/>.
    /// </summary>
    public class NewPlaylistTab : PubSubEvent<object> { }
}
