using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class SongCreditTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_credit table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"song_credit\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"song_id\" VARCHAR(36) NOT NULL , " +
                       "\"person_id\" VARCHAR(36) NOT NULL , " +
                       "\"is_featured\" BOOL NOT NULL DEFAULT FALSE )";
            }
        }
    }
}