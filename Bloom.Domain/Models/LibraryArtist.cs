using System;

namespace Bloom.Domain.Models
{
    public class LibraryArtist
    {
        public Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public int Rating { get; set; }

        public string Notes { get; set; }

        public int PlayCount { get; set; }

        public int SkipCount { get; set; }
    }
}
