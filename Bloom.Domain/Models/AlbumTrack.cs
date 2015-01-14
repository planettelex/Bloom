using System;

namespace Bloom.Domain.Models
{
    public class AlbumTrack
    {
        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public Guid SongId { get; set; }

        public Song Song { get; set; }

        public int DiscNumber { get; set; }

        public int TrackNumber { get; set; }

        public int? StartTime { get; set; }

        public int? StopTime { get; set; }
    }
}
