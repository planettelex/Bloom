using System.Windows;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// State properties common to all applications.
    /// </summary>
    public interface IApplicationState
    {
        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the data connections.
        /// </summary>
        Connections Connections { get; set; }

        /// <summary>
        /// Gets or sets the name of the skin.
        /// </summary>
        string SkinName { get; set; }

        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        WindowState WindowState { get; set; }
    }
}
