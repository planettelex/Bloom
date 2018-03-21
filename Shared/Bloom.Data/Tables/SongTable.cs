using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE song (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "artist_id BLOB NOT NULL , " +
                                   "name VARCHAR NOT NULL , " +
                                   "version VARCHAR , " +
                                   "length INTEGER NOT NULL , " +
                                   "genre_id BLOB , " +
                                   "bpm REAL , " +
                                   "key INTEGER , " +
                                   "time_signature_id BLOB , " +
                                   "description VARCHAR , " +
                                   "lyrics VARCHAR , " +
                                   "is_live BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_cover BOOL NOT NULL DEFAULT FALSE , " +
                                   "is_remix BOOL NOT NULL DEFAULT FALSE , " +
                                   "original_song_id BLOB , " +
                                   "is_holiday BOOL NOT NULL DEFAULT FALSE , " +
                                   "holiday_id BLOB , " +
                                   "has_explicit_content BOOL NOT NULL DEFAULT FALSE , " +
                                   "about_day_of_week INTEGER , " +
                                   "about_time_of_year INTEGER , " +
                                   "best_played_at_start INTEGER , " +
                                   "best_played_at_stop INTEGER , " +
                                   "rating INTEGER , " +
                                   "notes VARCHAR , " +
                                   "play_count INTEGER NOT NULL DEFAULT 0 , " +
                                   "skip_count INTEGER NOT NULL DEFAULT 0 , " +
                                   "remove_count INTEGER NOT NULL DEFAULT 0 , " +
                                   "added_on DATETIME NOT NULL , " +
                                   "rated_on DATETIME , " +
                                   "last_played DATETIME , " +
                                   "FOREIGN KEY (artist_id) REFERENCES artist(id) , " +
                                   "FOREIGN KEY (genre_id) REFERENCES genre(id) , " +
                                   "FOREIGN KEY (time_signature_id) REFERENCES time_signature(id) , " +
                                   "FOREIGN KEY (original_song_id) REFERENCES song(id) , " +
                                   "FOREIGN KEY (holiday_id) REFERENCES holiday(id) )";
    }
}
