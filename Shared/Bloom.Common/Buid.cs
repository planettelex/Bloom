using System;

namespace Bloom.Common
{
    /// <summary>
    /// Bloom (global) unique identifier.
    /// </summary>
    public class Buid
    {
        public Guid LibraryId { get; set; }

        public BloomEntity EntityType { get; set; }

        public Guid EntityId { get; set; }

        public override string ToString()
        {
            return LibraryId + "|" + (int) EntityType + "|" + EntityId;
        }
    }
}
