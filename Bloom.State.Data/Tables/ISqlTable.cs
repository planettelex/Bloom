namespace Bloom.State.Data.Tables
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
