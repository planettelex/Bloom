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
using Prism.Events;
using Prism.Regions;
using Settings = Bloom.Common.Settings;

namespace Bloom.Browser.Modules.LibraryModule.Services
{
    /// <summary>
    /// Service for library import operations.
    /// </summary>
    /// <seealso cref="IImportService" />
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
        // ReSharper disable once NotAccessedField.Local
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
        private bool _isRunning;
        private const string Delimiters = ",|;|/";

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State => (BrowserState) _regionManager.Regions[Settings.DocumentRegion].Context;

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
                var dataSource = State.GetConnectionData(libraryId);
                var library = State.GetConnection(libraryId).Library;
                
                foreach (var filePath in mediaFiles)
                {
                    var mediaFile = _fileSystemService.ReadMediaFile(filePath);
                    ImportFile(library, mediaFile, dataSource, copyFiles);
                }

                // AnalyzeImport(copyFiles, dataSource);
            }
            
            _isRunning = false;
            _importPreferences = null;
        }

        /// <summary>
        /// Imports the specified media file to the provided data source.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="mediaFile">The media file.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="copyFile">If set to <c>true</c> copies the media file.</param>
        private void ImportFile(Library library, MediaFile mediaFile, IDataSource dataSource, bool copyFile)
        {
            if (mediaFile == null)
                return;

            Artist songArtist = null;
            Album songAlbum = null;
            Genre songGenre = null;
            List<Person> composers = null;

            if (mediaFile.Metadata != null)
            {
                if (!string.IsNullOrEmpty(mediaFile.Metadata.GenreName))
                    songGenre = ImportGenre(mediaFile.Metadata.GenreName, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.Grouping))
                    ImportTaxonomy(mediaFile.Metadata.Grouping, dataSource);
                
                if (!string.IsNullOrEmpty(mediaFile.Metadata.Composers))
                    composers = ImportComposers(mediaFile.Metadata.Composers, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.ArtistName))
                    songArtist = ImportArtist(mediaFile.Metadata.ArtistName, dataSource);

                Artist albumArtist = null;
                if (!string.IsNullOrEmpty(mediaFile.Metadata.AlbumArtist))
                    albumArtist = ImportArtist(mediaFile.Metadata.AlbumArtist, dataSource);

                if (!string.IsNullOrEmpty(mediaFile.Metadata.AlbumName))
                {
                    songAlbum = ImportAlbum(albumArtist, mediaFile.Metadata.AlbumName, dataSource);
                    // CopyAlbumArtwork(library, songAlbum, mediaFile, dataSource);
                }
            }
            ImportSong(library, songArtist, songAlbum, songGenre, composers, mediaFile, copyFile, dataSource);
        }

        /// <summary>
        /// Imports the genre.
        /// </summary>
        /// <param name="genreName">The name of the genre.</param>
        /// <param name="dataSource">The data source.</param>
        private Genre ImportGenre(string genreName, IDataSource dataSource)
        {
            Genre genreMatch;

            var genreMatches = _genreRepository.FindGenre(dataSource, genreName);
            if (genreMatches == null || !genreMatches.Any())
            {
                genreMatch = Genre.Create(genreName);
                _genreRepository.AddGenre(dataSource, genreMatch);
            }
            else
                genreMatch = genreMatches.First();
            
            return genreMatch;
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
        private Activity ImportActivity(string activityName, IDataSource dataSource)
        {
            Activity activityMatch;

            var activityMatches = _activityRepository.FindActivity(dataSource, activityName);
            if (activityMatches == null || !activityMatches.Any())
            {
                activityMatch = Activity.Create(activityName);
                _activityRepository.AddActivity(dataSource, activityMatch);
            }
            else
                activityMatch = activityMatches.First();

            return activityMatch;
        }

        /// <summary>
        /// Imports the mood.
        /// </summary>
        /// <param name="moodName">The name of the mood.</param>
        /// <param name="dataSource">The data source.</param>
        private Mood ImportMood(string moodName, IDataSource dataSource)
        {
            Mood moodMatch;

            var moodMatches = _moodRepository.FindMood(dataSource, moodName);
            if (moodMatches == null || !moodMatches.Any())
            {
                moodMatch = Mood.Create(moodName);
                _moodRepository.AddMood(dataSource, moodMatch);
            }
            else
                moodMatch = moodMatches.First();
            
            return moodMatch;
        }

        /// <summary>
        /// Imports the tag.
        /// </summary>
        /// <param name="tagName">The name of the tag.</param>
        /// <param name="dataSource">The data source.</param>
        private Tag ImportTag(string tagName, IDataSource dataSource)
        {
            Tag tagMatch;

            var tagMatches = _tagRepository.FindTag(dataSource, tagName);
            if (tagMatches == null || !tagMatches.Any())
            {
                tagMatch = Tag.Create(tagName);
                _tagRepository.AddTag(dataSource, tagMatch);
            }
            else
                tagMatch = tagMatches.First();
            
            return tagMatch;
        }

        /// <summary>
        /// Imports composers from a list of names which can be comma, semi-colon, or forward slash delimited.
        /// </summary>
        /// <param name="peopleNames">A list of people names.</param>
        /// <param name="dataSource">The data source.</param>
        private List<Person> ImportComposers(string peopleNames, IDataSource dataSource)
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

            return allNames.Select(personName => ImportComposer(personName, dataSource)).ToList();
        } 

        /// <summary>
        /// Imports a composer from a name.
        /// </summary>
        /// <param name="personName">The name of the person.</param>
        /// <param name="dataSource">The data source.</param>
        private Person ImportComposer(string personName, IDataSource dataSource)
        {
            ImportRole(Role.Composer, dataSource);
            Person personMatch;

            var personMatches = _personRepository.FindPerson(dataSource, personName);
            if (personMatches == null || !personMatches.Any())
            {
                personMatch = Person.Create(personName);
                _personRepository.AddPerson(dataSource, personMatch);
            }
            else
                personMatch = personMatches.First();

            return personMatch;
        }

        /// <summary>
        /// Imports the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportRole(Role role, IDataSource dataSource)
        {
            if (!_roleRepository.RoleExists(dataSource, role.Id))
                _roleRepository.AddRole(dataSource, role);
        }

        /// <summary>
        /// Imports the artist.
        /// </summary>
        /// <param name="artistName">The scope of the artist.</param>
        /// <param name="dataSource">The data source.</param>
        private Artist ImportArtist(string artistName, IDataSource dataSource)
        {
            Artist artistMatch;

            var artistMatches = _artistRepository.FindArtist(dataSource, artistName);
            if (artistMatches == null || !artistMatches.Any())
            {
                artistMatch = Artist.Create(artistName);
                _artistRepository.AddArtist(dataSource, artistMatch);
            }
            else
                artistMatch = artistMatches.First();

            if (_importPreferences.ComposersAlsoArtistMembers)
            {
                // TODO
            }

            return artistMatch;
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
        /// <param name="albumArtist">The album artist.</param>
        /// <param name="albumName">An album name.</param>
        /// <param name="dataSource">The data source.</param>
        private Album ImportAlbum(Artist albumArtist, string albumName, IDataSource dataSource)
        {
            Album albumMatch;

            var albumMatches = _albumRepository.FindAlbum(dataSource, albumArtist?.Name, albumName);
            if (albumMatches == null || !albumMatches.Any())
            {
                albumName = _importPreferences.TitleCaseAlbumTitles ? albumName.TitleCase() : albumName;
                albumMatch = Album.Create(albumName);
                albumMatch.Artist = albumArtist;
                _albumRepository.AddAlbum(dataSource, albumMatch);
            }
            else
            {
                albumMatch = albumMatches.First();
                albumMatch = _albumRepository.GetAlbum(dataSource, albumMatch.Id);
            }

            /*
            if (_importPreferences.MapGroupingTo.Item2 == TaxonomyScope.Album)
            {
                if (_importState.Activity != null)
                    _activityRepository.AddActivityTo(dataSource, _importState.Activity, albumMatch);

                if (_importState.Mood != null)
                    _moodRepository.AddMoodTo(dataSource, _importState.Mood, albumMatch);

                if (_importState.Tag != null)
                    _tagRepository.AddTagTo(dataSource, _importState.Tag, albumMatch);
            }
            */

            if (albumMatch.Tracks == null)
                albumMatch.Tracks = new List<AlbumTrack>();
            
            return albumMatch;
        }

        /// <summary>
        /// Copies the album artwork.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="album">The album.</param>
        /// <param name="mediaFile">The media file.</param>
        /// <param name="dataSource">The data source.</param>
        private void CopyAlbumArtwork(Library library, Album album, MediaFile mediaFile, IDataSource dataSource)
        {
            AlbumArtwork albumArtwork;
            if (album.Artwork != null && album.Artwork.Any())
            {
                albumArtwork = album.Artwork.First();
                if (File.Exists(albumArtwork.FilePath))
                    return;
            }
                
            var albumArtworkImage = _mediaTagService.ReadMediaImage(mediaFile.Path);
            if (albumArtworkImage == null)
                return;

            var imageFilePath = _fileSystemService.SaveAlbumArtwork(library, albumArtworkImage, album);
            albumArtwork = AlbumArtwork.Create(album, imageFilePath, 1);
            _albumRepository.AddAlbumArtwork(dataSource, albumArtwork);
            album.Artwork = new List<AlbumArtwork> { albumArtwork };
        }

        /// <summary>
        /// Imports the song.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="album">The album.</param>
        /// <param name="genre">The genre.</param>
        /// <param name="composers">The song's composers.</param>
        /// <param name="mediaFile">The media file.</param>
        /// <param name="copyFile">If set to <c>true</c> copies the media file.</param>
        /// <param name="dataSource">The data source.</param>
        private void ImportSong(Library library, Artist artist, Album album, Genre genre, List<Person> composers, MediaFile mediaFile, bool copyFile, IDataSource dataSource)
        {
            var discNumber = 1;
            var trackNumber = 0;
            var songName = mediaFile.Metadata != null ? mediaFile.Metadata.Title : Path.GetFileNameWithoutExtension(mediaFile.Path);
            songName = _importPreferences.TitleCaseSongTitles ? songName.TitleCase() : songName;

            if (artist == null)
            {
                var artists = _artistRepository.FindArtist(dataSource, Bloom.Services.Settings.UnknownName);
                if (artists == null || !artists.Any())
                {
                    artist = Artist.Create(Bloom.Services.Settings.UnknownName);
                    _artistRepository.AddArtist(dataSource, artist);
                }
                else
                    artist = artists.First();
            }

            var newSong = Song.Create(songName, artist);
            newSong.Genre = genre;
            newSong.Length = Convert.ToInt32(mediaFile.Duration.TotalMilliseconds);

            if (album == null)
            {
                var artistName = newSong.Artist != null ? newSong.Artist.Name : Bloom.Services.Settings.UnknownName;
                var albums = _albumRepository.FindAlbum(dataSource, artistName, Bloom.Services.Settings.UnknownName);
                if (albums == null || !albums.Any())
                {
                    album = Album.Create(Bloom.Services.Settings.UnknownName, newSong.Artist);
                    _albumRepository.AddAlbum(dataSource, album);
                }
                else
                    album = albums.First();
            }

            var filePath = mediaFile.Path;
            if (copyFile)
                filePath = _fileSystemService.CopyMediaFile(library, mediaFile, newSong, album);
                
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
                                album.Description = mediaFile.Metadata.Comments;
                                break;
                            case TextPropertyType.LinerNotes:
                                album.LinerNotes = mediaFile.Metadata.Comments;
                                break;
                        }
                    }
                }

                newSong.Bpm = mediaFile.Metadata.Bpm;
                songMedia.BitRate = mediaFile.Bitrate;
                songMedia.SampleRate = mediaFile.SampleRate;

                if (mediaFile.Metadata.DiscNumber != null && mediaFile.Metadata.DiscNumber.Value > 0)
                    discNumber = mediaFile.Metadata.DiscNumber.Value;

                if (mediaFile.Metadata.DiscCount != null && mediaFile.Metadata.DiscCount.Value > 0)
                    album.DiscCount = mediaFile.Metadata.DiscCount.Value;

                if (mediaFile.Metadata.TrackNumber != null && mediaFile.Metadata.TrackNumber.Value > 0)
                    trackNumber = mediaFile.Metadata.TrackNumber.Value;

                if (mediaFile.Metadata.TrackCount != null && mediaFile.Metadata.TrackCount.Value > 0)
                    album.SetTrackCount(discNumber, mediaFile.Metadata.TrackCount.Value);
            }

            newSong.Media = new List<SongMedia> { songMedia };
            _songRepository.AddSong(dataSource, newSong);
            _songRepository.AddSongMedia(dataSource, songMedia);

            if (composers != null)
            {
                newSong.Credits = new List<SongCredit>();
                foreach (var composer in composers)
                {
                    var composerCredit = SongCredit.Create(newSong, composer);
                    composerCredit.Roles.Add(Role.Composer);
                    _songRepository.AddSongCredit(dataSource, composerCredit);
                    _songRepository.AddSongCreditRole(dataSource, composerCredit, Role.Composer);
                    newSong.Credits.Add(composerCredit);
                }
            }

            /*
            if (_importPreferences.MapGroupingTo.Item2 == TaxonomyScope.Song)
            {
                if (_importState.Activity != null)
                    _activityRepository.AddActivityTo(dataSource, _importState.Activity, newSong);

                if (_importState.Mood != null)
                    _moodRepository.AddMoodTo(dataSource, _importState.Mood, newSong);

                if (_importState.Tag != null)
                    _tagRepository.AddTagTo(dataSource, _importState.Tag, newSong);
            }
            */

            var albumTrack = AlbumTrack.Create(album, newSong, trackNumber, discNumber);
            _albumRepository.AddAlbumTrack(dataSource, albumTrack);
            album.Tracks.Add(albumTrack);
        }

        /// <summary>
        /// Analyzes the import: Sets album track and disc count. Sets album artist if null and all one artist, or mixed artist flag if not. Sets album length totals.
        /// Sets album credits
        /// </summary>
        /// <param name="copyFiles">If set to <c>true</c> copy media files.</param>
        /// <param name="dataSource">The data source.</param>
        private void AnalyzeImport(bool copyFiles, IDataSource dataSource)
        {
            foreach (var importedAlbum in new List<Album>())
            {
                if (string.IsNullOrEmpty(importedAlbum.TrackCounts))
                    importedAlbum.SetTrackCount(1, importedAlbum.Tracks.Count);

                var discCount = importedAlbum.DiscCount;
                var trackCounts = new Dictionary<int, int>();
                var totalTracks = 0;
                for (var i = 1; i <= discCount; i++)
                {
                    var discTracks = importedAlbum.GetTrackCount(i);
                    var trackCount = discTracks ?? 0;
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

                    if (songMedia?.DigitalFormat != null && !digitalFormats.HasFlag(songMedia.DigitalFormat))
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
                // if (albumArtist == null && !isMixedArtist)
                //     assignAlbumArtists.Add(AlbumKey(null, importedAlbum.Name), lastSongArtist); 

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
            
            dataSource.Save();
        }

        /// <summary>
        /// Reassigns the album artist.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="album">The album.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="copyFiles">If set to <c>true</c> copy media files.</param>
        private void ReassignAlbumArtist(Library library, Album album, Artist artist, bool copyFiles)
        {
            if (album == null)
                return;

            album.Artist = artist;

            if (copyFiles && album.Tracks != null)
            {
                foreach (var track in album.Tracks)
                {
                    var songMedia = track.Song.Media.FirstOrDefault();
                    if (songMedia == null)
                        continue;

                    songMedia.FilePath = _fileSystemService.MoveMediaFile(library, songMedia, album);
                }
            }
            _fileSystemService.MoveAlbumArtwork(library, album);
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
