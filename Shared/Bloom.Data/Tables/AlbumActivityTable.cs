using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_activity table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumActivityTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_activity table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE album_activity (" +
                       "album_id VARCHAR(36) NOT NULL , " +
                       "activity_id VARCHAR(36) NOT NULL ," +
                       "PRIMARY KEY (album_id, activity_id) , " +
                       "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                       "FOREIGN KEY (activity_id) REFERENCES activity(id) )";
            }
        }
    }
}
