using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class SongTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE song (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "artist_id VARCHAR(36) NOT NULL , " +
                       "name VARCHAR NOT NULL , " +
                       "version VARCHAR , " +
                       "length INTEGER NOT NULL , " +
                       "genre_id VARCHAR(36) , " +
                       "bpm REAL , " +
                       "key INTEGER , " +
                       "time_signature_id VARCHAR(36) , " +
                       "description VARCHAR , " +
                       "lyrics VARCHAR , " +
                       "is_live BOOL NOT NULL DEFAULT FALSE , " +
                       "is_cover BOOL NOT NULL DEFAULT FALSE , " +
                       "is_remix BOOL NOT NULL DEFAULT FALSE , " +
                       "original_song_id VARCHAR(36) , " +
                       "is_holiday BOOL NOT NULL DEFAULT FALSE , " +
                       "holiday_id VARCHAR(36) , " +
                       "has_explicit_content BOOL NOT NULL DEFAULT FALSE , " +
                       "about_day_of_week INTEGER , " +
                       "about_time_of_year INTEGER , " +
                       "best_played_at_start INTEGER , " +
                       "best_played_at_stop INTEGER , " +
                       "FOREIGN KEY (artist_id) REFERENCES artist(id) , " +
                       "FOREIGN KEY (genre_id) REFERENCES genre(id) , " +
                       "FOREIGN KEY (time_signature_id) REFERENCES time_signature(id) , " +
                       "FOREIGN KEY (original_song_id) REFERENCES song(id) , " +
                       "FOREIGN KEY (holiday_id) REFERENCES holiday(id) )";
            }
        }
    }
}
