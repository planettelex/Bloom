using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ArtistMemberRoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create artist_member_role table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE artist_member_role (" +
                       "artist_member_id VARCHAR(36) NOT NULL , " +
                       "role_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (artist_member_id, role_id) , " +
                       "FOREIGN KEY (artist_member_id) REFERENCES artist_member(id) , " +
                       "FOREIGN KEY (role_id) REFERENCES role(id) )";
            }
        }
    }
}