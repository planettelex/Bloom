using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_review table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongReviewTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_review table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE song_review (" +
                       "song_id VARCHAR(36) NOT NULL , " +
                       "review_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (song_id, review_id) , " +
                       "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                       "FOREIGN KEY (review_id) REFERENCES review(id) )";
            }
        }
    }
}