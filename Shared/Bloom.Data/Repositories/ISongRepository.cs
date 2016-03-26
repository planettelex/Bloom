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
        /// Gets the recording session.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="recordingSessionId">The recording session identifier.</param>
        RecordingSession GetRecordingSession(IDataSource dataSource, Guid recordingSessionId);

        /// <summary>
        /// Lists the songs.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Song> ListSongs(IDataSource dataSource);

        /// <summary>
        /// Lists the song's recording sessions.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songId">The song identifier.</param>
        List<RecordingSession> ListRecordingSessions(IDataSource dataSource, Guid songId);

        /// <summary>
        /// Adds the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        void AddSong(IDataSource dataSource, Song song);

        /// <summary>
        /// Adds the song media.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songMedia">The song media.</param>
        void AddSongMedia(IDataSource dataSource, SongMedia songMedia);

        /// <summary>
        /// Deletes the song media.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songMedia">The song media.</param>
        void DeleteSongMedia(IDataSource dataSource, SongMedia songMedia);

        /// <summary>
        /// Adds a song segment.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songSegment">The song segment.</param>
        void AddSongSegment(IDataSource dataSource, SongSegment songSegment);

        /// <summary>
        /// Deletes a song segment.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songSegment">The song segment.</param>
        void DeleteSongSegment(IDataSource dataSource, SongSegment songSegment);

        /// <summary>
        /// Adds a song collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCollaborator">The song collaborator.</param>
        void AddSongCollaborator(IDataSource dataSource, SongCollaborator songCollaborator);

        /// <summary>
        /// Deletes a song collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCollaborator">The song collaborator.</param>
        void DeleteSongCollaborator(IDataSource dataSource, SongCollaborator songCollaborator);

        /// <summary>
        /// Adds a song credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        void AddSongCredit(IDataSource dataSource, SongCredit songCredit);

        /// <summary>
        /// Deletes a song credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        void DeleteSongCredit(IDataSource dataSource, SongCredit songCredit);

        /// <summary>
        /// Adds a song credit role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        /// <param name="role">A role.</param>
        void AddSongCreditRole(IDataSource dataSource, SongCredit songCredit, Role role);

        /// <summary>
        /// Deletes a song credit role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="songCredit">The song credit.</param>
        /// <param name="role">A role.</param>
        void DeleteSongCreditRole(IDataSource dataSource, SongCredit songCredit, Role role);

        /// <summary>
        /// Adds a recording session.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="recordingSession">The recording session.</param>
        void AddRecordingSession(IDataSource dataSource, RecordingSession recordingSession);

        /// <summary>
        /// Deletes a recording session.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="recordingSession">The recording session.</param>
        void DeleteRecordingSession(IDataSource dataSource, RecordingSession recordingSession);

        /// <summary>
        /// Deletes the song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        void DeleteSong(IDataSource dataSource, Song song);
    }
}
