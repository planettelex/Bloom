using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
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
                join source in sourceTable on r.SourceId equals source.Id
                join person in personTable on r.AuthorId equals person.Id
                where r.Id == reviewId
                select new Review
                {
                    Id = r.Id,
                    Title = r.Title,
                    Body = r.Body,
                    PublishedOn = r.PublishedOn,
                    Url = r.Url,
                    SourceId = r.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    },
                    AuthorId = r.AuthorId,
                    Author = r.AuthorId == Guid.Empty ? null : new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        Bio = person.Bio,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn,
                        Twitter = person.Twitter
                    }
                };

            return reviewQuery.SingleOrDefault();
        }

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

            var reviewQuery =
                from sr in songReviewTable
                join review in reviewTable on sr.ReviewId equals review.Id
                join source in sourceTable on review.SourceId equals source.Id
                join person in personTable on review.AuthorId equals person.Id
                where sr.SongId == song.Id
                select new Review
                {
                    Id = review.Id,
                    Title = review.Title,
                    PublishedOn = review.PublishedOn,
                    Url = review.Url,
                    SourceId = review.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    },
                    AuthorId = review.AuthorId,
                    Author = review.AuthorId == Guid.Empty ? null : new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn,
                        Twitter = person.Twitter
                    }
                };

            return reviewQuery.ToList();
        }

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

            var reviewQuery =
                from ar in albumReviewTable
                join review in reviewTable on ar.ReviewId equals review.Id
                join source in sourceTable on review.SourceId equals source.Id
                join person in personTable on review.AuthorId equals person.Id
                where ar.AlbumId == album.Id
                select new Review
                {
                    Id = review.Id,
                    Title = review.Title,
                    PublishedOn = review.PublishedOn,
                    Url = review.Url,
                    SourceId = review.SourceId,
                    Source = new Source
                    {
                        Id = source.Id,
                        Name = source.Name,
                        Type = source.Type,
                        WebsiteUrl = source.WebsiteUrl
                    },
                    AuthorId = review.AuthorId,
                    Author = review.AuthorId == Guid.Empty ? null : new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn,
                        Twitter = person.Twitter
                    }
                };

            return reviewQuery.ToList();
        }

        public void AddReview(IDataSource dataSource, Review review)
        {
            if (!dataSource.IsConnected())
                return;

            var reviewTable = ReviewTable(dataSource);
            if (reviewTable == null)
                return;

            reviewTable.InsertOnSubmit(review);
        }

        public void AddReviewTo(IDataSource dataSource, Review review, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songReviewTable = SongReviewTable(dataSource);
            if (songReviewTable == null)
                return;

            songReviewTable.InsertOnSubmit(SongReview.Create(song, review));
        }

        public void AddReviewTo(IDataSource dataSource, Review review, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReviewTable = AlbumReviewTable(dataSource);
            if (albumReviewTable == null)
                return;

            albumReviewTable.InsertOnSubmit(AlbumReview.Create(album, review));
        }

        public void DeleteReview(IDataSource dataSource, Review review)
        {
            if (!dataSource.IsConnected())
                return;

            var reviewTable = ReviewTable(dataSource);
            if (reviewTable == null)
                return;

            reviewTable.DeleteOnSubmit(review);
        }

        public void DeleteReviewFrom(IDataSource dataSource, Review review, Song song)
        {
            if (!dataSource.IsConnected())
                return;

            var songReviewTable = SongReviewTable(dataSource);
            if (songReviewTable == null)
                return;

            songReviewTable.DeleteOnSubmit(SongReview.Create(song, review));
        }

        public void DeleteReviewFrom(IDataSource dataSource, Review review, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReviewTable = AlbumReviewTable(dataSource);
            if (albumReviewTable == null)
                return;

            albumReviewTable.DeleteOnSubmit(AlbumReview.Create(album, review));
        }

        private static Table<Review> ReviewTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Review>() : null;
        }

        private static Table<SongReview> SongReviewTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<SongReview>() : null;
        }

        private static Table<AlbumReview> AlbumReviewTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumReview>() : null;
        }

        private static IEnumerable<Source> SourceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Source>() : null;
        }

        private static IEnumerable<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }
    }
}
