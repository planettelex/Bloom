using System;

namespace Bloom.Domain.Models
{
    public class PlaylistMood
    {
        public Guid PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public Guid MoodId { get; set; }

        public Mood Mood { get; set; }
    }
}
