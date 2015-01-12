using System;

namespace Bloom.Domain.Models
{
    public class SongReview
    {
        public Guid SongId { get; set; }

        public Song Song { get; set; }

        public Guid ReviewId { get; set; }

        public Review Review { get; set; }
    }
}
