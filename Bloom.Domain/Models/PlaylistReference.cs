using System;

namespace Bloom.Domain.Models
{
    public class PlaylistReference
    {
        public Guid PlaylistId { get; set; }

        public Guid ReferenceId { get; set; }

        public Reference Reference { get; set; }
    }
}
