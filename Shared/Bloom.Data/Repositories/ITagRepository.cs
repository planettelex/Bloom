using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for tag data.
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tagId">The tag identifier.</param>
        Tag GetTag(IDataSource dataSource, Guid tagId);

        /// <summary>
        /// Finds all tags with the given name.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tagName">The name of the tag.</param>
        List<Tag> FindTag(IDataSource dataSource, string tagName);

        /// <summary>
        /// Lists the tags.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Tag> ListTags(IDataSource dataSource);

        /// <summary>
        /// Lists the tags for the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        List<Tag> ListTags(IDataSource dataSource, Song song);

        /// <summary>
        /// Lists the tags for the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        List<Tag> ListTags(IDataSource dataSource, Album album);

        /// <summary>
        /// Lists the tags for the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="playlist">The playlist.</param>
        List<Tag> ListTags(IDataSource dataSource, Playlist playlist);

        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        void AddTag(IDataSource dataSource, Tag tag);

        /// <summary>
        /// Adds the tag to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="song">The song.</param>
        void AddTagTo(IDataSource dataSource, Tag tag, Song song);

        /// <summary>
        /// Adds the tag to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="album">The album.</param>
        void AddTagTo(IDataSource dataSource, Tag tag, Album album);

        /// <summary>
        /// Adds the tag to the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="playlist">The playlist.</param>
        void AddTagTo(IDataSource dataSource, Tag tag, Playlist playlist);

        /// <summary>
        /// Deletes the tag.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        void DeleteTag(IDataSource dataSource, Tag tag);

        /// <summary>
        /// Deletes the tag from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="song">The song.</param>
        void DeleteTagFrom(IDataSource dataSource, Tag tag, Song song);

        /// <summary>
        /// Deletes the tag from the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="album">The album.</param>
        void DeleteTagFrom(IDataSource dataSource, Tag tag, Album album);

        /// <summary>
        /// Deletes the tag from the given playlist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="playlist">The playlist.</param>
        void DeleteTagFrom(IDataSource dataSource, Tag tag, Playlist playlist);
    }
}
