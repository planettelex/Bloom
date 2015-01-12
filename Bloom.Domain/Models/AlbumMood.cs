using System;

namespace Bloom.Domain.Models
{
    public class AlbumMood
    {
        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public Guid MoodId { get; set; }

        public Mood Mood { get; set; }
    }
}
