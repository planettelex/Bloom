using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Bloom.Data;
using Bloom.Data.Interfaces;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Settings = Bloom.Common.Settings;

namespace Bloom.Browser.LibraryModule.Services
{
    /// <summary>
    /// Service for library import operations.
    /// </summary>
    /// <seealso cref="Bloom.Browser.LibraryModule.Services.IImportService" />
    public class ImportService : IImportService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="fileSystemService">The file system service.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="genreRepository">The genre repository.</param>
        /// <param name="personRepository">The person repository.</param>
        /// <param name="artistRepository">The artist repository.</param>
        /// <param name="albumRepository">The album repository.</param>
        /// <param name="songRepository">The song repository.</param>
        public ImportService(IEventAggregator eventAggregator, IRegionManager regionManager, IFileSystemService fileSystemService,
            IRoleRepository roleRepository, IGenreRepository genreRepository, IPersonRepository personRepository,
            IArtistRepository artistRepository, IAlbumRepository albumRepository, ISongRepository songRepository)
        {
            _eventAggregator = eventAggregator;
            _fileSystemService = fileSystemService;
            _roleRepository = roleRepository;
            _genreRepository = genreRepository;
            _personRepository = personRepository;
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
            _songRepository = songRepository;
            _regionManager = regionManager;

            // Subscribe to events
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileSystemService _fileSystemService;
        private readonly IRoleRepository _roleRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly ISongRepository _songRepository;
        private readonly IRegionManager _regionManager;
        private Dictionary<string, Role> _importedRoles; 
        private Dictionary<string, Genre> _importedGenres;
        private Dictionary<string, Person> _importedPeople;
        private Dictionary<string, Artist> _importedArtists;
        private Dictionary<string, Album> _importedAlbums;
        private Dictionary<string, Song> _importedSongs; 
        private ImportState _importState;
        private bool _isRunning;
        private const string Delimiters = ",|;|/";

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Sets the browser state.
        /// </summary>
        private void SetState(object nothing)
        {
            State = (BrowserState) _regionManager.Regions[Settings.DocumentRegion].Context;
        }

        /// <summary>
        /// Imports music files at the provided folder to the specified libraries.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="libraryIds">The library identifiers to import to.</param>
        /// <param name="copyFiles">If set to <c>true</c> copy media files.</param>
        public void ImportFiles(string folderPath, List<Guid> libraryIds, bool copyFiles)
        {
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath) || libraryIds == null || !libraryIds.Any())
                return;

            var mediaFiles = _fileSystemService.ListMusicFiles(folderPath);
            if (mediaFiles == null || !mediaFiles.Any())
                return;

            _isRunning = true;
            foreach (var libraryId in libraryIds)
            {
                _importedRoles = new Dictionary<string, Role>();
                _importedGenres = new Dictionary<string, Genre>();
                _importedPeople = new Dictionary<string, Person>();
                _importedArtists = new Dictionary<string, Artist>();
                _importedAlbums = new Dictionary<string, Album>();
                _importedSongs = new Dictionary<string, Song>();
                foreach (var filePath in mediaFiles)
                {
                    var mediaFile = _fileSystemService.ReadMediaFile(filePath);
                    _importState = new ImportState { Library = State.GetConnection(libraryId).Library };
                    ImportFile(mediaFile, State.GetConnectionData(libraryId), copyFiles);
                }
            }
            
            _isRunning = false;
            _importState = null;
            _importedGenres = null;
            _importedPeople = null;
            _importedArtists = null;
            _importedAlbums = null;
        }

        /// <summary>
        /// Imports the specified media file to the provided data source.
        /// </summary>
        /// <param name="mediaFile">The media file.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="copyFile">If set to <c>true</c> copies the media file.</param>
        private void ImportFile(MediaFile mediaFile, LibraryDataSource dataSource, bool copyFile)
        {
            if (mediaFile == null)
                return;

            if (mediaFile.Metadata != null)
            {
                if (!string.IsNullOrEmpty(mediaFile.Metadata.GenreName))
                    ImportGenre(mediaFile.Metadata.GenreName, dataSource);
                
                if (!string.IsNullOrEmpty(mediaFile.Metadata.Composers))
                    ImportComposers(mediaFile.Metadata.Composers, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.ArtistName))
                    ImportArtist(mediaFile.Metadata.ArtistName, ArtistType.Song, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.AlbumArtist))
                    ImportArtist(mediaFile.Metadata.AlbumArtist, ArtistType.Album, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.AlbumName))
                    ImportAlbum(mediaFile.Metadata.AlbumArtist, mediaFile.Metadata.AlbumName, dataSource);
            }
            ImportSong(mediaFile, copyFile, dataSource);
        }

        /// <summary>
        /// Imports the genre.
        /// </summary>
        /// <param name="genreName">The name of the genre.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportGenre(string genreName, IDataSource dataSource)
        {
            var genreKey = genreName.ToLowerInvariant();
            Genre genreMatch = null;
            if (_importedGenres.ContainsKey(genreKey))
                genreMatch = _importedGenres[genreKey];

            if (genreMatch == null)
            {
                var genreMatches = _genreRepository.FindGenre(dataSource, genreName);
                if (genreMatches == null || !genreMatches.Any())
                {
                    genreMatch = Genre.Create(genreName);
                    _genreRepository.AddGenre(dataSource, genreMatch);
                }
                else if (genreMatches.Count == 1)
                    genreMatch = genreMatches.First();
                else
                {
                    genreMatch = genreMatches.Last();
                    // TODO: Genre Disabiguation Window
                }
                _importedGenres.Add(genreKey, genreMatch);
            }
            _importState.Genre = genreMatch;
        }

        /// <summary>
        /// Imports composers from a list of names which can be comma, semi-colon, or forward slash delimited.
        /// </summary>
        /// <param name="peopleNames">A list of people names.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportComposers(string peopleNames, IDataSource dataSource)
        {
            var allNames = new List<string>();
            var delimiterRegEx = new Regex(Delimiters);
            if (delimiterRegEx.IsMatch(peopleNames))
            {
                var nameSplit = delimiterRegEx.Split(peopleNames);
                allNames.AddRange(nameSplit.Select(name => name.Trim()));
            }
            else
                allNames.Add(peopleNames);

            foreach (var personName in allNames)
                ImportComposer(personName, dataSource);
        } 

        /// <summary>
        /// Imports a composer from a name.
        /// </summary>
        /// <param name="personName">The name of the person.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportComposer(string personName, IDataSource dataSource)
        {
            ImportRole(Role.Composer, dataSource);

            var personKey = personName.ToLowerInvariant();
            Person personMatch = null;
            if (_importedPeople.ContainsKey(personKey))
                personMatch = _importedPeople[personKey];

            if (personMatch == null)
            {
                var personMatches = _personRepository.FindPerson(dataSource, personName);
                if (personMatches == null || !personMatches.Any())
                {
                    personMatch = Person.Create(personName);
                    _personRepository.AddPerson(dataSource, personMatch);
                }
                else if (personMatches.Count == 1)
                    personMatch = personMatches.First();
                else
                {
                    personMatch = personMatches.Last();
                    // TODO: Person Disabiguation Window
                }
                _importedPeople.Add(personKey, personMatch);
            }
            if (!_importState.Composers.Contains(personMatch))
                _importState.Composers.Add(personMatch);
        }

        /// <summary>
        /// Imports a role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportRole(Role role, IDataSource dataSource)
        {
            var roleKey = role.Name.ToLowerInvariant();
            if (_importedRoles.ContainsKey(roleKey))
                return;

            if (!_roleRepository.RoleExists(dataSource, role.Id))
                _roleRepository.AddRole(dataSource, role);

            _importedRoles.Add(roleKey, role);
        }

        /// <summary>
        /// Imports the artist.
        /// </summary>
        /// <param name="artistName">The name of the artist.</param>
        /// <param name="artistType">The artist type.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportArtist(string artistName, ArtistType artistType, IDataSource dataSource)
        {
            var artistKey = artistName.ToLowerInvariant();
            Artist artistMatch = null;
            if (_importedArtists.ContainsKey(artistKey))
                artistMatch = _importedArtists[artistKey];

            if (artistMatch == null)
            {
                var artistMatches = _artistRepository.FindArtist(dataSource, artistName);
                if (artistMatches == null || !artistMatches.Any())
                {
                    artistMatch = Artist.Create(artistName);
                    _artistRepository.AddArtist(dataSource, artistMatch);
                }
                else if (artistMatches.Count == 1)
                    artistMatch = artistMatches.First();
                else
                {
                    artistMatch = artistMatches.Last();
                    // TODO: Artist Disabiguation Window
                }
                _importedArtists.Add(artistKey, artistMatch);
            }

            if (artistType == ArtistType.Album)
                _importState.AlbumArtist = artistMatch;
            else
                _importState.Artist = artistMatch;
        }

        /// <summary>
        /// Imports the album.
        /// </summary>
        /// <param name="artistName">An artist name.</param>
        /// <param name="albumName">An album name.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportAlbum(string artistName, string albumName, IDataSource dataSource)
        {
            var albumKey = albumName.ToLowerInvariant();
            Album albumMatch = null;
            if (_importedAlbums.ContainsKey(albumKey))
                albumMatch = _importedAlbums[albumKey];

            if (albumMatch == null)
            {
                Artist albumArtist = null;
                if (!string.IsNullOrEmpty(artistName))
                {
                    var artistKey = artistName.ToLowerInvariant();
                    albumArtist = _importedArtists[artistKey];
                }

                var albumMatches = _albumRepository.FindAlbum(dataSource, artistName, albumName);
                if (albumMatches == null || !albumMatches.Any())
                {
                    albumMatch = Album.Create(albumName);
                    if (albumArtist != null)
                        albumMatch.ArtistId = albumArtist.Id;

                    _albumRepository.AddAlbum(dataSource, albumMatch);
                }
                else if (albumMatches.Count == 1)
                {
                    albumMatch = albumMatches.First();
                    albumMatch = _albumRepository.GetAlbum(dataSource, albumMatch.Id);
                }
                else
                {
                    albumMatch = albumMatches.Last();
                    albumMatch = _albumRepository.GetAlbum(dataSource, albumMatch.Id);
                    // TODO: Album Disabiguation Window
                }

                if (albumMatch.Tracks == null)
                    albumMatch.Tracks = new List<AlbumTrack>();

                _importedAlbums.Add(albumKey, albumMatch);
            }
            _importState.Album = albumMatch;
        }

        /// <summary>
        /// Imports the song.
        /// </summary>
        /// <param name="mediaFile">The media file.</param>
        /// <param name="copyFile">If set to <c>true</c> copies the media file.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportSong(MediaFile mediaFile, bool copyFile, LibraryDataSource dataSource)
        {
            var discNumber = 1;
            var trackNumber = 0;
            var songName = mediaFile.Metadata != null ? mediaFile.Metadata.Title : Path.GetFileNameWithoutExtension(mediaFile.Path);
            var artist = _importState.Artist ?? _importState.AlbumArtist;
            var newSong = Song.Create(songName, artist);
            newSong.Genre = _importState.Genre;
            newSong.Length = Convert.ToInt32(mediaFile.Duration.TotalMilliseconds);

            var filePath = mediaFile.Path;
            if (copyFile)
                filePath = _fileSystemService.CopyMediaFile(_importState.Library, mediaFile, newSong, _importState.Album);
                
            var songMedia = SongMedia.Create(newSong, mediaFile.Format, filePath);
            songMedia.FileSize = mediaFile.Size;

            if (mediaFile.Metadata != null)
            {
                newSong.Bpm = mediaFile.Metadata.Bpm;
                newSong.Notes = mediaFile.Metadata.Comments;
                songMedia.BitRate = mediaFile.Bitrate;
                songMedia.SampleRate = mediaFile.SampleRate;

                if (mediaFile.Metadata.DiscNumber != null && mediaFile.Metadata.DiscNumber.Value > 0)
                    discNumber = mediaFile.Metadata.DiscNumber.Value;

                if (mediaFile.Metadata.DiscCount != null && mediaFile.Metadata.DiscCount.Value > 0)
                    _importState.Album.DiscCount = mediaFile.Metadata.DiscCount.Value;

                if (mediaFile.Metadata.TrackNumber != null && mediaFile.Metadata.TrackNumber.Value > 0)
                    trackNumber = mediaFile.Metadata.TrackNumber.Value;

                if (mediaFile.Metadata.TrackCount != null && mediaFile.Metadata.TrackCount.Value > 0)
                    _importState.Album.SetTrackCount(discNumber, mediaFile.Metadata.TrackCount.Value);
            }
            newSong.Media = new List<SongMedia> { songMedia };
            var track = AlbumTrack.Create(_importState.Album, newSong, trackNumber, discNumber);
            
            _songRepository.AddSong(dataSource, newSong);
            _songRepository.AddSongMedia(dataSource, songMedia);
            _albumRepository.AddAlbumTrack(dataSource, track);

            foreach (var composer in _importState.Composers)
            {
                var composerCredit = SongCredit.Create(newSong, composer);
                _songRepository.AddSongCredit(dataSource, composerCredit);
                _songRepository.AddSongCreditRole(dataSource, composerCredit, Role.Composer);
            }

            var songKey = newSong.Name.ToLower();
            _importedSongs.Add(songKey, newSong);
            _importState.Album.Tracks.Add(track);
        }

        /// <summary>
        /// Determines whether this instance is currently running an import.
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            return _isRunning;
        }

        #region Import State Class

        /// <summary>
        /// Enum to distinguish between a song and album artist.
        /// </summary>
        private enum ArtistType
        {
            Song,
            Album
        }

        /// <summary>
        /// Represents the state of the import.
        /// </summary>
        private class ImportState
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ImportState"/> class.
            /// </summary>
            public ImportState()
            {
                Composers = new List<Person>();
            }

            /// <summary>
            /// Gets or sets the library being imported to.
            /// </summary>
            public Library Library { get; set; }

            /// <summary>
            /// Gets or sets the composers being imported.
            /// </summary>
            public List<Person> Composers { get; private set; }

            /// <summary>
            /// Gets or sets the genre being imported.
            /// </summary>
            public Genre Genre { get; set; }

            /// <summary>
            /// Gets or sets the artist being imported.
            /// </summary>
            public Artist Artist { get; set; }

            /// <summary>
            /// Gets or sets the album artist being imported.
            /// </summary>
            public Artist AlbumArtist { get; set; }

            /// <summary>
            /// Gets or sets the album being imported.
            /// </summary>
            public Album Album { get; set; }
        }

        #endregion
    }
}
