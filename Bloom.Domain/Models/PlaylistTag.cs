using System;

namespace Bloom.Domain.Models
{
    public class PlaylistTag
    {
        public Guid PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
