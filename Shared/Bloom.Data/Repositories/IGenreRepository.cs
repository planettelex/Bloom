using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for genre data.
    /// </summary>
    public interface IGenreRepository
    {
        /// <summary>
        /// Gets a genre.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="genreId">The genre identifier.</param>
        Genre GetGenre(IDataSource dataSource, Guid genreId);

        /// <summary>
        /// Lists the genres.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Genre> ListGenres(IDataSource dataSource);

        /// <summary>
        /// Adds a genre.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="genre">The genre.</param>
        void AddGenre(IDataSource dataSource, Genre genre);

        /// <summary>
        /// Deletes a genre.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="genre">The genre.</param>
        void DeleteGenre(IDataSource dataSource, Genre genre);
    }
}
