using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// Represents the suite_state table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SuiteStateTable : ISqlTable
    {
        /// <summary>
        /// Gets the create suite_state table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE suite_state (" +
                                   "suite_name VARCHAR PRIMARY KEY NOT NULL UNIQUE , " +
                                   "last_process_access VARCHAR NOT NULL , " +
                                   "process_accessed_on DATETIME )";
    }
}
