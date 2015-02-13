namespace Bloom.Data.Interfaces
{
    /// <summary>
    /// A SQL Table
    /// </summary>
    public interface ISqlTable
    {
        /// <summary>
        /// Gets the create table SQL.
        /// </summary>
        string CreateSql { get; }
    }
}
