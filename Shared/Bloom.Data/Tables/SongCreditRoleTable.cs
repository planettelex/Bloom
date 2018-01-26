using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_credit_role table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongCreditRoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_credit_role table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE song_credit_role (" +
                                   "song_credit_id VARCHAR(36) NOT NULL , " +
                                   "role_id VARCHAR(36) NOT NULL , " +
                                   "PRIMARY KEY (song_credit_id, role_id) , " +
                                   "FOREIGN KEY (song_credit_id) REFERENCES song_credit(id) , " +
                                   "FOREIGN KEY (role_id) REFERENCES role(id) )";
    }
}