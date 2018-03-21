using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "artist_id BLOB , " +
                                   "name VARCHAR NOT NULL , " +
                                   "unofficial_name VARCHAR , " +
                                   "edition VARCHAR , " +
                                   "length_type INTEGER NOT NULL DEFAULT 0 , " +
                                   "length INTEGER NOT NULL DEFAULT 0 , " +
                                   "first_released_on DATETIME , " +
                                   "description VARCHAR , " +
                                   "liner_notes VARCHAR , " +
                                   "is_live BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_remix BOOL NOT NULL DEFAULT FALSE , " +
                                   "original_album_id BLOB , " +
                                   "is_tribute BOOL NOT NULL DEFAULT FALSE , " +
                                   "tribute_artist_id BLOB , " +
                                   "is_soundtrack BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_holiday BOOL NOT NULL DEFAULT FALSE , " +
                                   "holiday_id BLOB , " +
                                   "is_bootleg BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_promotional BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_compilation BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_mixed_artist BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_single_track BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_complete BOOL NOT NULL DEFAULT FALSE , " +
                                   "disc_count INTEGER NOT NULL DEFAULT 1 , " +
                                   "track_counts VARCHAR , " +
                                   "rating INTEGER , " +
                                   "FOREIGN KEY (artist_id) REFERENCES artist(id) , " +
                                   "FOREIGN KEY (tribute_artist_id) REFERENCES artist(id) , " +
                                   "FOREIGN KEY (holiday_id) REFERENCES holiday(id) )";
    }
}
