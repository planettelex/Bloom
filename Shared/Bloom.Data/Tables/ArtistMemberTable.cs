using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ArtistMemberTable : ISqlTable
    {
        /// <summary>
        /// Gets the create artist_member table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"artist_member\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"artist_id\" VARCHAR(36) NOT NULL , " +
                       "\"person_id\" VARCHAR(36) NOT NULL , " +
                       "\"started\" DATETIME , " +
                       "\"ended\" DATETIME )";
            }
        }
    }
}
