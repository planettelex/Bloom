using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        public Activity GetActivity(IDataSource dataSource, Guid activityId)
        {
            if (!dataSource.IsConnected())
                return null;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return null;

            var activityQuery =
                from m in activityTable
                where m.Id == activityId
                select m;

            return activityQuery.SingleOrDefault();
        }

        public List<Activity> ListActivities(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return null;

            var activitysQuery =
                from a in activityTable
                select a;

            return activitysQuery.ToList();
        }

        public List<Activity> ListActivities(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected() || song == null)
                return null;

            var activityTable = ActivityTable(dataSource);
            var songActivityTable = SongActivityTable(dataSource);
            if (songActivityTable == null)
                return null;

            var activitysQuery =
                from sa in songActivityTable
                join activity in activityTable on sa.ActivityId equals activity.Id
                where sa.SongId == song.Id
                select activity;

            return activitysQuery.ToList();
        }

        public List<Activity> ListActivities(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected() || album == null)
                return null;

            var activityTable = ActivityTable(dataSource);
            var albumActivityTable = AlbumActivityTable(dataSource);
            if (albumActivityTable == null)
                return null;

            var activitysQuery =
                from aa in albumActivityTable
                join activity in activityTable on aa.ActivityId equals activity.Id
                where aa.AlbumId == album.Id
                select activity;

            return activitysQuery.ToList();
        }

        public List<Activity> ListActivities(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected() || playlist == null)
                return null;

            var activityTable = ActivityTable(dataSource);
            var playlistActivityTable = PlaylistActivityTable(dataSource);
            if (playlistActivityTable == null)
                return null;

            var activitysQuery =
                from pa in playlistActivityTable
                join activity in activityTable on pa.ActivityId equals activity.Id
                where pa.PlaylistId == playlist.Id
                select activity;

            return activitysQuery.ToList();
        }

        public void AddActivity(IDataSource dataSource, Activity activity)
        {
            if (!dataSource.IsConnected())
                return;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return;

            activityTable.InsertOnSubmit(activity);
        }

        public void AddActivityTo(IDataSource dataSource, Activity activity, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songActivityTable = SongActivityTable(dataSource);
            if (songActivityTable == null)
                return;

            songActivityTable.InsertOnSubmit(SongActivity.Create(song, activity));
        }

        public void AddActivityTo(IDataSource dataSource, Activity activity, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumActivityTable = AlbumActivityTable(dataSource);
            if (albumActivityTable == null)
                return;

            albumActivityTable.InsertOnSubmit(AlbumActivity.Create(album, activity));
        }

        public void AddActivityTo(IDataSource dataSource, Activity activity, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistActivityTable = PlaylistActivityTable(dataSource);
            if (playlistActivityTable == null)
                return;

            playlistActivityTable.InsertOnSubmit(PlaylistActivity.Create(playlist, activity));
        }

        public void DeleteActivity(IDataSource dataSource, Activity activity)
        {
            if (!dataSource.IsConnected())
                return;

            var activityTable = ActivityTable(dataSource);
            if (activityTable == null)
                return;

            activityTable.DeleteOnSubmit(activity);
        }

        public void DeleteActivityFrom(IDataSource dataSource, Activity activity, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songActivityTable = SongActivityTable(dataSource);
            if (songActivityTable == null)
                return;

            songActivityTable.DeleteOnSubmit(SongActivity.Create(song, activity));
        }

        public void DeleteActivityFrom(IDataSource dataSource, Activity activity, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumActivityTable = AlbumActivityTable(dataSource);
            if (albumActivityTable == null)
                return;

            albumActivityTable.DeleteOnSubmit(AlbumActivity.Create(album, activity));
        }

        public void DeleteActivityFrom(IDataSource dataSource, Activity activity, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistActivityTable = PlaylistActivityTable(dataSource);
            if (playlistActivityTable == null)
                return;

            playlistActivityTable.DeleteOnSubmit(PlaylistActivity.Create(playlist, activity));
        }

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
    }
}
