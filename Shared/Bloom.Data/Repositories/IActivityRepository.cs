using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for activity data.
    /// </summary>
    public interface IActivityRepository
    {
        /// <summary>
        /// Gets the activity.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activityId">The activity identifier.</param>
        Activity GetActivity(IDataSource dataSource, Guid activityId);

        /// <summary>
        /// Finds all activities with the given name.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activityName">The name of the activity.</param>
        List<Activity> FindActivity(IDataSource dataSource, string activityName);

        /// <summary>
        /// Lists the activities.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Activity> ListActivities(IDataSource dataSource);

        /// <summary>
        /// Lists the activities for a given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        List<Activity> ListActivities(IDataSource dataSource, Song song);

        /// <summary>
        /// Lists the activities for a given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        List<Activity> ListActivities(IDataSource dataSource, Album album);

        /// <summary>
        /// Lists the activities for a given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        List<Activity> ListActivities(IDataSource dataSource, Playlist playlist);

        /// <summary>
        /// Adds the activity.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        void AddActivity(IDataSource dataSource, Activity activity);

        /// <summary>
        /// Adds the activity to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="song">The song.</param>
        void AddActivityTo(IDataSource dataSource, Activity activity, Song song);

        /// <summary>
        /// Adds the activity to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="album">The album.</param>
        void AddActivityTo(IDataSource dataSource, Activity activity, Album album);

        /// <summary>
        /// Adds the activity to the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="playlist">The playlist.</param>
        void AddActivityTo(IDataSource dataSource, Activity activity, Playlist playlist);

        /// <summary>
        /// Deletes the activity.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        void DeleteActivity(IDataSource dataSource, Activity activity);

        /// <summary>
        /// Deletes the activity from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="song">The song.</param>
        void DeleteActivityFrom(IDataSource dataSource, Activity activity, Song song);

        /// <summary>
        /// Deletes the activity from the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="album">The album.</param>
        void DeleteActivityFrom(IDataSource dataSource, Activity activity, Album album);

        /// <summary>
        /// Deletes the activity from the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="playlist">The playlist.</param>
        void DeleteActivityFrom(IDataSource dataSource, Activity activity, Playlist playlist);
    }
}
