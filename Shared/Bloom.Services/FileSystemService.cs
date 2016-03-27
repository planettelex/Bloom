using System;
using System.IO;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    /// <summary>
    /// Service for file system operations.
    /// </summary>
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
        public string CopyProfileImage(User user, string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            var fileName = (user.PersonId.ToString() + DateTime.Now.ToFileTime()).Replace("-", "");
            var newPath = Path.Combine(_userProfilesFolder, fileName + fileExtension);
            File.Copy(filePath, newPath, true);
            return newPath;
        }
    }
}
