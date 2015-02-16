using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LibrarySongMediaTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_song_media table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE library_song_media (" +
                       "song_id VARCHAR(36) NOT NULL , " +
                       "media_type INTEGER NOT NULL , " +
                       "digital_formats INTEGER NOT NULL DEFAULT 0 , " +
                       "uri VARCHAR NOT NULL , " +
                       "is_compressed BOOL NOT NULL DEFAULT FALSE , " +
                       "is_protected BOOL NOT NULL DEFAULT FALSE , " +
                       "is_damaged BOOL NOT NULL DEFAULT FALSE , " +
                       "file_size INTEGER , " +
                       "sample_rate INTEGER , " +
                       "bit_rate INTEGER , " +
                       "volume_offset INTEGER , " +
                       "PRIMARY KEY (song_id, media_type) , " +
                       "FOREIGN KEY (song_id) REFERENCES song(id) )";
            }
        }
    }
}
