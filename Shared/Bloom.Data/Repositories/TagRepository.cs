using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        public Tag GetTag(IDataSource dataSource, Guid tagId)
        {
            if (!dataSource.IsConnected())
                return null;

            var tagTable = TagTable(dataSource);
            if (tagTable == null)
                return null;

            var tagQuery =
                from t in tagTable
                where t.Id == tagId
                select t;

            return tagQuery.SingleOrDefault();
        }

        public List<Tag> ListTags(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var tagTable = TagTable(dataSource);
            if (tagTable == null)
                return null;

            var tagsQuery =
                from t in tagTable
                select t;

            return tagsQuery.ToList();
        }

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
                select tag;
            
            return tagsQuery.ToList();
        }

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
                select tag;

            return tagsQuery.ToList();
        }

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
                select tag;

            return tagsQuery.ToList();
        }

        public void AddTag(IDataSource dataSource, Tag tag)
        {
            if (!dataSource.IsConnected())
                return;

            var tagTable = TagTable(dataSource);
            if (tagTable == null)
                return;

            tagTable.InsertOnSubmit(tag);
        }

        public void AddTagTo(IDataSource dataSource, Tag tag, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songTagTable = SongTagTable(dataSource);
            if (songTagTable == null)
                return;

            songTagTable.InsertOnSubmit(SongTag.Create(song, tag));
        }

        public void AddTagTo(IDataSource dataSource, Tag tag, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTagTable = AlbumTagTable(dataSource);
            if (albumTagTable == null)
                return;

            albumTagTable.InsertOnSubmit(AlbumTag.Create(album, tag));
        }

        public void AddTagTo(IDataSource dataSource, Tag tag, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTagTable = PlaylistTagTable(dataSource);
            if (playlistTagTable == null)
                return;

            playlistTagTable.InsertOnSubmit(PlaylistTag.Create(playlist, tag));
        }

        public void DeleteTag(IDataSource dataSource, Tag tag)
        {
            if (!dataSource.IsConnected())
                return;

            var tagTable = TagTable(dataSource);
            if (tagTable == null)
                return;

            tagTable.DeleteOnSubmit(tag);
        }

        public void DeleteTagFrom(IDataSource dataSource, Tag tag, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songTagTable = SongTagTable(dataSource);
            if (songTagTable == null)
                return;

            songTagTable.DeleteOnSubmit(SongTag.Create(song, tag));
        }

        public void DeleteTagFrom(IDataSource dataSource, Tag tag, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTagTable = AlbumTagTable(dataSource);
            if (albumTagTable == null)
                return;

            albumTagTable.DeleteOnSubmit(AlbumTag.Create(album, tag));
        }

        public void DeleteTagFrom(IDataSource dataSource, Tag tag, Playlist playlist)
        {
            if (!dataSource.IsConnected())
                return;

            var playlistTagTable = PlaylistTagTable(dataSource);
            if (playlistTagTable == null)
                return;

            playlistTagTable.DeleteOnSubmit(PlaylistTag.Create(playlist, tag));
        }

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
    }
}
