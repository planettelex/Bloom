using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the review table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class ReviewTable : ISqlTable
    {
        /// <summary>
        /// Gets the create review table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE review (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "url VARCHAR , " +
                                   "title VARCHAR , " +
                                   "body VARCHAR , " +
                                   "published_on DATETIME ," +
                                   "source_id BLOB , " +
                                   "author_id BLOB , " +
                                   "FOREIGN KEY (source_id) REFERENCES source(id) , " +
                                   "FOREIGN KEY (author_id) REFERENCES person(id) )";
    }
}
