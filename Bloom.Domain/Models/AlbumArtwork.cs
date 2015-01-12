using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloom.Domain
{
    public class AlbumArtwork
    {
        public Guid AlbumId { get; set; }

        public int Order { get; set; }

        public string Url { get; set; }
    }
}
