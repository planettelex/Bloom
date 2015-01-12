using System;

namespace Bloom.Domain.Models
{
    public class SongTag
    {
        public Guid SongId { get; set; }

        public Song Song { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
