using System;

namespace Bloom.Domain.Models
{
    public class PlaylistArtwork
    {
        public Guid PlaylistId { get; set; }

        public int Order { get; set; }

        public string Url { get; set; }
    }
}
