using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the holiday table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class HolidayTable : ISqlTable
    {
        /// <summary>
        /// Gets the create holiday table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE holiday (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "description VARCHAR , " +
                       "start_month INTEGER NOT NULL DEFAULT 0 , " +
                       "start_day INTEGER NOT NULL DEFAULT 0 , " +
                       "end_month INTEGER NOT NULL DEFAULT 0 , " +
                       "end_day INTEGER NOT NULL DEFAULT 0 )";
            }
        }
    }
}
