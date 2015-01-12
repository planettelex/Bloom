using System;

namespace Bloom.Domain.Models
{
    public class SongMood
    {
        public Guid SongId { get; set; }

        public Song Song { get; set; }

        public Guid MoodId { get; set; }

        public Mood Mood { get; set; }
    }
}
