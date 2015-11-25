using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using Bloom.Data.Interfaces;
using Bloom.Data.Repositories;
using Bloom.Data.Services;

namespace Bloom.Data
{
    public class LibraryDataSource : IDataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDataSource" /> class.
        /// </summary>
        public LibraryDataSource()
        {
            _libraryTableService = new LibraryTableService();
        }
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
            AlbumRepository = new AlbumRepository(this);
            ArtistRepository = new ArtistRepository(this);
            FiltersetRepository = new FiltersetRepository(this);
            LabelRepository = new LabelRepository(this);
            LibraryRespository = new LibraryRepository(this);
            PersonRepository = new PersonRepository(this);
            PlaylistRepository = new PlaylistRepository(this);
            SongRepository = new SongRepository(this);
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

        /// <summary>
        /// Gets the album repository.
        /// </summary>
        public IAlbumRepository AlbumRepository { get; private set; }

        /// <summary>
        /// Gets the artist repository.
        /// </summary>
        public IArtistRepository ArtistRepository { get; private set; }

        /// <summary>
        /// Gets the filterset repository.
        /// </summary>
        public IFiltersetRepository FiltersetRepository { get; private set; }

        /// <summary>
        /// Gets the label repository.
        /// </summary>
        public ILabelRepository LabelRepository { get; private set; }

        /// <summary>
        /// Gets the library respository.
        /// </summary>
        public ILibraryRespository LibraryRespository { get; private set; }

        /// <summary>
        /// Gets the person repository.
        /// </summary>
        public IPersonRepository PersonRepository { get; private set; }

        /// <summary>
        /// Gets the playlist repository.
        /// </summary>
        public IPlaylistRepository PlaylistRepository { get; private set; }

        /// <summary>
        /// Gets the song repository.
        /// </summary>
        public ISongRepository SongRepository { get; private set; }
    }
}
