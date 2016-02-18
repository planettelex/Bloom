using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PlaylistArtworkTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_artwork table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE playlist_artwork (" +
                       "playlist_id VARCHAR(36) NOT NULL , " +
                       "file_path VARCHAR NOT NULL ," +
                       "priority INTEGER NOT NULL , " +
                       "PRIMARY KEY (playlist_id, priority) , " +
                       "FOREIGN KEY (playlist_id) REFERENCES playlist(id) )";
            }
        }
    }
}