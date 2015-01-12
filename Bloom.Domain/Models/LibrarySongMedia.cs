using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class LibrarySongMedia
    {
        public Guid SongId { get; set; }

        public Song Song { get; set; }

        public MediaTypes MediaType { get; set; }

        public DigitalFormat? DigitalFormat { get; set; }

        public string Uri { get; set; }

        public bool IsCompressed { get; set; }

        public bool IsProtected { get; set; }

        public bool IsDamaged { get; set; }

        public int? FileSize { get; set; }

        public int? SampleRate { get; set; }

        public int? BitRate { get; set; }

        public int VolumeOffset { get; set; }
    }
}
