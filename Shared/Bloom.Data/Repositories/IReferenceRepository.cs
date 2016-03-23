using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for reference data.
    /// </summary>
    public interface IReferenceRepository
    {
        /// <summary>
        /// Determines whether a reference exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="referenceId">The reference identifier.</param>
        bool ReferenceExists(IDataSource dataSource, Guid referenceId);

        /// <summary>
        /// Gets the reference.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="referenceId">The reference identifier.</param>
        Reference GetReference(IDataSource dataSource, Guid referenceId);

        /// <summary>
        /// Lists the references for the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">A song.</param>
        List<Reference> ListReferences(IDataSource dataSource, Song song);

        /// <summary>
        /// Lists the references for the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">An album.</param>
        List<Reference> ListReferences(IDataSource dataSource, Album album);

        /// <summary>
        /// Lists the references for the artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">An artist.</param>
        List<Reference> ListReferences(IDataSource dataSource, Artist artist);

        /// <summary>
        /// Lists the references for the person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">A person.</param>
        List<Reference> ListReferences(IDataSource dataSource, Person person);

        /// <summary>
        /// Lists the references for the playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">A playlist.</param>
        List<Reference> ListReferences(IDataSource dataSource, Playlist playlist);

        /// <summary>
        /// Adds a reference.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        void AddReference(IDataSource dataSource, Reference reference);

        /// <summary>
        /// Adds a reference to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="song">The song.</param>
        void AddReferenceTo(IDataSource dataSource, Reference reference, Song song);

        /// <summary>
        /// Adds a reference to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="album">The album.</param>
        void AddReferenceTo(IDataSource dataSource, Reference reference, Album album);

        /// <summary>
        /// Adds a reference to the given artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="artist">The artist.</param>
        void AddReferenceTo(IDataSource dataSource, Reference reference, Artist artist);

        /// <summary>
        /// Adds a reference to the given person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="person">The person.</param>
        void AddReferenceTo(IDataSource dataSource, Reference reference, Person person);

        /// <summary>
        /// Adds a reference to the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">A reference.</param>
        /// <param name="playlist">The playlist.</param>
        void AddReferenceTo(IDataSource dataSource, Reference reference, Playlist playlist);

        /// <summary>
        /// Deletes the reference.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        void DeleteReference(IDataSource dataSource, Reference reference);

        /// <summary>
        /// Deletes the reference from a song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="song">The song.</param>
        void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Song song);

        /// <summary>
        /// Deletes the reference from an album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="album">The album.</param>
        void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Album album);

        /// <summary>
        /// Deletes the reference from an artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="artist">The artist.</param>
        void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Artist artist);

        /// <summary>
        /// Deletes the reference from a person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="person">The person.</param>
        void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Person person);

        /// <summary>
        /// Deletes the reference from a playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="playlist">The playlist.</param>
        void DeleteReferenceFrom(IDataSource dataSource, Reference reference, Playlist playlist);
    }
}
