using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_credit_role table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumCreditRoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_credit_role table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album_credit_role (" +
                                   "album_credit_id BLOB NOT NULL , " +
                                   "role_id BLOB NOT NULL , " +
                                   "PRIMARY KEY (album_credit_id, role_id) , " +
                                   "FOREIGN KEY (album_credit_id) REFERENCES album_credit(id) , " +
                                   "FOREIGN KEY (role_id) REFERENCES role(id) )";
    }
}