using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ArtistReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create artist_reference table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"artist_reference\" (" +
                       "\"artist_id\" VARCHAR(36) NOT NULL , " +
                       "\"reference_id\" VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (\"artist_id\", \"reference_id\") )";
            }
        }
    }
}