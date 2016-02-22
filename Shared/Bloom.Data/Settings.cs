using System;
using System.IO;

namespace Bloom.Data
{
    /// <summary>
    /// Settings for Bloom.State.Data.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Gets the local data path.
        /// </summary>
        public static string LocalDataPath
        {
            get
            {
                var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(appDataFolder, Properties.Settings.Default.LocalFolder);
            }
        }

        /// <summary>
        /// Gets the tests data path.
        /// </summary>
        public static string TestsDataPath
        {
            get { return Path.Combine(LocalDataPath, Properties.Settings.Default.TestsFolder); }
        }
    }
}
