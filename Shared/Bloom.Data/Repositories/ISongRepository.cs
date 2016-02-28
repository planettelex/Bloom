using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for song data.
    /// </summary>
    public interface ISongRepository
    {
        /// <summary>
        /// Gets the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songId">The song identifier.</param>
        Song GetSong(IDataSource dataSource, Guid songId);

        /// <summary>
        /// Lists the songs.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Song> ListSongs(IDataSource dataSource);

        /// <summary>
        /// Adds the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        void AddSong(IDataSource dataSource, Song song);

        /// <summary>
        /// Adds a song segment.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songSegment">The song segment.</param>
        void AddSongSegment(IDataSource dataSource, SongSegment songSegment);

        /// <summary>
        /// Adds a song collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCollaborator">The song collaborator.</param>
        void AddSongCollaborator(IDataSource dataSource, SongCollaborator songCollaborator);

        /// <summary>
        /// Adds a song credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        void AddSongCredit(IDataSource dataSource, SongCredit songCredit);

        /// <summary>
        /// Deletes the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        void DeleteSong(IDataSource dataSource, Song song);
    }
}
