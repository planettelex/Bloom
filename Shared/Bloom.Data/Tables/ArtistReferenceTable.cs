using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the artist_reference table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class ArtistReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create artist_reference table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE artist_reference (" +
                                   "artist_id VARCHAR(36) NOT NULL , " +
                                   "reference_id VARCHAR(36) NOT NULL , " +
                                   "PRIMARY KEY (artist_id, reference_id) , " +
                                   "FOREIGN KEY (artist_id) REFERENCES artist(id) , " +
                                   "FOREIGN KEY (reference_id) REFERENCES reference(id) )";
    }
}