using System;
using System.Collections.Generic;
using System.IO;
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
            var localDataFolder = Data.Settings.LocalDataPath;
            _userProfilesFolder = Path.Combine(localDataFolder, Properties.Settings.Default.UserProfilesFolder);
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
        /// Lists all music files under the specified directory, looking in every subdirectory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>A list of music files.</returns>
        public List<string> ListMusicFiles(string directoryPath)
        {
            return null; //TODO: Implement 
        }
    }
}
