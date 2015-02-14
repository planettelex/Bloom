using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumReviewTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_review table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"album_review\" (" +
                       "\"album_id\" VARCHAR(36) NOT NULL , " +
                       "\"review_id\" VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (\"album_id\", \"review_id\") )";
            }
        }
    }
}