using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IReviewRepository
    {
        Review GetReview(IDataSource dataSource, Guid reviewId);

        List<Review> ListReviews(IDataSource dataSource, Song song);

        List<Review> ListReviews(IDataSource dataSource, Album album);

        void AddReview(IDataSource dataSource, Review review);

        void AddReviewTo(IDataSource dataSource, Review review, Song song);

        void AddReviewTo(IDataSource dataSource, Review review, Album album);

        void DeleteReview(IDataSource dataSource, Review review);

        void DeleteReviewFrom(IDataSource dataSource, Review review, Song song);

        void DeleteReviewFrom(IDataSource dataSource, Review review, Album album);
    }
}
