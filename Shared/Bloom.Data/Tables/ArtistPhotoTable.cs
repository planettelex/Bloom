using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the artist_photo table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class ArtistPhotoTable : ISqlTable
    {
        /// <summary>
        /// Gets the create artist_photo table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE artist_photo (" +
                                   "artist_id BLOB NOT NULL , " +
                                   "photo_id BLOB NOT NULL , " +
                                   "priority INTEGER NOT NULL , " +
                                   "PRIMARY KEY (artist_id, photo_id, priority) , " +
                                   "FOREIGN KEY (artist_id) REFERENCES artist(id) , " +
                                   "FOREIGN KEY (photo_id) REFERENCES photo(id) )";
    }
}