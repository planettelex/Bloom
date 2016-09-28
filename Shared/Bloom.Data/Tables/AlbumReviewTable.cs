using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_review table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumReviewTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_review table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE album_review (" +
                       "album_id VARCHAR(36) NOT NULL , " +
                       "review_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (album_id, review_id) , " +
                       "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                       "FOREIGN KEY (review_id) REFERENCES review(id) )";
            }
        }
    }
}