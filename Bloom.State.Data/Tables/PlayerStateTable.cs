namespace Bloom.State.Data.Tables
{
    public class PlayerStateTable : ISqlTable
    {
        /// <summary>
        /// Gets the create player_state table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"player_state\" (" +
                       "\"process_name\" VARCHAR PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"skin_name\" VARCHAR NOT NULL , " +
                       "\"window_state\" INTEGER NOT NULL , " +
                       "\"recent_width\" INTEGER NOT NULL , " +
                       "\"upcoming_width\" INTEGER NOT NULL )";
            }
        }
    }
}
