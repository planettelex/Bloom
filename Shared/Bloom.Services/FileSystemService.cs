using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Bloom.Domain.Enums;
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
        /// Lists all music files under the specified directory, looking in every subdirectory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>A list of music files.</returns>
        public List<string> ListMusicFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException(directoryPath + " was not found.");

            var allFiles = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories);
            var regEx = MusicFileRegEx();
            // ReSharper disable once AssignNullToNotNullAttribute
            var musicFiles = allFiles.Where(file => regEx.IsMatch(Path.GetExtension(file)));

            return musicFiles.ToList();
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
    }
}
