using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class SongReviewTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_review table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"song_review\" (" +
                       "\"song_id\" VARCHAR(36) NOT NULL , " +
                       "\"review_id\" VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (\"song_id\", \"review_id\") )";
            }
        }
    }
}