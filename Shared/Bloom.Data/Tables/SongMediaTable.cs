using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_media table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongMediaTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_media table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE song_media (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "song_id BLOB NOT NULL , " +
                                   "media_type INTEGER NOT NULL , " +
                                   "digital_format INTEGER , " +
                                   "file_path VARCHAR , " +
                                   "is_damaged BOOL NOT NULL DEFAULT FALSE , " +
                                   "file_size INTEGER , " +
                                   "sample_rate INTEGER , " +
                                   "bit_rate INTEGER , " +
                                   "volume_offset INTEGER , " +
                                   "received_from_person_id BLOB , " +
                                   "FOREIGN KEY (received_from_person_id) REFERENCES person(id) , " +
                                   "FOREIGN KEY (song_id) REFERENCES song(id) )";
    }
}
