using System;

namespace Bloom.Domain.Models
{
    public class ArtistPhoto
    {
        public Guid ArtistId { get; set; }

        public Guid PhotoId { get; set; }

        public Photo Photo { get; set; }

        public int Order { get; set; }
    }
}
