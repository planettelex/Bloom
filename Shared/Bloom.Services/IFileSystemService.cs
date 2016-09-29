using System.Collections.Generic;
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
        /// Lists all music files under the specified directory, looking in every subdirectory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>A list of music files.</returns>
        List<string> ListMusicFiles(string directoryPath);
    }
}
