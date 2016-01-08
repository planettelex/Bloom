using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using Bloom.Data.Interfaces;
using Bloom.Data.Repositories;
using Bloom.Data.Services;
using Microsoft.Practices.Unity;

namespace Bloom.Data
{
    public class LibraryDataSource : IDataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDataSource" /> class.
        /// </summary>
        public LibraryDataSource(IUnityContainer container)
        {
            _container = container;
            _container.RegisterType<ITableService, LibraryTableService>(new ContainerControlledLifetimeManager());
            _libraryTableService = _container.Resolve<ITableService>();
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
            _container.RegisterType<IPersonRepository, PersonRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<IPersonRepository>();
            _container.RegisterType<ILibraryRepository, LibraryRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<ILibraryRepository>();
            _container.RegisterType<IAlbumRepository, AlbumRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<IAlbumRepository>();
            _container.RegisterType<IArtistRepository, ArtistRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<IArtistRepository>();
            _container.RegisterType<IFiltersetRepository, FiltersetRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<IFiltersetRepository>();
            _container.RegisterType<ILabelRepository, LabelRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<ILabelRepository>();
            _container.RegisterType<IPlaylistRepository, PlaylistRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<PlaylistRepository>();
            _container.RegisterType<ISongRepository, SongRepository>(new ContainerControlledLifetimeManager());
            _container.Resolve<ISongRepository>();
        }

        /// <summary>
        /// Creates a database at this instance's file path, if one is specified.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot create a new library database while connected to an existing one.
        /// or
        /// The file path to the library database file has not been specified.
        /// </exception>
        public void Create()
        {
            if (IsConnected())
                throw new InvalidOperationException("Cannot create a new library database while connected to an existing one.");

            if (string.IsNullOrEmpty(FilePath))
                throw new InvalidOperationException("The file path to the library database file has not been specified.");

            SQLiteConnection.CreateFile(FilePath);
            Connect();
            _libraryTableService.CreateTables(Context);
        }

        /// <summary>
        /// Creates a library database at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <exception cref="System.InvalidOperationException">Cannot create a new library database while connected to an existing one.</exception>
        public void Create(string filePath)
        {
            if (IsConnected())
                throw new InvalidOperationException("Cannot create a new library database while connected to an existing one.");

            SQLiteConnection.CreateFile(filePath);
            Connect(filePath);
            _libraryTableService.CreateTables(Context);
        }

        /// <summary>
        /// Connects to a library database at this instance's file path, if one is specified.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot connect to a new library database while connected to an existing one.
        /// or
        /// The file path to the library database file has not been specified.
        /// </exception>
        public void Connect()
        {
            if (IsConnected())
                throw new InvalidOperationException("Cannot connect to a new library database while connected to an existing one.");

            if (string.IsNullOrEmpty(FilePath))
                throw new InvalidOperationException("The file path to the library database file has not been specified.");

            var connectionString = string.Format("Data Source={0};Version=3;Foreign Keys=true;", FilePath);
            var connection = new SQLiteConnection(connectionString);
            Context = new DataContext(connection);
        }

        /// <summary>
        /// Connects to a library database at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <exception cref="System.InvalidOperationException">Cannot connect to a new library database while connected to an existing one.</exception>
        public void Connect(string filePath)
        {
            if (IsConnected())
                throw new InvalidOperationException("Cannot connect to a new library database while connected to an existing one.");

            var connectionString = string.Format("Data Source={0};Version=3;Foreign Keys=true;", filePath);
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
