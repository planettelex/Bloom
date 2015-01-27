namespace Bloom.Services
{
    /// <summary>
    /// Service for interfacing with system processes.
    /// </summary>
    public interface IProcessService
    {
        /// <summary>
        /// Brings the browser process to the foreground, or starts a new one if one isn't already running.
        /// </summary>
        void GoToBrowserProcess();

        /// <summary>
        /// Brings the player process to the foreground, or starts a new one if one isn't already running.
        /// </summary>
        void GoToPlayerProcess();

        /// <summary>
        /// Brings the analytics process to the foreground, or starts a new one if one isn't already running.
        /// </summary>
        void GoToAnalyticsProcess();
    }
}
