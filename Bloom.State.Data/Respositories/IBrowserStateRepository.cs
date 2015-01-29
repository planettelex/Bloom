using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the browser state.
    /// </summary>
    public interface IBrowserStateRepository
    {
        /// <summary>
        /// Gets the browser state.
        /// </summary>
        BrowserState GetBrowserState();
    }
}
