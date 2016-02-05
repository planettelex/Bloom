using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    public class SuiteStateTable : ISqlTable
    {
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"suite_state\" (" +
                       "\"suite_name\" VARCHAR PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"last_process_access\" VARCHAR NOT NULL , " +
                       "\"process_accessed_on\" DATETIME)";
            }
        }
    }
}
