using System;
using System.IO;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public class FileSystemService : IFileSystemService
    {
        public FileSystemService()
        {
            var localDataFolder = State.Data.Settings.LocalDataPath;
            _userProfilesFolder = Path.Combine(localDataFolder, Properties.Settings.Default.UserProfilesFolder);
            if(!Directory.Exists(_userProfilesFolder))
                Directory.CreateDirectory(_userProfilesFolder);
        }
        private readonly string _userProfilesFolder;

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
