using System.Collections.Generic;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    /// <summary>
    /// Service for file system operations.
    /// </summary>
    public interface IFileSystemService
    {
        /// <summary>
        /// Copies a user's profile image to local storage.
        /// </summary>
        /// <param name="user">A user.</param>
        /// <param name="filePath">The profile image file path.</param>
        /// <returns>The new file path.</returns>
        string CopyProfileImage(User user, string filePath);

        /// <summary>
        /// Lists all music files under the specified folder, looking in every sub folder.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        List<string> ListMusicFiles(string folderPath);

        /// <summary>
        /// Reads the media file at the given path.
        /// </summary>
        /// <param name="filePath">The media's file path.</param>
        MediaFile ReadMediaFile(string filePath);

        /// <summary>
        /// Creates a library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        string CreateFolder(Library library);

        /// <summary>
        /// Creates a library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="artist">An artist.</param>
        string CreateFolder(Library library, Artist artist);

        /// <summary>
        /// Creates a library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="album">An album.</param>
        string CreateFolder(Library library, Album album);

        /// <summary>
        /// Creates a library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="person">A person.</param>
        string CreateFolder(Library library, Person person);

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="playlist">A playlist.</param>
        string CreateFolder(Library library, Playlist playlist);

        /// <summary>
        /// Copies a media file to an album library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="song">The song.</param>
        /// <param name="album">An album.</param>
        string CopyMediaFile(Library library, MediaFile sourceFile, Song song, Album album);

        /// <summary>
        /// Copies a media file to a playlist library folder.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="song">The song.</param>
        /// <param name="playlist">A playlist.</param>
        string CopyMediaFile(Library library, MediaFile sourceFile, Song song, Playlist playlist);
    }
}
