using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Length { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedById { get; set; }

        public Person CreatedBy { get; set; }

        public List<PlaylistTrack> Tracks { get; set; } 

        public List<PlaylistArtwork> Artwork { get; set; }

        public List<PlaylistReference> References { get; set; } 
    }
}
