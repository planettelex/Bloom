using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for player state data.
    /// </summary>
    public interface IPlayerStateRepository
    {
        /// <summary>
        /// Determines whether the player state exists.
        /// </summary>
        /// <param name="user">The user.</param>
        bool PlayerStateExists(User user);

        /// <summary>
        /// Gets player state.
        /// </summary>
        /// <param name="user">The user.</param>
        PlayerState GetPlayerState(User user);

        /// <summary>
        /// Adds the player state.
        /// </summary>
        /// <param name="playerState">The player state.</param>
        void AddPlayerState(PlayerState playerState);
    }
}
