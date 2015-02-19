namespace Bloom.Common
{
    public static class Settings
    {
        public static string LibraryFileExtension
        {
            get { return Properties.Settings.Default.LibraryFileExtension; }
        }

        public static string StateFileExtension
        {
            get { return Properties.Settings.Default.StateFileExtension; }
        }
    }
}
