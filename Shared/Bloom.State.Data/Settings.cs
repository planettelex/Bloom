using System;
using System.IO;

namespace Bloom.State.Data
{
    public static class Settings
    {
        public static string LocalDataPath
        {
            get
            {
                var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(appDataFolder, Properties.Settings.Default.LocalFolder);
            }
        }
    }
}
