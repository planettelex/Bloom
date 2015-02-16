using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class SongTagTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_tag table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE song_tag (" +
                       "song_id VARCHAR(36) NOT NULL , " +
                       "tag_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (song_id, tag_id) , " +
                       "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                       "FOREIGN KEY (tag_id) REFERENCES tag(id) )";
            }
        }
    }
}