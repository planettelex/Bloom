using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Bloom.Browser.State.Domain.Models;
using Bloom.Common.ExtensionMethods;
using Bloom.Data.Interfaces;
using Bloom.Data.Repositories;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
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
        /// <param name="mediaTagService">The media tag service.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="genreRepository">The genre repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="moodRepository">The mood repository.</param>
        /// <param name="tagRepository">The tag repository.</param>
        /// <param name="personRepository">The person repository.</param>
        /// <param name="artistRepository">The artist repository.</param>
        /// <param name="albumRepository">The album repository.</param>
        /// <param name="songRepository">The song repository.</param>
        public ImportService(IEventAggregator eventAggregator, IRegionManager regionManager, IFileSystemService fileSystemService, IMediaTagService mediaTagService,
            IRoleRepository roleRepository, IGenreRepository genreRepository, IActivityRepository activityRepository, IMoodRepository moodRepository, ITagRepository tagRepository,
            IPersonRepository personRepository, IArtistRepository artistRepository, IAlbumRepository albumRepository, ISongRepository songRepository)
        {
            _eventAggregator = eventAggregator;
            _fileSystemService = fileSystemService;
            _mediaTagService = mediaTagService;
            _roleRepository = roleRepository;
            _genreRepository = genreRepository;
            _activityRepository = activityRepository;
            _moodRepository = moodRepository;
            _tagRepository = tagRepository;
            _personRepository = personRepository;
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
            _songRepository = songRepository;
            _regionManager = regionManager;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileSystemService _fileSystemService;
        private readonly IMediaTagService _mediaTagService;
        private readonly IRoleRepository _roleRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IMoodRepository _moodRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly ISongRepository _songRepository;
        private readonly IRegionManager _regionManager;
        private ImportPreferences _importPreferences;
        private Dictionary<string, Role> _importedRoles; 
        private Dictionary<string, Genre> _importedGenres;
        private Dictionary<string, Activity> _importedActivities;
        private Dictionary<string, Mood> _importedMoods;
        private Dictionary<string, Tag> _importedTags;
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
        public BrowserState State { get { return (BrowserState) _regionManager.Regions[Settings.DocumentRegion].Context; } }

        /// <summary>
        /// Imports music files at the provided folder to the specified libraries.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="libraryIds">The library identifiers to import to.</param>
        /// <param name="copyFiles">If set to <c>true</c> copy media files.</param>
        /// <param name="importPreferences">The import preferences.</param>
        public void ImportFiles(string folderPath, List<Guid> libraryIds, bool copyFiles, ImportPreferences importPreferences)
        {
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath) || libraryIds == null || !libraryIds.Any())
                return;

            var mediaFiles = _fileSystemService.ListMusicFiles(folderPath);
            if (mediaFiles == null || !mediaFiles.Any())
                return;

            _importPreferences = importPreferences;
            _isRunning = true;
            foreach (var libraryId in libraryIds)
            {
                var library = State.GetConnection(libraryId).Library;
                var dataSource = State.GetConnectionData(libraryId);
                _importedRoles = new Dictionary<string, Role>();
                _importedGenres = new Dictionary<string, Genre>();
                _importedActivities = new Dictionary<string, Activity>();
                _importedMoods = new Dictionary<string, Mood>();
                _importedTags = new Dictionary<string, Tag>();
                _importedPeople = new Dictionary<string, Person>();
                _importedArtists = new Dictionary<string, Artist>();
                _importedAlbums = new Dictionary<string, Album>();
                _importedSongs = new Dictionary<string, Song>();
                
                foreach (var filePath in mediaFiles)
                {
                    _importState = new ImportState { Library = library };
                    var mediaFile = _fileSystemService.ReadMediaFile(filePath);
                    ImportFile(mediaFile, dataSource, copyFiles);
                }

                AnalyzeImport(copyFiles, dataSource);
                _importState = null;
            }
            
            _isRunning = false;
            _importPreferences = null;
            _importedGenres = null;
            _importedActivities = null;
            _importedMoods = null;
            _importedTags = null;
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
        private void ImportFile(MediaFile mediaFile, IDataSource dataSource, bool copyFile)
        {
            if (mediaFile == null)
                return;

            if (mediaFile.Metadata != null)
            {
                if (!string.IsNullOrEmpty(mediaFile.Metadata.GenreName))
                    ImportGenre(mediaFile.Metadata.GenreName, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.Grouping))
                    ImportTaxonomy(mediaFile.Metadata.Grouping, dataSource);
                
                if (!string.IsNullOrEmpty(mediaFile.Metadata.Composers))
                    ImportComposers(mediaFile.Metadata.Composers, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.ArtistName))
                    ImportArtist(mediaFile.Metadata.ArtistName, ArtistScope.Song, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.AlbumArtist))
                    ImportArtist(mediaFile.Metadata.AlbumArtist, ArtistScope.Album, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.AlbumName))
                {
                    ImportAlbum(mediaFile.Metadata.AlbumArtist, mediaFile.Metadata.AlbumName, dataSource);
                    CopyAlbumArtwork(mediaFile.Metadata.AlbumArtist, mediaFile.Metadata.AlbumName, mediaFile, dataSource);
                }
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
            var genreKey = genreName.AsKey();
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
        /// Imports the taxonomy.
        /// </summary>
        /// <param name="taxonomyName">The taxonomy name.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportTaxonomy(string taxonomyName, IDataSource dataSource)
        {
            switch (_importPreferences.MapGroupingTo.Item1)
            {
                case TaxonomyType.Activity:
                    ImportActivity(taxonomyName, dataSource);
                    break;
                case TaxonomyType.Mood:
                    ImportMood(taxonomyName, dataSource);
                    break;
                case TaxonomyType.Tag:
                    ImportTag(taxonomyName, dataSource);
                    break;
            }
        }

        /// <summary>
        /// Imports the activity.
        /// </summary>
        /// <param name="activityName">The name of the activity.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportActivity(string activityName, IDataSource dataSource)
        {
            var activityKey = activityName.AsKey();
            Activity activityMatch = null;
            if (_importedActivities.ContainsKey(activityKey))
                activityMatch = _importedActivities[activityKey];

            if (activityMatch == null)
            {
                var activityMatches = _activityRepository.FindActivity(dataSource, activityName);
                if (activityMatches == null || !activityMatches.Any())
                {
                    activityMatch = Activity.Create(activityName);
                    _activityRepository.AddActivity(dataSource, activityMatch);
                }
                else if (activityMatches.Count == 1)
                    activityMatch = activityMatches.First();
                else
                {
                    activityMatch = activityMatches.Last();
                    // TODO: Activity Disabiguation Window
                }
                _importedActivities.Add(activityKey, activityMatch);
            }
            _importState.Activity = activityMatch;
        }

        /// <summary>
        /// Imports the mood.
        /// </summary>
        /// <param name="moodName">The name of the mood.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportMood(string moodName, IDataSource dataSource)
        {
            var moodKey = moodName.AsKey();
            Mood moodMatch = null;
            if (_importedMoods.ContainsKey(moodKey))
                moodMatch = _importedMoods[moodKey];

            if (moodMatch == null)
            {
                var moodMatches = _moodRepository.FindMood(dataSource, moodName);
                if (moodMatches == null || !moodMatches.Any())
                {
                    moodMatch = Mood.Create(moodName);
                    _moodRepository.AddMood(dataSource, moodMatch);
                }
                else if (moodMatches.Count == 1)
                    moodMatch = moodMatches.First();
                else
                {
                    moodMatch = moodMatches.Last();
                    // TODO: Mood Disabiguation Window
                }
                _importedMoods.Add(moodKey, moodMatch);
            }
            _importState.Mood = moodMatch;
        }

        /// <summary>
        /// Imports the tag.
        /// </summary>
        /// <param name="tagName">The name of the tag.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportTag(string tagName, IDataSource dataSource)
        {
            var tagKey = tagName.AsKey();
            Tag tagMatch = null;
            if (_importedTags.ContainsKey(tagKey))
                tagMatch = _importedTags[tagKey];

            if (tagMatch == null)
            {
                var tagMatches = _tagRepository.FindTag(dataSource, tagName);
                if (tagMatches == null || !tagMatches.Any())
                {
                    tagMatch = Tag.Create(tagName);
                    _tagRepository.AddTag(dataSource, tagMatch);
                }
                else if (tagMatches.Count == 1)
                    tagMatch = tagMatches.First();
                else
                {
                    tagMatch = tagMatches.Last();
                    // TODO: Tag Disabiguation Window
                }
                _importedTags.Add(tagKey, tagMatch);
            }
            _importState.Tag = tagMatch;
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

            var personKey = personName.AsKey();
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
        /// Imports the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportRole(Role role, IDataSource dataSource)
        {
            var roleKey = role.Name.AsKey();
            if (_importedRoles.ContainsKey(roleKey))
                return;

            if (!_roleRepository.RoleExists(dataSource, role.Id))
                _roleRepository.AddRole(dataSource, role);

            _importedRoles.Add(roleKey, role);
        }

        /// <summary>
        /// Imports the artist.
        /// </summary>
        /// <param name="artistName">The scope of the artist.</param>
        /// <param name="artistType">The artist type.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportArtist(string artistName, ArtistScope artistType, IDataSource dataSource)
        {
            var artistKey = artistName.AsKey();
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

            if (artistType == ArtistScope.Album)
                _importState.AlbumArtist = artistMatch;
            else
                _importState.Artist = artistMatch;

            if (!_importPreferences.ComposersAlsoArtistMembers) 
                return;

            foreach (var composer in _importState.Composers)
                ImportArtistMember(artistMatch, composer, dataSource);
        }

        /// <summary>
        /// Imports the artist member.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="member">The artist member.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportArtistMember(Artist artist, Person member, IDataSource dataSource)
        {
            if (artist.Members == null)
                artist.Members = new List<ArtistMember>();

            if (artist.Members.Any(m => m.PersonId == member.Id))
                return;

            var priority = artist.GetNextMemberPriority();
            var artistMember = ArtistMember.Create(artist, member, priority);
            _artistRepository.AddArtistMember(dataSource, artistMember);
            artist.Members.Add(artistMember);
        }

        /// <summary>
        /// Imports the album.
        /// </summary>
        /// <param name="artistName">An artist name.</param>
        /// <param name="albumName">An album name.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportAlbum(string artistName, string albumName, IDataSource dataSource)
        {
            var albumKey = AlbumKey(artistName, albumName);
            Album albumMatch = null;
            if (_importedAlbums.ContainsKey(albumKey))
                albumMatch = _importedAlbums[albumKey];

            if (albumMatch == null)
            {
                Artist albumArtist = null;
                if (!string.IsNullOrEmpty(artistName))
                {
                    var artistKey = artistName.AsKey();
                    albumArtist = _importedArtists[artistKey];
                }

                var albumMatches = _albumRepository.FindAlbum(dataSource, artistName, albumName);
                if (albumMatches == null || !albumMatches.Any())
                {
                    albumName = _importPreferences.TitleCaseAlbumTitles ? albumName.TitleCase() : albumName;
                    albumMatch = Album.Create(albumName);
                    if (albumArtist != null)
                        albumMatch.Artist = albumArtist;

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

                if (_importPreferences.MapGroupingTo.Item2 == TaxonomyScope.Album)
                {
                    if (_importState.Activity != null)
                        _activityRepository.AddActivityTo(dataSource, _importState.Activity, albumMatch);

                    if (_importState.Mood != null)
                        _moodRepository.AddMoodTo(dataSource, _importState.Mood, albumMatch);

                    if (_importState.Tag != null)
                        _tagRepository.AddTagTo(dataSource, _importState.Tag, albumMatch);
                }

                if (albumMatch.Tracks == null)
                    albumMatch.Tracks = new List<AlbumTrack>();

                _importedAlbums.Add(albumKey, albumMatch);
            }
            _importState.Album = albumMatch;
        }

        /// <summary>
        /// Copies the album artwork.
        /// </summary>
        /// <param name="artistName">The name of the artist.</param>
        /// <param name="albumName">The name of the album.</param>
        /// <param name="mediaFile">The media file.</param>
        /// <param name="dataSource">The data source.</param>
        private void CopyAlbumArtwork(string artistName, string albumName, MediaFile mediaFile, IDataSource dataSource)
        {
            var albumKey = AlbumKey(artistName, albumName);
            Album albumMatch = null;
            if (_importedAlbums.ContainsKey(albumKey))
                albumMatch = _importedAlbums[albumKey];

            if (albumMatch == null || (albumMatch.Artwork != null && albumMatch.Artwork.Any()))
                return;

            var albumArtworkImage = _mediaTagService.ReadMediaImage(mediaFile.Path);
            if (albumArtworkImage == null)
                return;

            var imageFilePath = _fileSystemService.SaveAlbumImage(_importState.Library, albumArtworkImage, albumMatch);
            var albumArtwork = AlbumArtwork.Create(albumMatch, imageFilePath, 1);
            _albumRepository.AddAlbumArtwork(dataSource, albumArtwork);
            _importedAlbums[albumKey].Artwork = new List<AlbumArtwork> { albumArtwork };
        }

        /// <summary>
        /// Imports the song.
        /// </summary>
        /// <param name="mediaFile">The media file.</param>
        /// <param name="copyFile">If set to <c>true</c> copies the media file.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportSong(MediaFile mediaFile, bool copyFile, IDataSource dataSource)
        {
            var discNumber = 1;
            var trackNumber = 0;
            var songName = mediaFile.Metadata != null ? mediaFile.Metadata.Title : Path.GetFileNameWithoutExtension(mediaFile.Path);
            songName = _importPreferences.TitleCaseSongTitles ? songName.TitleCase() : songName;
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
                if (!string.IsNullOrEmpty(mediaFile.Metadata.Comments))
                {
                    if (_importPreferences.MapCommentsTo.Item2 == ArtistScope.Song)
                    {
                        switch (_importPreferences.MapCommentsTo.Item1)
                        {
                            case TextPropertyType.Description:
                                newSong.Description = mediaFile.Metadata.Comments;
                                break;
                            case TextPropertyType.Notes:
                                newSong.Notes = mediaFile.Metadata.Comments;
                                break;
                            case TextPropertyType.Lyrics:
                                newSong.Lyrics = mediaFile.Metadata.Comments;
                                break;
                        }
                    }
                    else
                    {
                        switch (_importPreferences.MapCommentsTo.Item1)
                        {
                            case TextPropertyType.Description:
                                _importState.Album.Description = mediaFile.Metadata.Comments;
                                break;
                            case TextPropertyType.LinerNotes:
                                _importState.Album.LinerNotes = mediaFile.Metadata.Comments;
                                break;
                        }
                    }
                }

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
            var albumTrack = AlbumTrack.Create(_importState.Album, newSong, trackNumber, discNumber);
            
            _songRepository.AddSong(dataSource, newSong);
            _songRepository.AddSongMedia(dataSource, songMedia);
            _albumRepository.AddAlbumTrack(dataSource, albumTrack);

            if (_importPreferences.MapGroupingTo.Item2 == TaxonomyScope.Song)
            {
                if (_importState.Activity != null)
                    _activityRepository.AddActivityTo(dataSource, _importState.Activity, newSong);

                if (_importState.Mood != null)
                    _moodRepository.AddMoodTo(dataSource, _importState.Mood, newSong);

                if (_importState.Tag != null)
                    _tagRepository.AddTagTo(dataSource, _importState.Tag, newSong);
            }

            newSong.Credits = new List<SongCredit>();
            foreach (var composer in _importState.Composers)
            {
                var composerCredit = SongCredit.Create(newSong, composer);
                composerCredit.Roles.Add(Role.Composer);
                _songRepository.AddSongCredit(dataSource, composerCredit);
                _songRepository.AddSongCreditRole(dataSource, composerCredit, Role.Composer);
                newSong.Credits.Add(composerCredit);
            }

            var songKey = newSong.Name.AsKey();
            _importedSongs.Add(songKey, newSong);
            var artistName = _importState.AlbumArtist != null ? _importState.AlbumArtist.Name : null;
            var albumKey = AlbumKey(artistName, _importState.Album.Name);
            _importedAlbums[albumKey].Tracks.Add(albumTrack);
        }

        /// <summary>
        /// Builds an album key from the provided artist and album names.
        /// </summary>
        /// <param name="artistName">The name of the artist.</param>
        /// <param name="albumName">The name of the album.</param>
        private static string AlbumKey(string artistName, string albumName)
        {
            var albumKey = albumName.AsKey();
            if (!string.IsNullOrEmpty(artistName))
                albumKey = string.Format("{0}-{1}", artistName.AsKey(), albumKey);

            return albumKey;
        }

        /// <summary>
        /// Analyzes the import.
        /// </summary>
        /// <param name="copyFiles">If set to <c>true</c> copy media files.</param>
        /// <param name="dataSource">The data source.</param>
        private void AnalyzeImport(bool copyFiles, IDataSource dataSource)
        {
            var assignAlbumArtists = new Dictionary<string, Artist>();
            foreach (var importedAlbum in _importedAlbums.Values.Where(importedAlbum => importedAlbum.Tracks != null && importedAlbum.Tracks.Any()))
            {
                if (string.IsNullOrEmpty(importedAlbum.TrackCounts))
                    importedAlbum.SetTrackCount(1, importedAlbum.Tracks.Count);

                var discCount = importedAlbum.DiscCount;
                var trackCounts = new Dictionary<int, int>();
                var totalTracks = 0;
                for (var i = 1; i <= discCount; i++)
                {
                    var discTracks = importedAlbum.GetTrackCount(i);
                    var trackCount = discTracks == null ? 0 : discTracks.Value;
                    totalTracks += trackCount;
                    trackCounts.Add(i, trackCount);
                }

                Artist lastSongArtist = null;
                var albumArtist = importedAlbum.Artist;
                var isMixedArtist = false;
                var runningLength = 0;
                var digitalFormats = DigitalFormats.Unknown;
                var albumCredits = new List<AlbumCredit>();
                foreach (var albumTrack in importedAlbum.Tracks)
                {
                    var disc = albumTrack.DiscNumber;
                    var track = albumTrack.TrackNumber;
                    var discTrackCount = trackCounts[disc];
                    if (track > discTrackCount)
                        importedAlbum.SetTrackCount(disc, track);

                    var song = albumTrack.Song;
                    var songMedia = song.Media.First();

                    if (song.Artist == null && albumArtist != null)
                        song.Artist = albumArtist;

                    if (lastSongArtist != null && song.ArtistId != lastSongArtist.Id)
                        isMixedArtist = true;

                    if (songMedia != null && songMedia.DigitalFormat != null && !digitalFormats.HasFlag(songMedia.DigitalFormat))
                        digitalFormats = digitalFormats | songMedia.DigitalFormat.Value;

                    foreach (var credit in song.Credits)
                    {
                        var songCredit = credit;
                        var albumCredit = albumCredits.SingleOrDefault(c => c.PersonId == songCredit.PersonId);
                        if (albumCredit == null)
                        {
                            albumCredit = AlbumCredit.Create(importedAlbum, songCredit.Person);
                            albumCredit.Roles.AddRange(songCredit.Roles);
                            albumCredits.Add(albumCredit);
                        }
                        else
                        {
                            var missingRoles = songCredit.Roles.Where(scr => albumCredit.Roles.Any(acr => acr.Id != scr.Id));
                            albumCredit.Roles.AddRange(missingRoles);
                        }
                    }

                    runningLength += song.Length;
                    lastSongArtist = song.Artist;
                }
                importedAlbum.IsComplete = importedAlbum.Tracks.Count >= totalTracks;
                importedAlbum.Length = runningLength;
                importedAlbum.LengthType = DetermineAlbumLengthType(importedAlbum);
                importedAlbum.IsMixedArtist = isMixedArtist;
                if (albumArtist == null && !isMixedArtist)
                    assignAlbumArtists.Add(AlbumKey(null, importedAlbum.Name), lastSongArtist); 

                var albumMedia = AlbumMedia.Create(importedAlbum, digitalFormats);
                _albumRepository.AddAlbumMedia(dataSource, albumMedia);

                foreach (var credit in albumCredits)
                {
                    var albumCredit = credit;
                    _albumRepository.AddAlbumCredit(dataSource, albumCredit);
                    foreach (var role in albumCredit.Roles)
                        _albumRepository.AddAlbumCreditRole(dataSource, albumCredit, role);
                }
            }

            foreach (var albumArtist in assignAlbumArtists)
                ReassignAlbumArtist(albumArtist.Key, albumArtist.Value, copyFiles);
            
            dataSource.Save();
        }

        /// <summary>
        /// Reassigns the album artist.
        /// </summary>
        /// <param name="albumKey">The album key.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="copyFiles">If set to <c>true</c> copy media files.</param>
        private void ReassignAlbumArtist(string albumKey, Artist artist, bool copyFiles)
        {
            var album = _importedAlbums[albumKey];
            if (album == null)
                return;

            album.Artist = artist;

            var newAlbumKey = AlbumKey(artist.Name, album.Name);
            _importedAlbums.Remove(albumKey);
            _importedAlbums.Add(newAlbumKey, album);

            if (copyFiles && album.Tracks != null)
            {
                foreach (var track in album.Tracks)
                {
                    var songMedia = track.Song.Media.FirstOrDefault();
                    if (songMedia == null)
                        continue;

                    songMedia.FilePath = _fileSystemService.MoveMediaFile(_importState.Library, songMedia, album);
                }
            }
            _fileSystemService.MoveAlbumArtwork(_importState.Library, album);
        }

        /// <summary>
        /// Determines the album length type.
        /// </summary>
        /// <param name="album">The album.</param>
        private static LengthType DetermineAlbumLengthType(Album album)
        {
            var discLengthTypes = new List<LengthType>();
            for (var i = 1; i <= album.DiscCount; i++)
                discLengthTypes.Add(DetermineDiscLengthType(album, i));

            if (album.DiscCount == 1)
                return DetermineDiscLengthType(album, 1);

            switch (discLengthTypes.Count(lengthType => lengthType == LengthType.LP))
            {
                case 0:
                    return LengthType.Unknown;
                case 1:
                    return LengthType.LP;
                case 2:
                    return LengthType.DoubleLP;
                case 3:
                    return LengthType.TripleLP;
                default:
                    return LengthType.BoxSet;
            }
        }

        /// <summary>
        /// Determines the disc length type.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="disc">The disc number.</param>
        private static LengthType DetermineDiscLengthType(Album album, int disc)
        {
            var discTrackCount = album.GetTrackCount(disc);
            if (discTrackCount == null)
                return LengthType.Unknown;

            var trackCount = discTrackCount.Value;
            var length = album.GetLength(disc);

            if (trackCount == 0 || length == 0)
                return LengthType.Unknown;

            const int singleMaximumMinutes = 20;
            const int singleMaximumMilliseconds = singleMaximumMinutes * 60 * 1000;
            const int singleMaximumTracks = 5;
            if (length <= singleMaximumMilliseconds && trackCount <= singleMaximumTracks)
                return LengthType.Single;

            const int epMaximumMinutes = 35;
            const int epMaximumMilliseconds = epMaximumMinutes * 60 * 1000;
            const int epMaximumTracks = 8;
            if (length <= epMaximumMilliseconds && trackCount <= epMaximumTracks)
                return LengthType.EP;

            return LengthType.LP;
        }

        /// <summary>
        /// Determines whether this instance is currently running an import.
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            return _isRunning;
        }
    }
}
