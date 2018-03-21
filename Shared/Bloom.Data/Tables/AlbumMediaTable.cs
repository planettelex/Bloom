using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_media table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumMediaTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_media table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album_media (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "album_id BLOB NOT NULL , " +
                                   "media_type INTEGER NOT NULL , " +
                                   "digital_format INTEGER , " +
                                   "media_condition INTEGER , " +
                                   "packaging_condition INTEGER , " +
                                   "approximate_value REAL , " +
                                   "purchased_price REAL , " +
                                   "purchased_on DATETIME , " +
                                   "on_loan_to_person_id BLOB , " +
                                   "release_id BLOB , " +
                                   "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                                   "FOREIGN KEY (on_loan_to_person_id) REFERENCES person(id) , " +
                                   "FOREIGN KEY (release_id) REFERENCES album_release(id) )";
    }
}
