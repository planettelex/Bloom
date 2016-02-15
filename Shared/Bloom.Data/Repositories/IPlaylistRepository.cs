using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IPlaylistRepository
    {
        Playlist GetPlaylist(IDataSource dataSource, Guid playlistId);

        List<Playlist> ListPlaylists(IDataSource dataSource);

        void AddPlaylist(IDataSource dataSource, Playlist playlist);

        void AddPlaylistTrack(IDataSource dataSource, PlaylistTrack playlistTrack);

        void AddPlaylistArtwork(IDataSource dataSource, PlaylistArtwork playlistArtwork);

        void DeletePlaylist(IDataSource dataSource, Playlist playlist);
    }
}
