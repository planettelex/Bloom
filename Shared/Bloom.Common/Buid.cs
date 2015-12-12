using System;

namespace Bloom.Common
{
    /// <summary>
    /// Bloom universal identifier.
    /// </summary>
    public class Buid
    {
        public Buid(Guid libraryId, BloomEntity entityType, Guid entityId)
        {
            LibraryId = libraryId;
            EntityType = entityType;
            EntityId = entityId;
        }

        public Guid LibraryId { get; set; }

        public BloomEntity EntityType { get; set; }

        public Guid EntityId { get; set; }

        public override string ToString()
        {
            return LibraryId + "|" + (int) EntityType + "|" + EntityId;
        }
    }
}
