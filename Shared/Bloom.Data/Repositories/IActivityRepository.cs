using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IActivityRepository
    {
        Activity GetActivity(IDataSource dataSource, Guid activityId);

        List<Activity> ListActivities(IDataSource dataSource);

        List<Activity> ListActivities(IDataSource dataSource, Song song);

        List<Activity> ListActivities(IDataSource dataSource, Album album);

        List<Activity> ListActivities(IDataSource dataSource, Playlist playlist);

        void AddActivity(IDataSource dataSource, Activity activity);

        void AddActivityTo(IDataSource dataSource, Activity activity, Song song);

        void AddActivityTo(IDataSource dataSource, Activity activity, Album album);

        void AddActivityTo(IDataSource dataSource, Activity activity, Playlist playlist);

        void DeleteActivity(IDataSource dataSource, Activity activity);

        void DeleteActivityFrom(IDataSource dataSource, Activity activity, Song song);

        void DeleteActivityFrom(IDataSource dataSource, Activity activity, Album album);

        void DeleteActivityFrom(IDataSource dataSource, Activity activity, Playlist playlist);
    }
}
