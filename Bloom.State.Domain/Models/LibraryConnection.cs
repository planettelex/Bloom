using System;

namespace Bloom.State.Domain.Models
{
    public class LibraryConnection
    {
        public Guid LibraryId { get; set; }

        public string FilePath { get; set; }
    }
}
