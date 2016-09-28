using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_reference table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_reference table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE album_reference (" +
                       "album_id VARCHAR(36) NOT NULL , " +
                       "reference_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (album_id, reference_id) , " +
                       "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                       "FOREIGN KEY (reference_id) REFERENCES reference(id) )";
            }
        }
    }
}