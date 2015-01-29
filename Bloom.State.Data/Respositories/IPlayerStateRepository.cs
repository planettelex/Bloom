using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the player state.
    /// </summary>
    public interface IPlayerStateRepository
    {
        /// <summary>
        /// Gets the state of the player.
        /// </summary>
        PlayerState GetPlayerState();
    }
}
