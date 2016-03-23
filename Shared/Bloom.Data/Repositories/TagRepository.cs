using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for tag data.
    /// </summary>
    public class TagRepository : ITagRepository
    {
        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tagId">The tag identifier.</param>
        public Tag GetTag(IDataSource dataSource, Guid tagId)
        {
            if (!dataSource.IsConnected())
                return null;

            var tagTable = TagTable(dataSource);
            if (tagTable == null)
                return null;

            var tagQuery =
                from tag in tagTable
                where tag.Id == tagId
                select tag;

            return tagQuery.SingleOrDefault();
        }

        /// <summary>
        /// Lists the tags.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Tag> ListTags(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var tagTable = TagTable(dataSource);
            if (tagTable == null)
                return null;

            var tagsQuery =
                from tag in tagTable
                orderby tag.Name
                select tag;

            return tagsQuery.ToList();
        }

        /// <summary>
        /// Lists the tags for the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        public List<Tag> ListTags(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected() || song == null)
                return null;

            var tagTable = TagTable(dataSource);
            var songTagTable = SongTagTable(dataSource);
            if (songTagTable == null)
                return null;

            var tagsQuery =
                from st in songTagTable
                join tag in tagTable on st.TagId equals tag.Id
                where st.SongId == song.Id
                orderby tag.Name
                select tag;
            
            return tagsQuery.ToList();
        }

        /// <summary>
        /// Lists the tags for the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        public List<Tag> ListTags(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected() || album == null)
                return null;

            var tagTable = TagTable(dataSource);
            var albumTagTable = AlbumTagTable(dataSource);
            if (albumTagTable == null)
                return null;

            var tagsQuery =
                from at in albumTagTable
                join tag in tagTable on at.TagId equals tag.Id
                where at.AlbumId == album.Id
                orderby tag.Name
                select tag;

            return tagsQuery.ToList();
        }

        /// <summary>
        /// Lists the tags for the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        public List<Tag> ListTags(IDataSource dataSource, Playlist playlist)
        {
            if (!dataSource.IsConnected() || playlist == null)
                return null;

            var tagTable = TagTable(dataSource);
            var playlistTagTable = PlaylistTagTable(dataSource);
            if (playlistTagTable == null)
                return null;

            var tagsQuery =
                from pt in playlistTagTable
                join tag in tagTable on pt.TagId equals tag.Id
                where pt.PlaylistId == playlist.Id
                orderby tag.Name
                select tag;

            return tagsQuery.ToList();
        }

        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        public void AddTag(IDataSource dataSource, Tag tag)
        {
            if (!dataSource.IsConnected())
                return;

            var tagTable = TagTable(dataSource);
            if (tagTable == null)
                return;

            tagTable.InsertOnSubmit(tag);
            dataSource.Save();
        }

        /// <summary>
        /// Adds the tag to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="song">The song.</param>
        public void AddTagTo(IDataSource dataSource, Tag tag, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songTagTable = SongTagTable(dataSource);
            if (songTagTable == null)
                return;

            songTagTable.InsertOnSubmit(SongTag.Create(song, tag));
            dataSource.Save();
        }

        /// <summary>
        /// Adds the tag to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="album">The album.</param>
        public void AddTagTo(IDataSource dataSource, Tag tag, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTagTable = AlbumTagTable(dataSource);
            if (albumTagTable == null)
                return;

            albumTagTable.InsertOnSubmit(AlbumTag.Create(album, tag));
            dataSource.Save();
        }

        /// <summary>
        /// Adds the tag to the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="playlist">The playlist.</param>
        public void AddTagTo(IDataSource dataSource, Tag tag, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTagTable = PlaylistTagTable(dataSource);
            if (playlistTagTable == null)
                return;

            playlistTagTable.InsertOnSubmit(PlaylistTag.Create(playlist, tag));
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the tag.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        public void DeleteTag(IDataSource dataSource, Tag tag)
        {
            if (!dataSource.IsConnected())
                return;

            var tagTable = TagTable(dataSource);
            if (tagTable == null)
                return;

            var songTagTable = SongTagTable(dataSource);
            var songTagsQuery =
                from st in songTagTable
                where st.TagId == tag.Id
                select st;

            songTagTable.DeleteAllOnSubmit(songTagsQuery.AsEnumerable());
            dataSource.Save();

            var albumTagTable = AlbumTagTable(dataSource);
            var albumTagsQuery =
                from at in albumTagTable
                where at.TagId == tag.Id
                select at;

            albumTagTable.DeleteAllOnSubmit(albumTagsQuery.AsEnumerable());
            dataSource.Save();

            var playlistTagTable = PlaylistTagTable(dataSource);
            var playlistTagsQuery =
                from pt in playlistTagTable
                where pt.TagId == tag.Id
                select pt;

            playlistTagTable.DeleteAllOnSubmit(playlistTagsQuery.AsEnumerable());
            dataSource.Save();

            tagTable.DeleteOnSubmit(tag);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the tag from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="song">The song.</param>
        public void DeleteTagFrom(IDataSource dataSource, Tag tag, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songTagTable = SongTagTable(dataSource);
            if (songTagTable == null)
                return;

            var songTagQuery =
                from st in songTagTable
                where st.TagId == tag.Id && st.SongId == song.Id
                select st;

            var songTag = songTagQuery.SingleOrDefault();
            if (songTag == null)
                return;

            songTagTable.DeleteOnSubmit(songTag);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the tag from the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="album">The album.</param>
        public void DeleteTagFrom(IDataSource dataSource, Tag tag, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTagTable = AlbumTagTable(dataSource);
            if (albumTagTable == null)
                return;

            var albumTagQuery =
                from at in albumTagTable
                where at.TagId == tag.Id && at.AlbumId == album.Id
                select at;

            var albumTag = albumTagQuery.SingleOrDefault();
            if (albumTag == null)
                return;

            albumTagTable.DeleteOnSubmit(albumTag);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the tag from the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="playlist">The playlist.</param>
        public void DeleteTagFrom(IDataSource dataSource, Tag tag, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTagTable = PlaylistTagTable(dataSource);
            if (playlistTagTable == null)
                return;

            var playlistTagQuery =
                from pt in playlistTagTable
                where pt.TagId == tag.Id && pt.PlaylistId == playlist.Id
                select pt;

            var playlistTag = playlistTagQuery.SingleOrDefault();
            if (playlistTag == null)
                return;

            playlistTagTable.DeleteOnSubmit(playlistTag);
            dataSource.Save();
        }

        #region Tables

        private static Table<Tag> TagTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Tag>() : null;
        }

        private static Table<SongTag> SongTagTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongTag>() : null;
        }

        private static Table<AlbumTag> AlbumTagTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumTag>() : null;
        }

        private static Table<PlaylistTag> PlaylistTagTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PlaylistTag>() : null;
        }

        #endregion
    }
}
