using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_credit table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumCreditTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_credit table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album_credit (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "album_id BLOB NOT NULL , " +
                                   "person_id BLOB NOT NULL , " +
                                   "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                                   "FOREIGN KEY (person_id) REFERENCES person(id) )";
    }
}