using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for review data.
    /// </summary>
    public class ReviewRepository : IReviewRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewRepository"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository.</param>
        /// <param name="sourceRepository">The source repository.</param>
        public ReviewRepository(IPersonRepository personRepository, ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
            _personRepository = personRepository;
        }
        private readonly ISourceRepository _sourceRepository;
        private readonly IPersonRepository _personRepository;
        
        /// <summary>
        /// Gets the review.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="reviewId">The review identifier.</param>
        public Review GetReview(IDataSource dataSource, Guid reviewId)
        {
            if (!dataSource.IsConnected())
                return null;

            var sourceTable = SourceTable(dataSource);
            var personTable = PersonTable(dataSource);
            var reviewTable = ReviewTable(dataSource);
            if (reviewTable == null)
                return null;

            var reviewQuery =
                from r in reviewTable
                from person in personTable.Where(p => r.AuthorId == p.Id).DefaultIfEmpty()
                join source in sourceTable on r.SourceId equals source.Id
                where r.Id == reviewId
                select new
                {
                    Review = r,
                    Person = person,
                    Source = source
                };

            var result = reviewQuery.SingleOrDefault();

            var review = result?.Review;
            if (review == null)
                return null;

            review.Author = result.Person;
            review.Source = result.Source;

            return review;
        }

        /// <summary>
        /// Lists the reviews for a given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="song">The song.</param>
        public List<Review> ListReviews(IDataSource dataSource, Song song)
        {
            if (!dataSource.IsConnected() || song == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var personTable = PersonTable(dataSource);
            var reviewTable = ReviewTable(dataSource);
            var songReviewTable = SongReviewTable(dataSource);
            if (songReviewTable == null)
                return null;

            var reviewsQuery =
                from sr in songReviewTable
                join review in reviewTable on sr.ReviewId equals review.Id
                join source in sourceTable on review.SourceId equals source.Id
                from person in personTable.Where(p => review.AuthorId == p.Id).DefaultIfEmpty()
                orderby review.PublishedOn descending, review.Title
                where sr.SongId == song.Id
                select new
                {
                    Review = review,
                    Source = source,
                    Person = person
                };

            var results = reviewsQuery.ToList();

            if (!results.Any())
                return null;

            var reviews = new List<Review>();
            foreach (var result in results)
            {
                var review = result.Review;
                review.Source = result.Source;
                review.Author = result.Person;
                reviews.Add(review);
            }

            return reviews;
        }

        /// <summary>
        /// Lists the reviews for a given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        public List<Review> ListReviews(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected() || album == null)
                return null;

            var sourceTable = SourceTable(dataSource);
            var personTable = PersonTable(dataSource);
            var reviewTable = ReviewTable(dataSource);
            var albumReviewTable = AlbumReviewTable(dataSource);
            if (albumReviewTable == null)
                return null;

            var reviewsQuery =
                from ar in albumReviewTable
                join review in reviewTable on ar.ReviewId equals review.Id
                join source in sourceTable on review.SourceId equals source.Id
                from person in personTable.Where(p => review.AuthorId == p.Id).DefaultIfEmpty()
                orderby review.PublishedOn descending, review.Title
                where ar.AlbumId == album.Id
                select new
                {
                    Review = review,
                    Source = source,
                    Person = person
                };

            var results = reviewsQuery.ToList();

            if (!results.Any())
                return null;

            var reviews = new List<Review>();
            foreach (var result in results)
            {
                var review = result.Review;
                review.Source = result.Source;
                review.Author = result.Person;
                reviews.Add(review);
            }

            return reviews;
        }

        /// <summary>
        /// Adds the review.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        public void AddReview(IDataSource dataSource, Review review)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_sourceRepository.SourceExists(dataSource, review.SourceId))
                _sourceRepository.AddSource(dataSource, review.Source);

            if (review.AuthorId != null && !_personRepository.PersonExists(dataSource, review.AuthorId.Value))
                _personRepository.AddPerson(dataSource, review.Author);

            var reviewTable = ReviewTable(dataSource);
            if (reviewTable == null)
                return;

            reviewTable.InsertOnSubmit(review);
            dataSource.Save();
        }

        /// <summary>
        /// Adds the review to the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        /// <param name="song">The song.</param>
        public void AddReviewTo(IDataSource dataSource, Review review, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songReviewTable = SongReviewTable(dataSource);
            if (songReviewTable == null)
                return;

            songReviewTable.InsertOnSubmit(SongReview.Create(song, review));
            dataSource.Save();
        }

        /// <summary>
        /// Adds the review to the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        /// <param name="album">The album.</param>
        public void AddReviewTo(IDataSource dataSource, Review review, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReviewTable = AlbumReviewTable(dataSource);
            if (albumReviewTable == null)
                return;

            albumReviewTable.InsertOnSubmit(AlbumReview.Create(album, review));
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the review.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        public void DeleteReview(IDataSource dataSource, Review review)
        {
            if (!dataSource.IsConnected())
                return;

            var reviewTable = ReviewTable(dataSource);
            if (reviewTable == null)
                return;

            var songReviewTable = SongReviewTable(dataSource);
            var songReviewsQuery =
                from sr in songReviewTable
                where sr.ReviewId == review.Id
                select sr;

            songReviewTable.DeleteAllOnSubmit(songReviewsQuery.AsEnumerable());
            dataSource.Save();

            var albumReviewTable = AlbumReviewTable(dataSource);
            var albumReviewsQuery =
                from ar in albumReviewTable
                where ar.ReviewId == review.Id
                select ar;

            albumReviewTable.DeleteAllOnSubmit(albumReviewsQuery.AsEnumerable());
            dataSource.Save();

            reviewTable.DeleteOnSubmit(review);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the review from the given song.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        /// <param name="song">The song.</param>
        public void DeleteReviewFrom(IDataSource dataSource, Review review, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songReviewTable = SongReviewTable(dataSource);
            if (songReviewTable == null)
                return;

            var songReviewQuery =
                from sr in songReviewTable
                where sr.ReviewId == review.Id && sr.SongId == song.Id
                select sr;

            var songReview = songReviewQuery.SingleOrDefault();
            if (songReview == null)
                return;

            songReviewTable.DeleteOnSubmit(songReview);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the review from the given album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="review">The review.</param>
        /// <param name="album">The album.</param>
        public void DeleteReviewFrom(IDataSource dataSource, Review review, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReviewTable = AlbumReviewTable(dataSource);
            if (albumReviewTable == null)
                return;

            var albumReviewQuery =
                from ar in albumReviewTable
                where ar.ReviewId == review.Id && ar.AlbumId == album.Id
                select ar;

            var albumReview = albumReviewQuery.SingleOrDefault();
            if (albumReview == null)
                return;

            albumReviewTable.DeleteOnSubmit(albumReview);
            dataSource.Save();
        }

        #region Tables

        private static Table<Review> ReviewTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Review>();
        }

        private static Table<SongReview> SongReviewTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<SongReview>();
        }

        private static Table<AlbumReview> AlbumReviewTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<AlbumReview>();
        }

        private static IEnumerable<Source> SourceTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Source>();
        }

        private static IEnumerable<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Person>();
        }

        #endregion
    }
}
