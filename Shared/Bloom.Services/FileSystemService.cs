using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Bloom.Common.ExtensionMethods;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    /// <summary>
    /// Service for file system operations.
    /// </summary>
    /// <seealso cref="Bloom.Services.IFileSystemService" />
    public class FileSystemService : IFileSystemService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemService"/> class.
        /// </summary>
        public FileSystemService()
        {
            _userProfilesFolder = Settings.UserProfilesPath;
            if(!Directory.Exists(_userProfilesFolder))
                Directory.CreateDirectory(_userProfilesFolder);
        }
        private readonly string _userProfilesFolder;

        /// <summary>
        /// Copies a user's profile image to local storage.
        /// </summary>
        /// <param name="user">A user.</param>
        /// <param name="filePath">The profile image file path.</param>
        /// <returns>The new file path.</returns>
        public string CopyProfileImage(User user, string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            var fileName = (user.PersonId.ToString() + DateTime.Now.ToFileTime()).Replace("-", "");
            var newPath = Path.Combine(_userProfilesFolder, fileName + fileExtension);
            File.Copy(filePath, newPath, true);
            return newPath;
        }

        /// <summary>
        /// Lists all music files under the specified folder, looking in every sub folder.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <returns>A list of music files.</returns>
        public List<string> ListMusicFiles(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException(folderPath + " was not found.");

            var allFiles = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories);
            var regEx = MusicFileRegEx();
            // ReSharper disable once AssignNullToNotNullAttribute
            var musicFiles = allFiles.Where(file => regEx.IsMatch(Path.GetExtension(file)) && 
                !new FileInfo (file).Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System));

            return musicFiles.ToList();
        }

        /// <summary>
        /// Reads the media file at the given path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        public MediaFile ReadMediaFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var file = TagLib.File.Create(filePath);
            if (file == null)
                return null;

            var mediaFile = new MediaFile(filePath);
            var fileExtension = Path.GetExtension(filePath);
            fileExtension = fileExtension?.TrimStart('.');

            DigitalFormats format;
            if (!Enum.TryParse(fileExtension, true, out format))
                format = DigitalFormats.Unknown;

            mediaFile.Size = new FileInfo(filePath).Length;
            
            if (file.Properties != null)
            {
                mediaFile.Bitrate = file.Properties.AudioBitrate;
                mediaFile.SampleRate = file.Properties.AudioSampleRate;
                mediaFile.Duration = file.Properties.Duration;
                mediaFile.Format = format;
            }

            if (file.Tag != null && !file.Tag.IsEmpty)
            {
                mediaFile.Metadata = new MediaTag
                {
                    Title = file.Tag.Title,
                    AlbumName = file.Tag.Album,
                    ArtistName = file.Tag.FirstPerformer,
                    AlbumArtist = file.Tag.FirstAlbumArtist,
                    GenreName = file.Tag.FirstGenre,
                    Comments = file.Tag.Comment,
                    TrackNumber = (int?) file.Tag.Track,
                    TrackCount = (int?) file.Tag.TrackCount,
                    DiscNumber = (int?) file.Tag.Disc,
                    DiscCount = (int?) file.Tag.DiscCount,
                    Grouping = file.Tag.Grouping,
                    Year = (int?) file.Tag.Year,
                    Composers = file.Tag.Composers != null && file.Tag.Composers.Any() ? file.Tag.Composers.First() : null,
                    Bpm = file.Tag.BeatsPerMinute == 0 ? (double?) null : file.Tag.BeatsPerMinute
                };
            }

            return mediaFile;
        }

        /// <summary>
        /// Creates a library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        public string CreateFolder(Library library)
        {
            if (!Directory.Exists(library.FolderPath))
                Directory.CreateDirectory(library.FolderPath);

            CreateLibraryPeopleFolder(library);
            CreateLibraryArtistsFolders(library);
            CreateLibraryPlaylistFolder(library);

            return library.FolderPath;
        }

        /// <summary>
        /// Creates a library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="artist">An artist.</param>
        public string CreateFolder(Library library, Artist artist)
        {
            CreateLibraryArtistsFolders(library);
            var artistsFolder = LibraryArtistsFolderPath(library);
            var artistFolderName = MakeFolderName(artist.Name);
            var artistFolderPath = Path.Combine(artistsFolder, artistFolderName);

            if (!Directory.Exists(artistFolderPath))
                Directory.CreateDirectory(artistFolderPath);

            return artistFolderPath;
        }

        /// <summary>
        /// Creates a library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="album">An album.</param>
        public string CreateFolder(Library library, Album album)
        {
            string albumsFolder;
            if (album.Artist != null)
                albumsFolder = CreateFolder(library, album.Artist);
            else
            {
                CreateLibraryArtistsFolders(library);
                albumsFolder = LibraryMixedArtistsFolderPath(library);
            }
            var albumFolderName = MakeFolderName(album.Name);
            var albumFolderPath = Path.Combine(albumsFolder, albumFolderName);

            if (!Directory.Exists(albumFolderPath))
                Directory.CreateDirectory(albumFolderPath);

            return albumFolderPath;
        }

        /// <summary>
        /// Creates a library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="person">A person.</param>
        public string CreateFolder(Library library, Person person)
        {
            CreateLibraryPeopleFolder(library);
            var peopleFolder = LibraryPeopleFolderPath(library);
            var personFolderName = MakeFolderName(person.Name);
            var personFolderPath = Path.Combine(peopleFolder, personFolderName);

            if (!Directory.Exists(personFolderPath))
                Directory.CreateDirectory(personFolderPath);

            return personFolderPath;
        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="playlist">A playlist.</param>
        public string CreateFolder(Library library, Playlist playlist)
        {
            CreateLibraryPlaylistFolder(library);
            var playlistsFolder = LibraryPlaylistsFolderPath(library);
            var playlistFolderName = MakeFolderName(playlist.Name);
            var playlistFolderPath = Path.Combine(playlistsFolder, playlistFolderName);

            if (!Directory.Exists(playlistFolderPath))
                Directory.CreateDirectory(playlistFolderPath);

            return playlistFolderPath;
        }

        /// <summary>
        /// Moves the media file to the provided album's folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="songMedia">The song media.</param>
        /// <param name="album">The album to move the media to.</param>
        public string MoveMediaFile(Library library, SongMedia songMedia, Album album)
        {
            if (string.IsNullOrEmpty(songMedia?.FilePath) || !File.Exists(songMedia.FilePath))
                return null;

            var fileName = Path.GetFileName(songMedia.FilePath);
            var albumFolderPath = CreateFolder(library, album);
            var newFilePath = SafeFilePath(albumFolderPath, fileName);

            File.Move(songMedia.FilePath, newFilePath);

            return newFilePath;
        }

        /// <summary>
        /// Moves the album artwork from its current location to the one specified by the provided album's data.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="album">An album.</param>
        public void MoveAlbumArtwork(Library library, Album album)
        {
            if (album.Artwork == null || !album.Artwork.Any())
                return;

            var albumFolderPath = CreateFolder(library, album);
            var oldAlbumFolderPath = string.Empty;
            foreach (var albumArtwork in album.Artwork)
            {
                var fileName = Path.GetFileName(albumArtwork.FilePath);
                if (fileName == null)
                    continue;

                var newFilePath = SafeFilePath(albumFolderPath, fileName);

                oldAlbumFolderPath = Path.GetDirectoryName(albumArtwork.FilePath);
                File.Move(albumArtwork.FilePath, newFilePath);
                albumArtwork.FilePath = newFilePath;
            }

            if (string.IsNullOrEmpty(oldAlbumFolderPath)) 
                return;

            // Delete the old folder if its empty.
            var directoryItems = Directory.GetFileSystemEntries(oldAlbumFolderPath);
            if (!directoryItems.Any())
                Directory.Delete(oldAlbumFolderPath);
        }

        /// <summary>
        /// Copies a media file to an album library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="song">The song.</param>
        /// <param name="album">An album.</param>
        public string CopyMediaFile(Library library, MediaFile sourceFile, Song song, Album album)
        {
            var albumFolderPath = CreateFolder(library, album);
            var songFileName = MakeFileName(sourceFile, song);
            var songFilePath = Path.Combine(albumFolderPath, songFileName);

            var i = 1;
            while (File.Exists(songFilePath))
            {
                songFileName += $"_{i.ToString(CultureInfo.InvariantCulture)}";
                songFilePath = Path.Combine(albumFolderPath, songFileName);
                i++;
            }

            File.Copy(sourceFile.Path, songFilePath);

            return songFilePath;
        }

        /// <summary>
        /// Copies a media file to a playlist library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="song">The song.</param>
        /// <param name="playlist">A playlist.</param> 
        public string CopyMediaFile(Library library, MediaFile sourceFile, Song song, Playlist playlist)
        {
            var playlistFolderPath = CreateFolder(library, playlist);
            var songFileName = MakeFileName(sourceFile, song);
            var songFilePath = Path.Combine(playlistFolderPath, songFileName);

            var i = 1;
            while (File.Exists(songFilePath))
            {
                songFileName += $"_{i.ToString(CultureInfo.InvariantCulture)}";
                songFilePath = Path.Combine(playlistFolderPath, songFileName);
                i++;
            }

            File.Copy(sourceFile.Path, songFilePath);

            return songFilePath;
        }

        /// <summary>
        /// Saves the album image.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="image">The image.</param>
        /// <param name="album">The album.</param>
        public string SaveAlbumImage(Library library, Image image, Album album)
        {
            if (image == null)
                return null;

            var albumFolderPath = CreateFolder(library, album);
            var imageFileName = MakeImageFileName(album);
            var imageFilePath = Path.Combine(albumFolderPath, imageFileName);

            image.Save(imageFilePath, ImageFormat.Png);

            return imageFilePath;
        }

        /// <summary>
        /// Creates a file path that is safe to move or copy to.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileName">Name of the file.</param>
        private static string SafeFilePath(string folderPath, string fileName)
        {
            var filePath = Path.Combine(folderPath, fileName);

            var i = 1;
            while (File.Exists(filePath))
            {
                fileName += $"_{i.ToString(CultureInfo.InvariantCulture)}";
                filePath = Path.Combine(folderPath, fileName);
                i++;
            }

            return filePath;
        }

        /// <summary>
        /// Creates a regular expression based on the non-zero values of <see cref="Bloom.Domain.Enums.DigitalFormats"/>.
        /// </summary>
        private static Regex MusicFileRegEx()
        {
            var allFormats = (DigitalFormats[]) Enum.GetValues(typeof (DigitalFormats));
            var regExPattern = string.Empty;
            foreach (var extension in allFormats.Where(format => format != DigitalFormats.Unknown))
                regExPattern += "\\." + extension + "|";
            
            regExPattern = regExPattern.Trim('|');

            return new Regex(regExPattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Creates the library people folder.
        /// </summary>
        /// <param name="library">The library.</param>
        private static void CreateLibraryPeopleFolder(Library library)
        {
            var peopleFolder = LibraryPeopleFolderPath(library);
            if (!Directory.Exists(peopleFolder))
                Directory.CreateDirectory(peopleFolder);
        }

        /// <summary>
        /// Gets the library people folder path.
        /// </summary>
        /// <param name="library">The library.</param>
        private static string LibraryPeopleFolderPath(Library library)
        {
            return Path.Combine(library.FolderPath, Settings.PeopleLibraryFolder);
        }

        /// <summary>
        /// Creates the library artists folders.
        /// </summary>
        /// <param name="library">The library.</param>
        private static void CreateLibraryArtistsFolders(Library library)
        {
            var artistsFolder = LibraryArtistsFolderPath(library);
            if (!Directory.Exists(artistsFolder))
                Directory.CreateDirectory(artistsFolder);

            var mixedArtistFolder = LibraryMixedArtistsFolderPath(library);
            if (!Directory.Exists(mixedArtistFolder))
                Directory.CreateDirectory(mixedArtistFolder);
        }

        /// <summary>
        /// Gets the library artists folder path.
        /// </summary>
        /// <param name="library">The library.</param>
        private static string LibraryArtistsFolderPath(Library library)
        {
            return Path.Combine(library.FolderPath, Settings.ArtistsLibraryFolder);
        }

        /// <summary>
        /// Gets the library mixed artists folder path.
        /// </summary>
        /// <param name="library">The library.</param>
        private static string LibraryMixedArtistsFolderPath(Library library)
        {
            var artistsFolder = LibraryArtistsFolderPath(library);
            return Path.Combine(artistsFolder, Settings.MixedArtistsLibraryFolder);
        }

        /// <summary>
        /// Creates the library playlist folder.
        /// </summary>
        /// <param name="library">The library.</param>
        private static void CreateLibraryPlaylistFolder(Library library)
        {
            var playlistsFolder = LibraryPlaylistsFolderPath(library);
            if (!Directory.Exists(playlistsFolder))
                Directory.CreateDirectory(playlistsFolder);
        }

        /// <summary>
        /// Gets the library artists folder path.
        /// </summary>
        /// <param name="library">The library.</param>
        private static string LibraryPlaylistsFolderPath(Library library)
        {
            return Path.Combine(library.FolderPath, Settings.PlaylistsLibraryFolder);
        }

        /// <summary>
        /// Makes a folder name from a provided name.
        /// </summary>
        /// <param name="name">The name.</param>
        private static string MakeFolderName(string name)
        {
            return name.AsFolderName();
        }

        /// <summary>
        /// Makes a file name from the provided media file and song.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="song">The song.</param>
        private static string MakeFileName(MediaFile sourceFile, Song song)
        {
            var fileName = song.Name.AsFileName();
            if (sourceFile.Metadata?.TrackNumber != null)
            {
                var trackNumber = sourceFile.Metadata.TrackNumber.Value < 100 ? sourceFile.Metadata.TrackNumber.Value.ToString("D2") : 
                                                                                sourceFile.Metadata.TrackNumber.Value.ToString("D3");

                fileName = trackNumber + " " + fileName;
            }

            var extension = Path.GetExtension(sourceFile.Path);
            if (!string.IsNullOrEmpty(extension))
                fileName += extension.ToLower();

            return fileName;
        }

        /// <summary>
        /// Makes a file name from the provided album.
        /// </summary>
        /// <param name="album">The album.</param>
        private static string MakeImageFileName(Album album)
        {
            if (album.Artwork == null || !album.Artwork.Any())
                return "Cover.png";

            var pageNumber = album.Artwork.Count + 1;
            return $"Page {pageNumber:D2}.png";
        }
    }
}
