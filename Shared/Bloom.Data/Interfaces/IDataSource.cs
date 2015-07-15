using System.Data.Linq;

namespace Bloom.Data.Interfaces
{
    /// <summary>
    /// A file-based LINQ compatable data source.
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Gets the file path.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        DataContext Context { get; }

        /// <summary>
        /// Registers the repositories.
        /// </summary>
        void RegisterRepositories();

        /// <summary>
        /// Creates a state database at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void Create(string filePath);

        /// <summary>
        /// Connects to a state database at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void Connect(string filePath);

        /// <summary>
        /// Determines whether this data source is connected.
        /// </summary>
        bool IsConnected();

        /// <summary>
        /// Saves this data source.
        /// </summary>
        void Save();

        /// <summary>
        /// Disconnects this data source.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Destroys the data source file.
        /// </summary>
        void Destroy();
    }
}
