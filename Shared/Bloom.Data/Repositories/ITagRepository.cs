using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface ITagRepository
    {
        Tag GetTag(IDataSource dataSource, Guid tagId);

        List<Tag> ListTags(IDataSource dataSource);

        List<Tag> ListTags(IDataSource dataSource, Song song);

        List<Tag> ListTags(IDataSource dataSource, Album album);

        List<Tag> ListTags(IDataSource dataSource, Playlist playlist);

        void AddTag(IDataSource dataSource, Tag tag);

        void AddTagTo(IDataSource dataSource, Tag tag, Song song);

        void AddTagTo(IDataSource dataSource, Tag tag, Album album);

        void AddTagTo(IDataSource dataSource, Tag tag, Playlist playlist);

        void DeleteTag(IDataSource dataSource, Tag tag);

        void DeleteTagFrom(IDataSource dataSource, Tag tag, Song song);

        void DeleteTagFrom(IDataSource dataSource, Tag tag, Album album);

        void DeleteTagFrom(IDataSource dataSource, Tag tag, Playlist playlist);
    }
}
