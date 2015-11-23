namespace Bloom.Common
{
    /// <summary>
    /// Global suite settings.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Gets the library file type extension.
        /// </summary>
        public static string LibraryFileExtension
        {
            get { return Properties.Settings.Default.LibraryFileExtension; }
        }

        /// <summary>
        /// Gets the state file type extension.
        /// </summary>
        public static string StateFileExtension
        {
            get { return Properties.Settings.Default.StateFileExtension; }
        }
    }
}
