using System;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for photo data.
    /// </summary>
    public interface IPhotoRespository
    {
        /// <summary>
        /// Determines whether a photo exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="photoId">The photo identifier.</param>
        bool PhotoExists(IDataSource dataSource, Guid photoId);

        /// <summary>
        /// Determines whether a photo exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filePath">The file path.</param>
        bool PhotoExists(IDataSource dataSource, string filePath);

        /// <summary>
        /// Gets a photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="photoId">The photo identifier.</param>
        Photo GetPhoto(IDataSource dataSource, Guid photoId);

        /// <summary>
        /// Gets a photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filePath">The file path.</param>
        Photo GetPhoto(IDataSource dataSource, string filePath);

        /// <summary>
        /// Adds a photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="photo">The photo.</param>
        void AddPhoto(IDataSource dataSource, Photo photo);

        /// <summary>
        /// Deletes a photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="photo">The photo.</param>
        void DeletePhoto(IDataSource dataSource, Photo photo);
    }
}
