using System;

namespace Bloom.Domain.Models
{
    public class PlaylistActivity
    {
        public Guid PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public Guid ActivityId { get; set; }

        public Activity Activity { get; set; }
    }
}
