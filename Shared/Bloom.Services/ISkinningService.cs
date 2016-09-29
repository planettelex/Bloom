namespace Bloom.Services
{
    /// <summary>
    /// Service for managing named collections of brushes and icons.
    /// </summary>
    public interface ISkinningService
    {
        /// <summary>
        /// Sets the skin.
        /// </summary>
        /// <param name="skinName">The name of the skin.</param>
        void SetSkin(string skinName);
    }
}
