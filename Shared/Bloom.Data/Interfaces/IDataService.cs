namespace Bloom.Data.Interfaces
{
    /// <summary>
    /// Service for managing SQL data.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Seeds the SQL tables.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        void SeedTables(IDataSource dataSource);
    }
}
