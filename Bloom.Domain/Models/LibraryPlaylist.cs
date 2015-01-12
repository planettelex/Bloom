using System;

namespace Bloom.Domain.Models
{
    public class LibraryPlaylist
    {
        public Guid PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public int Rating { get; set; }
    }
}
