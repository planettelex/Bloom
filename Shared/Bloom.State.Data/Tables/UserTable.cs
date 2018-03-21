using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// Represents the user table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class UserTable : ISqlTable
    {
        /// <summary>
        /// Gets the create user table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE user (" +
                                   "person_id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR NOT NULL , " +
                                   "birthday DATETIME , " +
                                   "twitter VARCHAR , " +
                                   "profile_image_path VARCHAR , " +
                                   "last_login DATETIME )";
    }
}
