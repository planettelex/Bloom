using System;

namespace Bloom.Domain.Models
{
    public class SongActivity
    {
        public Guid SongId { get; set; }

        public Song Song { get; set; }

        public Guid ActivityId { get; set; }

        public Activity Activity { get; set; }
    }
}
