using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Bloom.State.Data.Services;
using Microsoft.Practices.Unity;

namespace Bloom.State.Data
{
    /// <summary>
    /// A file-based LINQ compatable data source for handling state data.
    /// </summary>
    public class StateDataSource : IDataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateDataSource"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StateDataSource(IUnityContainer container)
        {
            _container = container;
            _container.RegisterType<ITableService, StateTableService>(new ContainerControlledLifetimeManager());
            _stateTableService = _container.Resolve<ITableService>();
            FilePath = GetStateDatabasePath();
        }
        private readonly IUnityContainer _container;
        private readonly ITableService _stateTableService;

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
            _container.RegisterType<ISuiteStateRepository, SuiteStateRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<ISuiteStateRepository>();
            _container.RegisterType<ILibraryConnectionRepository, LibraryConnectionRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<ILibraryConnectionRepository>();
            _container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<IUserRepository>();
            _container.RegisterType<ITabRepository, TabRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<ITabRepository>();
            _container.RegisterType<IAnalyticsStateRepository, AnalyticsStateRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<IAnalyticsStateRepository>();
            _container.RegisterType<IBrowserStateRepository, BrowserStateRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<IBrowserStateRepository>();
            _container.RegisterType<IPlayerStateRepository, PlayerStateRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<IPlayerStateRepository>();
        }

        /// <summary>
        /// Creates a state database at this instance's file path, if one is specified.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot create a new state database while connected to an existing one.
        /// or
        /// The file path to the state database file has not been specified.
        /// </exception>
        public void Create()
        {
            if (IsConnected())
                throw new InvalidOperationException("Cannot create a new state database while connected to an existing one.");

            if (string.IsNullOrEmpty(FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            SQLiteConnection.CreateFile(FilePath);
            Connect();
            _stateTableService.CreateTables(Context);
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
            _stateTableService.CreateTables(Context);
        }

        /// <summary>
        /// Connects to a state database at this instance's file path, if one is specified.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot connect to a new state database while connected to an existing one.
        /// or
        /// The file path to the state database file has not been specified.
        /// </exception>
        public void Connect()
        {
            if (IsConnected())
                throw new InvalidOperationException("Cannot connect to a new state database while connected to an existing one.");

            if (string.IsNullOrEmpty(FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            var connectionString = string.Format("Data Source={0};Version=3;", FilePath);
            var connection = new SQLiteConnection(connectionString);
            Context = new DataContext(connection);
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
        /// Refreshes the specified object.
        /// </summary>
        /// <param name="toRefresh">The entitiy to refresh.</param>
        public void Refresh(object toRefresh)
        {
            if (IsConnected())
                Context.Refresh(RefreshMode.KeepChanges, toRefresh);
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

        private string GetStateDatabasePath()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appDataFolder, Properties.Settings.Default.Database_File);
        }
    }
}
