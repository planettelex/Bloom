using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IMoodRepository
    {
        Mood GetMood(IDataSource dataSource, Guid moodId);

        List<Mood> ListMoods(IDataSource dataSource);

        List<Mood> ListMoods(IDataSource dataSource, Song song);

        List<Mood> ListMoods(IDataSource dataSource, Album album);

        List<Mood> ListMoods(IDataSource dataSource, Playlist playlist);

        void AddMood(IDataSource dataSource, Mood mood);

        void AddMoodTo(IDataSource dataSource, Mood mood, Song song);

        void AddMoodTo(IDataSource dataSource, Mood mood, Album album);

        void AddMoodTo(IDataSource dataSource, Mood mood, Playlist playlist);

        void DeleteMood(IDataSource dataSource, Mood mood);

        void DeleteMoodFrom(IDataSource dataSource, Mood mood, Song song);

        void DeleteMoodFrom(IDataSource dataSource, Mood mood, Album album);

        void DeleteMoodFrom(IDataSource dataSource, Mood mood, Playlist playlist);
    }
}
