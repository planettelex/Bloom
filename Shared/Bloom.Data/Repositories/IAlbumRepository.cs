using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for album data.
    /// </summary>
    public interface IAlbumRepository
    {
        /// <summary>
        /// Gets the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumId">The album identifier.</param>
        Album GetAlbum(IDataSource dataSource, Guid albumId);

        /// <summary>
        /// Gets the album release.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumReleaseId">The album release identifier.</param>
        AlbumRelease GetAlbumRelease(IDataSource dataSource, Guid albumReleaseId);

        /// <summary>
        /// Lists the albums.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Album> ListAlbums(IDataSource dataSource);

        /// <summary>
        /// Lists the albums for a given artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artistId">The artist identifier.</param>
        List<Album> ListArtistAlbums(IDataSource dataSource, Guid artistId);

        /// <summary>
        /// Lists the album releases.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumId">The album identifier.</param>
        List<AlbumRelease> ListAlbumReleases(IDataSource dataSource, Guid albumId);

        /// <summary>
        /// Adds the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        void AddAlbum(IDataSource dataSource, Album album);

        /// <summary>
        /// Adds a track to the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumTrack">The album track.</param>
        void AddAlbumTrack(IDataSource dataSource, AlbumTrack albumTrack);

        /// <summary>
        /// Deletes an album track.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumTrack">The album track.</param>
        void DeleteAlbumTrack(IDataSource dataSource, AlbumTrack albumTrack);

        /// <summary>
        /// Adds album artwork.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumArtwork">The album artwork.</param>
        void AddAlbumArtwork(IDataSource dataSource, AlbumArtwork albumArtwork);

        /// <summary>
        /// Deletes album artwork.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumArtwork">The album artwork.</param>
        void DeleteAlbumArtwork(IDataSource dataSource, AlbumArtwork albumArtwork);

        /// <summary>
        /// Adds an album release.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumRelease">The album release.</param>
        void AddAlbumRelease(IDataSource dataSource, AlbumRelease albumRelease);

        /// <summary>
        /// Deletes an album release.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumRelease">The album release.</param>
        void DeleteAlbumRelease(IDataSource dataSource, AlbumRelease albumRelease);

        /// <summary>
        /// Adds an album collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCollaborator">The album collaborator.</param>
        void AddAlbumCollaborator(IDataSource dataSource, AlbumCollaborator albumCollaborator);

        /// <summary>
        /// Deletes an album collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCollaborator">The album collaborator.</param>
        void DeleteAlbumCollaborator(IDataSource dataSource, AlbumCollaborator albumCollaborator);

        /// <summary>
        /// Adds an album credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCredit">The album credit.</param>
        void AddAlbumCredit(IDataSource dataSource, AlbumCredit albumCredit);

        /// <summary>
        /// Deletes an album credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCredit">The album credit.</param>
        void DeleteAlbumCredit(IDataSource dataSource, AlbumCredit albumCredit);

        /// <summary>
        /// Adds an album credit role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCredit">The album credit.</param>
        /// <param name="role">The role.</param>
        void AddAlbumCreditRole(IDataSource dataSource, AlbumCredit albumCredit, Role role);

        /// <summary>
        /// Deletes an album credit role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCredit">The album credit.</param>
        /// <param name="role">The role.</param>
        void DeleteAlbumCreditRole(IDataSource dataSource, AlbumCredit albumCredit, Role role);

        /// <summary>
        /// Deletes the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        void DeleteAlbum(IDataSource dataSource, Album album);
    }
}
