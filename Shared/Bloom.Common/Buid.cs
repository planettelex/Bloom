using System;

namespace Bloom.Common
{
    /// <summary>
    /// Bloom universal identifier.
    /// </summary>
    public class Buid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Buid"/> class.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity identifier.</param>
        public Buid(Guid libraryId, BloomEntity entityType, Guid entityId)
        {
            LibraryId = libraryId;
            EntityType = entityType;
            EntityId = entityId;
        }

        /// <summary>
        /// Gets a Buid whose values are all zeros.
        /// </summary>
        public static Buid Empty => new Buid(Guid.Empty, BloomEntity.None, Guid.Empty);

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public Guid LibraryId { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity.
        /// </summary>
        public BloomEntity EntityType { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return LibraryId + "|" + (int) EntityType + "|" + EntityId;
        }
    }
}
