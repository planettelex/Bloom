using System;

namespace Bloom.Domain.Models
{
    public class AlbumReference
    {
        public Guid AlbumId { get; set; }

        public Guid ReferenceId { get; set; }

        public Reference Reference { get; set; }

    }
}
