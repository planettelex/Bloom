using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for activity data.
    /// </summary>
    public class ActivityRepository : IActivityRepository
    {
        /// <summary>
        /// Gets the activity.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activityId">The activity identifier.</param>
        public Activity GetActivity(IDataSource dataSource, Guid activityId)
        {
            if (!dataSource.IsConnected())
                return null;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return null;

            var activityQuery =
                from activity in activityTable
                where activity.Id == activityId
                select activity;

            return activityQuery.SingleOrDefault();
        }

        /// <summary>
        /// Finds all activities with the given name.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activityName">Name of the activity.</param>
        public List<Activity> FindActivity(IDataSource dataSource, string activityName)
        {
            if (!dataSource.IsConnected())
                return null;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return null;

            var activityQuery =
                from a in activityTable
                where a.Name.ToLower() == activityName.ToLower()
                select a;

            var results = activityQuery.ToList();
            return !results.Any() ? null : results;
        }

        /// <summary>
        /// Lists the activities.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Activity> ListActivities(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return null;

            var activitiesQuery =
                from activity in activityTable
                orderby activity.Name
                select activity;

            return activitiesQuery.ToList();
        }

        /// <summary>
        /// Lists the activities for a given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        public List<Activity> ListActivities(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected() || song == null)
                return null;

            var activityTable = ActivityTable(dataSource);
            var songActivityTable = SongActivityTable(dataSource);
            if (songActivityTable == null)
                return null;

            var activitiesQuery =
                from sa in songActivityTable
                join activity in activityTable on sa.ActivityId equals activity.Id
                where sa.SongId == song.Id
                orderby activity.Name
                select activity;

            return activitiesQuery.ToList();
        }

        /// <summary>
        /// Lists the activities for a given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        public List<Activity> ListActivities(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected() || album == null)
                return null;

            var activityTable = ActivityTable(dataSource);
            var albumActivityTable = AlbumActivityTable(dataSource);
            if (albumActivityTable == null)
                return null;

            var activitiesQuery =
                from aa in albumActivityTable
                join activity in activityTable on aa.ActivityId equals activity.Id
                where aa.AlbumId == album.Id
                orderby activity.Name
                select activity;

            return activitiesQuery.ToList();
        }

        /// <summary>
        /// Lists the activities for a given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        public List<Activity> ListActivities(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected() || playlist == null)
                return null;

            var activityTable = ActivityTable(dataSource);
            var playlistActivityTable = PlaylistActivityTable(dataSource);
            if (playlistActivityTable == null)
                return null;

            var activitiesQuery =
                from pa in playlistActivityTable
                join activity in activityTable on pa.ActivityId equals activity.Id
                where pa.PlaylistId == playlist.Id
                orderby activity.Name
                select activity;

            return activitiesQuery.ToList();
        }

        /// <summary>
        /// Adds the activity.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        public void AddActivity(IDataSource dataSource, Activity activity)
        {
            if (!dataSource.IsConnected())
                return;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return;

            activityTable.InsertOnSubmit(activity);
            dataSource.Save();
        }

        /// <summary>
        /// Adds the activity to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="song">The song.</param>
        public void AddActivityTo(IDataSource dataSource, Activity activity, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songActivityTable = SongActivityTable(dataSource);
            if (songActivityTable == null)
                return;

            var songActivityQuery =
                from sa in songActivityTable
                where sa.SongId == song.Id && sa.ActivityId == activity.Id
                select sa;

            if (songActivityQuery.Any())
                return;

            songActivityTable.InsertOnSubmit(SongActivity.Create(song, activity));
            dataSource.Save();
        }

        /// <summary>
        /// Adds the activity to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="album">The album.</param>
        public void AddActivityTo(IDataSource dataSource, Activity activity, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumActivityTable = AlbumActivityTable(dataSource);
            if (albumActivityTable == null)
                return;

            var albumActivityQuery =
                from aa in albumActivityTable
                where aa.AlbumId == album.Id && aa.ActivityId == activity.Id
                select aa;

            if (albumActivityQuery.Any())
                return;

            albumActivityTable.InsertOnSubmit(AlbumActivity.Create(album, activity));
            dataSource.Save();
        }

        /// <summary>
        /// Adds the activity to the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="playlist">The playlist.</param>
        public void AddActivityTo(IDataSource dataSource, Activity activity, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistActivityTable = PlaylistActivityTable(dataSource);
            if (playlistActivityTable == null)
                return;

            var playlistActivityQuery =
                from pa in playlistActivityTable
                where pa.PlaylistId == playlist.Id && pa.ActivityId == activity.Id
                select pa;

            if (playlistActivityQuery.Any())
                return;

            playlistActivityTable.InsertOnSubmit(PlaylistActivity.Create(playlist, activity));
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the activity.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        public void DeleteActivity(IDataSource dataSource, Activity activity)
        {
            if (!dataSource.IsConnected())
                return;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return;

            var songActivityTable = SongActivityTable(dataSource);
            var songActivitiesQuery =
                from sa in songActivityTable
                where sa.ActivityId == activity.Id
                select sa;

            songActivityTable.DeleteAllOnSubmit(songActivitiesQuery.AsEnumerable());
            dataSource.Save();

            var albumActivityTable = AlbumActivityTable(dataSource);
            var albumActivitiesQuery =
                from aa in albumActivityTable
                where aa.ActivityId == activity.Id
                select aa;

            albumActivityTable.DeleteAllOnSubmit(albumActivitiesQuery.AsEnumerable());
            dataSource.Save();

            var playlistActivityTable = PlaylistActivityTable(dataSource);
            var playlistActivitiesQuery =
                from pa in playlistActivityTable
                where pa.ActivityId == activity.Id
                select pa;

            playlistActivityTable.DeleteAllOnSubmit(playlistActivitiesQuery.AsEnumerable());
            dataSource.Save();

            activityTable.DeleteOnSubmit(activity);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the activity from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="song">The song.</param>
        public void DeleteActivityFrom(IDataSource dataSource, Activity activity, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songActivityTable = SongActivityTable(dataSource);
            if (songActivityTable == null)
                return;

            var songActivityQuery =
                from sa in songActivityTable
                where sa.ActivityId == activity.Id && sa.SongId == song.Id
                select sa;

            var songActivity = songActivityQuery.SingleOrDefault();
            if (songActivity == null)
                return;

            songActivityTable.DeleteOnSubmit(songActivity);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the activity from the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="album">The album.</param>
        public void DeleteActivityFrom(IDataSource dataSource, Activity activity, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumActivityTable = AlbumActivityTable(dataSource);
            if (albumActivityTable == null)
                return;

            var albumActivityQuery =
                from aa in albumActivityTable
                where aa.ActivityId == activity.Id && aa.AlbumId == album.Id
                select aa;

            var albumActivity = albumActivityQuery.SingleOrDefault();
            if (albumActivity == null)
                return;

            albumActivityTable.DeleteOnSubmit(albumActivity);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the activity from the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="playlist">The playlist.</param>
        public void DeleteActivityFrom(IDataSource dataSource, Activity activity, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistActivityTable = PlaylistActivityTable(dataSource);
            if (playlistActivityTable == null)
                return;

            var playlistActivityQuery =
                from pa in playlistActivityTable
                where pa.ActivityId == activity.Id && pa.PlaylistId == playlist.Id
                select pa;

            var playlistActivity = playlistActivityQuery.SingleOrDefault();
            if (playlistActivity == null)
                return;

            playlistActivityTable.DeleteOnSubmit(playlistActivity);
            dataSource.Save();
        }

        #region Tables

        private static Table<Activity> ActivityTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Activity>() : null;
        }

        private static Table<SongActivity> SongActivityTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongActivity>() : null;
        }

        private static Table<AlbumActivity> AlbumActivityTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumActivity>() : null;
        }

        private static Table<PlaylistActivity> PlaylistActivityTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistActivity>() : null;
        }

        #endregion
    }
}
