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
        public static string LibraryFileExtension => Properties.Settings.Default.LibraryFileExtension;

        /// <summary>
        /// Gets the state file type extension.
        /// </summary>
        public static string StateFileExtension => Properties.Settings.Default.StateFileExtension;

        /// <summary>
        /// Gets the menu region.
        /// </summary>
        public static string MenuRegion => Properties.Settings.Default.MenuRegion;

        /// <summary>
        /// Gets the document region.
        /// </summary>
        public static string DocumentRegion => Properties.Settings.Default.DocumentRegion;
    }
}
