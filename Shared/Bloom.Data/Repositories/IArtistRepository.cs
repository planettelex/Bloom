using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IArtistRepository
    {
        Artist GetArtist(IDataSource dataSource, Guid artistId);

        List<Artist> ListArtists(IDataSource dataSource);

        void AddArtist(IDataSource dataSource, Artist artist);

        void AddArtistMember(IDataSource dataSource, ArtistMember member);

        void AddArtistPhoto(IDataSource dataSource, Artist artist, Photo photo, int priority);

        void AddArtistReference(IDataSource dataSource, Artist artist, Reference reference);

        void DeleteArtist(IDataSource dataSource, Artist artist);
    }
}
