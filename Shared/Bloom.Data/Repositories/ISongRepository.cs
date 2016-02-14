using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface ISongRepository
    {
        Song GetSong(IDataSource dataSource, Guid songId);

        List<Song> ListSongs(IDataSource dataSource);

        void AddSong(IDataSource dataSource, Song song);

        void AddSongSegment(IDataSource dataSource, SongSegment songSegment);

        void AddSongCollaborator(IDataSource dataSource, SongCollaborator songCollaborator);

        void AddSongCredit(IDataSource dataSource, SongCredit songCredit);

        void DeleteSong(IDataSource dataSource, Song song);
    }
}
