using System;

namespace Bloom.Domain.Models
{
    public class PlaylistTrack
    {
        public Guid PlaylistId { get; set; }

        public Guid SongId { get; set; }

        public Song Song { get; set; }

        public int TrackNumber { get; set; }
    }
}
