using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_credit table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongCreditTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_credit table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE song_credit (" +
                                   "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                                   "song_id VARCHAR(36) NOT NULL , " +
                                   "person_id VARCHAR(36) NOT NULL , " +
                                   "is_featured BOOL NOT NULL DEFAULT FALSE , " +
                                   "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                                   "FOREIGN KEY (person_id) REFERENCES person(id) )";
    }
}