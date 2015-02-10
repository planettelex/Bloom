using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using Bloom.Data.Interfaces;
using Microsoft.Practices.Unity;

namespace Bloom.Data
{
    public class LibraryDataSource : IDataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDataSource"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public LibraryDataSource(IUnityContainer container)
        {
            _container = container;
            //_container.RegisterType<ITableService, StateTableService>(new ContainerControlledLifetimeManager());
            //_stateTableService = _container.Resolve<ITableService>();
        }
        private readonly IUnityContainer _container;
        private readonly ITableService _libraryTableService;

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        public DataContext Context { get; private set; }

        /// <summary>
        /// Registers the repositories.
        /// </summary>
        public void RegisterRepositories()
        {
            
        }

        /// <summary>
        /// Creates a state database at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void Create(string filePath)
        {
            if (IsConnected())
                throw new InvalidOperationException("Cannot create a new state database while connected to an existing one.");

            SQLiteConnection.CreateFile(filePath);
            Connect(filePath);
            _libraryTableService.CreateTables(Context);
        }

        /// <summary>
        /// Connects to a state database at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <exception cref="System.InvalidOperationException">Cannot connect to a new state database while connected to an existing one.</exception>
        public void Connect(string filePath)
        {
            if (IsConnected())
                throw new InvalidOperationException("Cannot connect to a new state database while connected to an existing one.");

            var connectionString = string.Format("Data Source={0};Version=3;", filePath);
            var connection = new SQLiteConnection(connectionString);
            Context = new DataContext(connection);
            FilePath = filePath;
        }

        /// <summary>
        /// Determines whether this data source is connected.
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return Context != null;
        }

        /// <summary>
        /// Saves this data source.
        /// </summary>
        public void Save()
        {
            if (IsConnected())
                Context.SubmitChanges();
        }

        /// <summary>
        /// Disconnects this data source.
        /// </summary>
        public void Disconnect()
        {
            if (IsConnected())
            {
                Context.Dispose();
                Context = null;
            }
        }

        /// <summary>
        /// Destroys the data source file.
        /// </summary>
        public void Destroy()
        {
            Disconnect();
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            FilePath = null;
        }
    }
}
