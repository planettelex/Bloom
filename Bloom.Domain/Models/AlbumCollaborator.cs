using System;

namespace Bloom.Domain.Models
{
    public class AlbumCollaborator
    {
        public Guid AlbumId { get; set; }
        
        public Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public bool IsFeatured { get; set; }
    }
}
