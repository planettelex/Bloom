namespace Bloom.Services
{
    /// <summary>
    /// Service for managing skins, which are named collections of brushes and icons.
    /// </summary>
    public interface ISkinningService
    {
        /// <summary>
        /// Sets the skin.
        /// </summary>
        /// <param name="skinName">Name of the skin.</param>
        void SetSkin(string skinName);
    }
}
