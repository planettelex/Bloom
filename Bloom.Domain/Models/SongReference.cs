using System;

namespace Bloom.Domain.Models
{
    public class SongReference
    {
        public Guid SongId { get; set; }

        public Guid ReferenceId { get; set; }

        public Reference Reference { get; set; }
    }
}
