using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for review data.
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Gets the review.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reviewId">The review identifier.</param>
        Review GetReview(IDataSource dataSource, Guid reviewId);

        /// <summary>
        /// Lists the reviews for a given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        List<Review> ListReviews(IDataSource dataSource, Song song);

        /// <summary>
        /// Lists the reviews for a given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        List<Review> ListReviews(IDataSource dataSource, Album album);

        /// <summary>
        /// Adds the review.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        void AddReview(IDataSource dataSource, Review review);

        /// <summary>
        /// Adds the review to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        /// <param name="song">The song.</param>
        void AddReviewTo(IDataSource dataSource, Review review, Song song);

        /// <summary>
        /// Adds the review to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        /// <param name="album">The album.</param>
        void AddReviewTo(IDataSource dataSource, Review review, Album album);

        /// <summary>
        /// Deletes the review.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        void DeleteReview(IDataSource dataSource, Review review);

        /// <summary>
        /// Deletes the review from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        /// <param name="song">The song.</param>
        void DeleteReviewFrom(IDataSource dataSource, Review review, Song song);

        /// <summary>
        /// Deletes the review from the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        /// <param name="album">The album.</param>
        void DeleteReviewFrom(IDataSource dataSource, Review review, Album album);
    }
}
