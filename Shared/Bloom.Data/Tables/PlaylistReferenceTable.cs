using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the playlist_reference table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PlaylistReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_reference table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE playlist_reference (" +
                                   "playlist_id VARCHAR(36) NOT NULL , " +
                                   "reference_id VARCHAR(36) NOT NULL , " +
                                   "PRIMARY KEY (playlist_id, reference_id) , " +
                                   "FOREIGN KEY (playlist_id) REFERENCES playlist(id) , " +
                                   "FOREIGN KEY (reference_id) REFERENCES reference(id) )";
    }
}