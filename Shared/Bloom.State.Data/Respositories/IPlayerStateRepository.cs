using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the player state.
    /// </summary>
    public interface IPlayerStateRepository
    {
        /// <summary>
        /// Determines whether the player state exists.
        /// </summary>
        /// <returns></returns>
        bool PlayerStateExists(User user);

        /// <summary>
        /// Gets the state of the player.
        /// </summary>
        PlayerState GetPlayerState(User user);

        /// <summary>
        /// Adds the state of the player.
        /// </summary>
        /// <param name="playerState">State of the player.</param>
        void AddPlayerState(PlayerState playerState);
    }
}
