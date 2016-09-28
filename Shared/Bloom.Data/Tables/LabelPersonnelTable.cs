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
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE label_personnel (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "label_id VARCHAR(36) NOT NULL , " +
                       "person_id VARCHAR(36) NOT NULL , " +
                       "started DATETIME , " +
                       "ended DATETIME , " +
                       "priority INTEGER NOT NULL , " +
                       "FOREIGN KEY (label_id) REFERENCES label(id) , " +
                       "FOREIGN KEY (person_id) REFERENCES person(id) )";
            }
        }
    }
}
