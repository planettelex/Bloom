using System;

namespace Bloom.Domain.Models
{
    public class RecordingSession
    {
        public Guid Id { get; set; }

        public Guid SongId { get; set; }

        public DateTime Date { get; set; }
    }
}
