using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for genre data.
    /// </summary>
    /// <seealso cref="Bloom.Data.Repositories.IGenreRepository" />
    public class GenreRepository : IGenreRepository
    {
        /// <summary>
        /// Gets a genre.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="genreId">The genre identifier.</param>
        public Genre GetGenre(IDataSource dataSource, Guid genreId)
        {
            if (!dataSource.IsConnected())
                return null;

            var genreTable = GenreTable(dataSource);
            if (genreTable == null)
                return null;

            var genreQuery =
                from g in genreTable
                where g.Id == genreId
                select g;

            var genre = genreQuery.SingleOrDefault();

            if (genre == null)
                return null;

            if (genre.ParentGenreId != null)
                genre.ParentGenre = GetGenre(dataSource, genre.ParentGenreId.Value);

            return genre;
        }

        /// <summary>
        /// Finds all genres with the given name.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="genreName">A genre name.</param>
        public List<Genre> FindGenre(IDataSource dataSource, string genreName)
        {
            if (!dataSource.IsConnected())
                return null;

            var genreTable = GenreTable(dataSource);
            if (genreTable == null)
                return null;

            var genreQuery =
                from g in genreTable
                where g.Name.ToLower() == genreName.ToLower()
                select g;

            var results = genreQuery.ToList();
            return !results.Any() ? null : results;
        }

        /// <summary>
        /// Lists the genres.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Genre> ListGenres(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var genreTable = GenreTable(dataSource);
            if (genreTable == null)
                return null;

            var genresQuery =
                from g in genreTable
                orderby g.Name
                select g;

            return genresQuery.ToList();
        }

        /// <summary>
        /// Adds a genre.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="genre">The genre.</param>
        public void AddGenre(IDataSource dataSource, Genre genre)
        {
            if (!dataSource.IsConnected())
                return;

            var genreTable = GenreTable(dataSource);
            if (genreTable == null)
                return;

            genreTable.InsertOnSubmit(genre);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a genre.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="genre">The genre.</param>
        public void DeleteGenre(IDataSource dataSource, Genre genre)
        {
            if (!dataSource.IsConnected())
                return;

            var genreTable = GenreTable(dataSource);
            if (genreTable == null)
                return;

            genreTable.DeleteOnSubmit(genre);
            dataSource.Save();
        }

        #region Tables

        private static Table<Genre> GenreTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Genre>() : null;
        }

        #endregion
    }
}
