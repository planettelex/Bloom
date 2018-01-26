using System;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for photo data.
    /// </summary>
    public class PhotoRespository : IPhotoRespository
    {
        /// <summary>
        /// Determines whether a photo exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="photoId">The photo identifier.</param>
        public bool PhotoExists(IDataSource dataSource, Guid photoId)
        {
            if (!dataSource.IsConnected())
                return false;

            var photoTable = PhotoTable(dataSource);
            if (photoTable == null)
                return false;

            var photoQuery =
                from p in photoTable
                where p.Id == photoId
                select p;

            return photoQuery.Any();
        }

        /// <summary>
        /// Determines whether a photo exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filePath">The file path.</param>
        public bool PhotoExists(IDataSource dataSource, string filePath)
        {
            if (!dataSource.IsConnected())
                return false;

            var photoTable = PhotoTable(dataSource);
            if (photoTable == null)
                return false;

            var photoQuery =
                from p in photoTable
                where p.FilePath.ToLower() == filePath.ToLower()
                select p;

            return photoQuery.Any();
        }

        /// <summary>
        /// Gets a photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="photoId">The photo identifier.</param>
        public Photo GetPhoto(IDataSource dataSource, Guid photoId)
        {
            if (!dataSource.IsConnected())
                return null;

            var photoTable = PhotoTable(dataSource);
            if (photoTable == null)
                return null;

            var photoQuery =
                from p in photoTable
                where p.Id == photoId
                select p;

            return photoQuery.SingleOrDefault();
        }

        /// <summary>
        /// Gets a photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filePath">The file path.</param>
        public Photo GetPhoto(IDataSource dataSource, string filePath)
        {
            if (!dataSource.IsConnected())
                return null;

            var photoTable = PhotoTable(dataSource);
            if (photoTable == null)
                return null;

            var photoQuery =
                from p in photoTable
                where p.FilePath.ToLower() == filePath.ToLower()
                select p;

            return photoQuery.SingleOrDefault();
        }

        /// <summary>
        /// Adds a photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="photo">The photo.</param>
        public void AddPhoto(IDataSource dataSource, Photo photo)
        {
            if (!dataSource.IsConnected())
                return;

            var photoTable = PhotoTable(dataSource);
            if (photoTable == null)
                return;

            photoTable.InsertOnSubmit(photo);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="photo">The photo.</param>
        public void DeletePhoto(IDataSource dataSource, Photo photo)
        {
            if (!dataSource.IsConnected())
                return;

            var photoTable = PhotoTable(dataSource);
            if (photoTable == null)
                return;

            photoTable.DeleteOnSubmit(photo);
            dataSource.Save();
        }

        #region Tables

        private static Table<Photo> PhotoTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Photo>();
        }

        #endregion
    }
}
