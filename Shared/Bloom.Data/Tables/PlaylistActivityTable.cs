using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PlaylistActivityTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_activity table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"playlist_activity\" (" +
                       "\"playlist_id\" VARCHAR(36) NOT NULL , " +
                       "\"activity_id\" VARCHAR(36) NOT NULL ," +
                       "PRIMARY KEY (\"playlist_id\", \"activity_id\") )";
            }
        }
    }
}
