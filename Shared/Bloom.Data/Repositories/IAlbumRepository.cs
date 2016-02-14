using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IAlbumRepository
    {
        Album GetAlbum(IDataSource dataSource, Guid albumId);

        List<Album> ListAlbums(IDataSource dataSource);

        void AddAlbum(IDataSource dataSource, Album album);

        void AddAlbumTrack(IDataSource dataSource, AlbumTrack albumTrack);

        void AddAlbumArtwork(IDataSource dataSource, AlbumArtwork albumArtwork);

        void AddAlbumCollaborator(IDataSource dataSource, AlbumCollaborator songCollaborator);

        void AddAlbumCredit(IDataSource dataSource, AlbumCredit songCredit);

        void DeleteAlbum(IDataSource dataSource, Album album);
    }
}
