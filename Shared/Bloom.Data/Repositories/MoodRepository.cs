using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for mood data.
    /// </summary>
    public class MoodRepository : IMoodRepository
    {
        /// <summary>
        /// Gets the mood.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="moodId">The mood identifier.</param>
        public Mood GetMood(IDataSource dataSource, Guid moodId)
        {
            if (!dataSource.IsConnected())
                return null;

            var moodTable = MoodTable(dataSource);
            if (moodTable == null)
                return null;

            var moodQuery =
                from mood in moodTable
                where mood.Id == moodId
                select mood;

            return moodQuery.SingleOrDefault();
        }

        /// <summary>
        /// Lists the moods.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Mood> ListMoods(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var moodTable = MoodTable(dataSource);
            if (moodTable == null)
                return null;

            var moodsQuery =
                from mood in moodTable
                orderby mood.Name
                select mood;

            return moodsQuery.ToList();
        }

        /// <summary>
        /// Lists the moods for a given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
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
                orderby mood.Name
                select mood;

            return moodsQuery.ToList();
        }

        /// <summary>
        /// Lists the moods for a given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
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
                orderby mood.Name
                select mood;

            return moodsQuery.ToList();
        }

        /// <summary>
        /// Lists the moods for a given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
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
                orderby mood.Name
                select mood;

            return moodsQuery.ToList();
        }

        /// <summary>
        /// Adds the mood.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        public void AddMood(IDataSource dataSource, Mood mood)
        {
            if (!dataSource.IsConnected())
                return;

            var moodTable = MoodTable(dataSource);
            if (moodTable == null)
                return;

            moodTable.InsertOnSubmit(mood);
            dataSource.Save();
        }

        /// <summary>
        /// Adds the mood to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="song">The song.</param>
        public void AddMoodTo(IDataSource dataSource, Mood mood, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songMoodTable = SongMoodTable(dataSource);
            if (songMoodTable == null)
                return;

            songMoodTable.InsertOnSubmit(SongMood.Create(song, mood));
            dataSource.Save();
        }

        /// <summary>
        /// Adds the mood to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="album">The album.</param>
        public void AddMoodTo(IDataSource dataSource, Mood mood, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumMoodTable = AlbumMoodTable(dataSource);
            if (albumMoodTable == null)
                return;

            albumMoodTable.InsertOnSubmit(AlbumMood.Create(album, mood));
            dataSource.Save();
        }

        /// <summary>
        /// Adds the mood to the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="playlist">The playlist.</param>
        public void AddMoodTo(IDataSource dataSource, Mood mood, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistMoodTable = PlaylistMoodTable(dataSource);
            if (playlistMoodTable == null)
                return;

            playlistMoodTable.InsertOnSubmit(PlaylistMood.Create(playlist, mood));
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the mood.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        public void DeleteMood(IDataSource dataSource, Mood mood)
        {
            if (!dataSource.IsConnected())
                return;

            var moodTable = MoodTable(dataSource);
            if (moodTable == null)
                return;

            var songMoodTable = SongMoodTable(dataSource);
            var songMoodsQuery =
                from sm in songMoodTable
                where sm.MoodId == mood.Id
                select sm;

            songMoodTable.DeleteAllOnSubmit(songMoodsQuery.AsEnumerable());
            dataSource.Save();

            var albumMoodTable = AlbumMoodTable(dataSource);
            var albumMoodsQuery =
                from am in albumMoodTable
                where am.MoodId == mood.Id
                select am;

            albumMoodTable.DeleteAllOnSubmit(albumMoodsQuery.AsEnumerable());
            dataSource.Save();

            var playlistMoodTable = PlaylistMoodTable(dataSource);
            var playlistMoodsQuery =
                from pm in playlistMoodTable
                where pm.MoodId == mood.Id
                select pm;

            playlistMoodTable.DeleteAllOnSubmit(playlistMoodsQuery.AsEnumerable());
            dataSource.Save();

            moodTable.DeleteOnSubmit(mood);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the mood from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="song">The song.</param>
        public void DeleteMoodFrom(IDataSource dataSource, Mood mood, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songMoodTable = SongMoodTable(dataSource);
            if (songMoodTable == null)
                return;

            var songMoodQuery =
                from sm in songMoodTable
                where sm.MoodId == mood.Id && sm.SongId == song.Id
                select sm;

            var songMood = songMoodQuery.SingleOrDefault();
            if (songMood == null)
                return;

            songMoodTable.DeleteOnSubmit(songMood);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the mood from the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="album">The album.</param>
        public void DeleteMoodFrom(IDataSource dataSource, Mood mood, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumMoodTable = AlbumMoodTable(dataSource);
            if (albumMoodTable == null)
                return;

            var albumMoodQuery =
                from am in albumMoodTable
                where am.MoodId == mood.Id && am.AlbumId == album.Id
                select am;

            var albumMood = albumMoodQuery.SingleOrDefault();
            if (albumMood == null)
                return;

            albumMoodTable.DeleteOnSubmit(albumMood);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the mood from the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="mood">The mood.</param>
        /// <param name="playlist">The playlist.</param>
        public void DeleteMoodFrom(IDataSource dataSource, Mood mood, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistMoodTable = PlaylistMoodTable(dataSource);
            if (playlistMoodTable == null)
                return;

            var playlistMoodQuery =
                from pm in playlistMoodTable
                where pm.MoodId == mood.Id && pm.PlaylistId == playlist.Id
                select pm;

            var playlistMood = playlistMoodQuery.SingleOrDefault();
            if (playlistMood == null)
                return;

            playlistMoodTable.DeleteOnSubmit(playlistMood);
            dataSource.Save();
        }

        #region Tables

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

        #endregion
    }
}
