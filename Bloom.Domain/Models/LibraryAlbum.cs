using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class LibraryAlbum
    {
        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public int Rating { get; set; }

        public string Review { get; set; }

        public List<LibraryAlbumMedia> Media { get; set; }
    }
}
