using System.Collections.Generic;

namespace Bloom.Data.Interfaces
{
    /// <summary>
    /// Seed data for a provided domain entity.
    /// </summary>
    /// <typeparam name="T">A domain model type.</typeparam>
    public interface ISeedData<T>
    {
        /// <summary>
        /// Gets the seed data for the entity type.
        /// </summary>
        List<T> SeedData();
    }
}
