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

        /// <summary>
        /// Gets the menu region.
        /// </summary>
        public static string MenuRegion
        {
            get { return Properties.Settings.Default.MenuRegion; }
        }

        /// <summary>
        /// Gets the document region.
        /// </summary>
        public static string DocumentRegion
        {
            get { return Properties.Settings.Default.DocumentRegion; }
        }
    }
}
