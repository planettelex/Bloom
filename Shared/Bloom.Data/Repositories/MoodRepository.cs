using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class MoodRepository : IMoodRepository
    {
        public Mood GetMood(IDataSource dataSource, Guid moodId)
        {
            if (!dataSource.IsConnected())
                return null;

            var moodTable = MoodTable(dataSource);
            if (moodTable == null)
                return null;

            var moodQuery =
                from m in moodTable
                where m.Id == moodId
                select m;

            return moodQuery.SingleOrDefault();
        }

        public List<Mood> ListMoods(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var moodTable = MoodTable(dataSource);
            if (moodTable == null)
                return null;

            var moodsQuery =
                from m in moodTable
                select m;

            return moodsQuery.ToList();
        }

        public List<Mood> ListMoods(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected() || song == null)
                return null;

            var moodTable = MoodTable(dataSource);
            var songMoodTable = SongMoodTable(dataSource);
            if (songMoodTable == null)
                return null;

            var moodsQuery =
                from sm in songMoodTable
                join mood in moodTable on sm.MoodId equals mood.Id
                where sm.SongId == song.Id
                select mood;

            return moodsQuery.ToList();
        }

        public List<Mood> ListMoods(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected() || album == null)
                return null;

            var moodTable = MoodTable(dataSource);
            var albumMoodTable = AlbumMoodTable(dataSource);
            if (albumMoodTable == null)
                return null;

            var moodsQuery =
                from am in albumMoodTable
                join mood in moodTable on am.MoodId equals mood.Id
                where am.AlbumId == album.Id
                select mood;

            return moodsQuery.ToList();
        }

        public List<Mood> ListMoods(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected() || playlist == null)
                return null;

            var moodTable = MoodTable(dataSource);
            var playlistMoodTable = PlaylistMoodTable(dataSource);
            if (playlistMoodTable == null)
                return null;

            var moodsQuery =
                from pm in playlistMoodTable
                join mood in moodTable on pm.MoodId equals mood.Id
                where pm.PlaylistId == playlist.Id
                select mood;

            return moodsQuery.ToList();
        }

        public void AddMood(IDataSource dataSource, Mood mood)
        {
            if (!dataSource.IsConnected())
                return;

            var moodTable = MoodTable(dataSource);
            if (moodTable == null)
                return;

            moodTable.InsertOnSubmit(mood);
        }

        public void AddMoodTo(IDataSource dataSource, Mood mood, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songMoodTable = SongMoodTable(dataSource);
            if (songMoodTable == null)
                return;

            songMoodTable.InsertOnSubmit(SongMood.Create(song, mood));
        }

        public void AddMoodTo(IDataSource dataSource, Mood mood, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumMoodTable = AlbumMoodTable(dataSource);
            if (albumMoodTable == null)
                return;

            albumMoodTable.InsertOnSubmit(AlbumMood.Create(album, mood));
        }

        public void AddMoodTo(IDataSource dataSource, Mood mood, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistMoodTable = PlaylistMoodTable(dataSource);
            if (playlistMoodTable == null)
                return;

            playlistMoodTable.InsertOnSubmit(PlaylistMood.Create(playlist, mood));
        }

        public void DeleteMood(IDataSource dataSource, Mood mood)
        {
            if (!dataSource.IsConnected())
                return;

            var moodTable = MoodTable(dataSource);
            if (moodTable == null)
                return;

            moodTable.DeleteOnSubmit(mood);
        }

        public void DeleteMoodFrom(IDataSource dataSource, Mood mood, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songMoodTable = SongMoodTable(dataSource);
            if (songMoodTable == null)
                return;

            songMoodTable.DeleteOnSubmit(SongMood.Create(song, mood));
        }

        public void DeleteMoodFrom(IDataSource dataSource, Mood mood, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumMoodTable = AlbumMoodTable(dataSource);
            if (albumMoodTable == null)
                return;

            albumMoodTable.DeleteOnSubmit(AlbumMood.Create(album, mood));
        }

        public void DeleteMoodFrom(IDataSource dataSource, Mood mood, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistMoodTable = PlaylistMoodTable(dataSource);
            if (playlistMoodTable == null)
                return;

            playlistMoodTable.DeleteOnSubmit(PlaylistMood.Create(playlist, mood));
        }

        private static Table<Mood> MoodTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Mood>() : null;
        }

        private static Table<SongMood> SongMoodTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongMood>() : null;
        }

        private static Table<AlbumMood> AlbumMoodTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumMood>() : null;
        }

        private static Table<PlaylistMood> PlaylistMoodTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistMood>() : null;
        }
    }
}
