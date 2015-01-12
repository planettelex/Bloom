using System;

namespace Bloom.Domain.Models
{
    public class AlbumActivity
    {
        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public Guid ActivityId { get; set; }

        public Activity Activity { get; set; }
    }
}
