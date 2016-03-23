using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for playlist data.
    /// </summary>
    public interface IPlaylistRepository
    {
        /// <summary>
        /// Gets the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistId">The playlist identifier.</param>
        Playlist GetPlaylist(IDataSource dataSource, Guid playlistId);

        /// <summary>
        /// Lists the playlists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Playlist> ListPlaylists(IDataSource dataSource);

        /// <summary>
        /// Adds the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        void AddPlaylist(IDataSource dataSource, Playlist playlist);

        /// <summary>
        /// Adds a track to the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistTrack">The playlist track.</param>
        void AddPlaylistTrack(IDataSource dataSource, PlaylistTrack playlistTrack);

        /// <summary>
        /// Deletes a playlist track.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistTrack">The playlist track.</param>
        void DeletePlaylistTrack(IDataSource dataSource, PlaylistTrack playlistTrack);

        /// <summary>
        /// Adds playlist artwork.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistArtwork">The playlist artwork.</param>
        void AddPlaylistArtwork(IDataSource dataSource, PlaylistArtwork playlistArtwork);

        /// <summary>
        /// Deletes playlist artwork.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlistArtwork">The playlist artwork.</param>
        void DeletePlaylistArtwork(IDataSource dataSource, PlaylistArtwork playlistArtwork);

        /// <summary>
        /// Deletes the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        void DeletePlaylist(IDataSource dataSource, Playlist playlist);
    }
}
