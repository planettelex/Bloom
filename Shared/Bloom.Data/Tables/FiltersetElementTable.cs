using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class FiltersetElementTable : ISqlTable
    {
        /// <summary>
        /// Gets the create filterset_element table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE filterset_element (" +
                       "filterset_id VARCHAR(36) NOT NULL , " +
                       "element_number INTEGER NOT NULL , " +
                       "element_type INTEGER NOT NULL , " +
                       "statement_id VARCHAR(36) , " +
                       "PRIMARY KEY (filterset_id, element_number) , " +
                       "FOREIGN KEY (filterset_id) REFERENCES filterset(id) )";
            }
        }
    }
}