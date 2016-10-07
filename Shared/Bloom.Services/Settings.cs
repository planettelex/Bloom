using System.IO;

namespace Bloom.Services
{
    /// <summary>
    /// Settings for Bloom.Services.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Gets the local data path.
        /// </summary>
        public static string LocalDataPath
        {
            get { return Data.Settings.LocalDataPath; }
        }

        /// <summary>
        /// Gets the tests data path.
        /// </summary>
        public static string TestsDataPath
        {
            get { return Data.Settings.TestsDataPath; }
        }

        /// <summary>
        /// Gets the browser executable path.
        /// </summary>
        public static string BrowserExecutablePath
        {
            get { return Properties.Settings.Default.BrowserExecutablePath; }
        }

        /// <summary>
        /// Gets the name of the browser process.
        /// </summary>
        public static string BrowserProcessName
        {
            get { return Properties.Settings.Default.BrowserProcessName; }
        }

        /// <summary>
        /// Gets the player executable path.
        /// </summary>
        public static string PlayerExecutablePath
        {
            get { return Properties.Settings.Default.PlayerExecutablePath; }
        }

        /// <summary>
        /// Gets the name of the player process.
        /// </summary>
        public static string PlayerProcessName
        {
            get { return Properties.Settings.Default.PlayerProcessName; }
        }

        /// <summary>
        /// Gets the analytics executable path.
        /// </summary>
        public static string AnalyticsExecutablePath
        {
            get { return Properties.Settings.Default.AnalyticsExecutablePath; }
        }

        /// <summary>
        /// Gets the name of the analytics process.
        /// </summary>
        public static string AnalyticsProcessName
        {
            get { return Properties.Settings.Default.AnalyticsProcessName; }
        }

        /// <summary>
        /// Gets the user profiles folder.
        /// </summary>
        public static string UserProfilesPath
        {
            get { return Path.Combine(LocalDataPath, Properties.Settings.Default.UserProfilesFolder); }
        }

        /// <summary>
        /// Gets the people library folder.
        /// </summary>
        public static string PeopleLibraryFolder
        {
            get { return Properties.Settings.Default.PeopleFolder; }
        }

        /// <summary>
        /// Gets the artists library folder.
        /// </summary>
        public static string ArtistsLibraryFolder
        {
            get { return Properties.Settings.Default.ArtistsFolder; }
        }

        /// <summary>
        /// Gets the mixed artists library folder.
        /// </summary>
        public static string MixedArtistsLibraryFolder
        {
            get { return Properties.Settings.Default.MixedArtistFolder; }
        }

        /// <summary>
        /// Gets the playlists library folder.
        /// </summary>
        public static string PlaylistsLibraryFolder
        {
            get { return Properties.Settings.Default.PlaylistsFolder; }
        }
    }
}
