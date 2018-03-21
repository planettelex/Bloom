using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the label_personel table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class LabelPersonnelTable : ISqlTable
    {
        /// <summary>
        /// Gets the create label_personel table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE label_personnel (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "label_id BLOB NOT NULL , " +
                                   "person_id BLOB NOT NULL , " +
                                   "started DATETIME , " +
                                   "ended DATETIME , " +
                                   "priority INTEGER NOT NULL , " +
                                   "FOREIGN KEY (label_id) REFERENCES label(id) , " +
                                   "FOREIGN KEY (person_id) REFERENCES person(id) )";
    }
}
