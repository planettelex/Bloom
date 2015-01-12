using System;

namespace Bloom.Domain.Models
{
    public class AlbumReview
    {
        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public Guid ReviewId { get; set; }

        public Review Review { get; set; }
    }
}
