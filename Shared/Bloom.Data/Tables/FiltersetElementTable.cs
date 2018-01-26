using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the filterset_element table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class FiltersetElementTable : ISqlTable
    {
        /// <summary>
        /// Gets the create filterset_element table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE filterset_element (" +
                                   "filterset_id VARCHAR(36) NOT NULL , " +
                                   "element_number INTEGER NOT NULL , " +
                                   "element_type INTEGER NOT NULL , " +
                                   "filter_id VARCHAR(36) , " +
                                   "filter_comparison INTEGER , " +
                                   "filter_against VARCHAR , " +
                                   "PRIMARY KEY (filterset_id, element_number) , " +
                                   "FOREIGN KEY (filterset_id) REFERENCES filterset(id) )";
    }
}