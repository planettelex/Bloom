using System;

namespace Bloom.Domain.Models
{
    public class AlbumTag
    {
        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
