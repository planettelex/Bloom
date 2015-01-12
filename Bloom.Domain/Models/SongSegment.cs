using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class SongSegment
    {
        public Guid Id { get; set; }

        public Guid SongId { get; set; }

        public int Start { get; set; }

        public int Stop { get; set; }

        public MusicalKeys Key { get; set; }

        public TimeSignature TimeSignature { get; set; }
    }
}