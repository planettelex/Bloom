using System;

namespace Bloom.Domain.Models
{
    public class SongCollaborator
    {
        public Guid SongId { get; set; }

        public Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public bool IsFeatured { get; set; }
    }
}
