using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the person_reference table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PersonReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create person_reference table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE person_reference (" +
                                   "person_id BLOB NOT NULL , " +
                                   "reference_id BLOB NOT NULL , " +
                                   "PRIMARY KEY (person_id, reference_id) , " +
                                   "FOREIGN KEY (person_id) REFERENCES person(id) , " +
                                   "FOREIGN KEY (reference_id) REFERENCES reference(id) )";
    }
}