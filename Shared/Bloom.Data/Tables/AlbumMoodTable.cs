using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumMoodTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_mood table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"album_mood\" (" +
                       "\"album_id\" VARCHAR(36) NOT NULL , " +
                       "\"mood_id\" VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (\"album_id\", \"mood_id\") )";
            }
        }
    }
}