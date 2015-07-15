namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// A connection to the state database.
    /// </summary>
    public class StateConnection
    {
        /// <summary>
        /// Gets or sets the state database file path.
        /// </summary>
        public string FilePath { get; set; }
    }
}
