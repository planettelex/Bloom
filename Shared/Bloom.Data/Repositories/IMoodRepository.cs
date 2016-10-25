using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for mood data.
    /// </summary>
    public interface IMoodRepository
    {
        /// <summary>
        /// Gets the mood.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="moodId">The mood identifier.</param>
        Mood GetMood(IDataSource dataSource, Guid moodId);

        /// <summary>
        /// Finds all moods with the given name.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="moodName">The name of the mood.</param>
        List<Mood> FindMood(IDataSource dataSource, string moodName);

        /// <summary>
        /// Lists the moods.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Mood> ListMoods(IDataSource dataSource);

        /// <summary>
        /// Lists the moods for a given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        List<Mood> ListMoods(IDataSource dataSource, Song song);

        /// <summary>
        /// Lists the moods for a given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        List<Mood> ListMoods(IDataSource dataSource, Album album);

        /// <summary>
        /// Lists the moods for a given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        List<Mood> ListMoods(IDataSource dataSource, Playlist playlist);

        /// <summary>
        /// Adds the mood.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        void AddMood(IDataSource dataSource, Mood mood);

        /// <summary>
        /// Adds the mood to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="song">The song.</param>
        void AddMoodTo(IDataSource dataSource, Mood mood, Song song);

        /// <summary>
        /// Adds the mood to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="album">The album.</param>
        void AddMoodTo(IDataSource dataSource, Mood mood, Album album);

        /// <summary>
        /// Adds the mood to the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="playlist">The playlist.</param>
        void AddMoodTo(IDataSource dataSource, Mood mood, Playlist playlist);

        /// <summary>
        /// Deletes the mood.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        void DeleteMood(IDataSource dataSource, Mood mood);

        /// <summary>
        /// Deletes the mood from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="song">The song.</param>
        void DeleteMoodFrom(IDataSource dataSource, Mood mood, Song song);

        /// <summary>
        /// Deletes the mood from the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="album">The album.</param>
        void DeleteMoodFrom(IDataSource dataSource, Mood mood, Album album);

        /// <summary>
        /// Deletes the mood from the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="playlist">The playlist.</param>
        void DeleteMoodFrom(IDataSource dataSource, Mood mood, Playlist playlist);
    }
}
