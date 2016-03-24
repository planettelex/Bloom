using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the review model class.
    /// </summary>
    [TestFixture]
    public class ReviewModelTests
    {
        /// <summary>
        /// Tests the review create method.
        /// </summary>
        [Test]
        public void CreateReviewTest()
        {
            var source = Source.Create("Source");
            const string url = "http://www.test.com/review";
            var review = Review.Create(source, url);

            Assert.AreNotEqual(review.Id, Guid.Empty);
            Assert.AreEqual(review.Url, url);
            Assert.NotNull(review.Source);
            Assert.AreEqual(source.Id, review.SourceId);
        }

        /// <summary>
        /// Tests the review create method.
        /// </summary>
        [Test]
        public void CreateLocalReviewTest()
        {
            const string title = "Test Title";
            const string body = "Test Body";
            const string authorName = "Test Author";
            var source = Source.Create("Source");
            var author = Person.Create(authorName);
            var review = Review.Create(source, title, body, author);

            Assert.AreNotEqual(review.Id, Guid.Empty);
            Assert.AreEqual(review.Title, title);
            Assert.AreEqual(review.Body, body);
            Assert.AreEqual(review.AuthorId, author.Id);
            Assert.AreEqual(review.Author.Name, authorName);
            Assert.NotNull(review.Source);
            Assert.AreEqual(source.Id, review.SourceId);
        }

        /// <summary>
        /// Tests the review properties.
        /// </summary>
        [Test]
        public void ReviewPropertiesTest()
        {
            var id = Guid.NewGuid();
            const string authorName = "Test Author";
            var source = Source.Create("Source");
            var author = Person.Create(authorName);
            var review = new Review
            {
                Id = id,
                Source = source,
                Author = author,
                Body = "Body",
                Url = "http://www.test.com/review",
                Title = "Title",
                PublishedOn = DateTime.Parse("6/9/1999")
            };

            Assert.AreEqual(review.Id, id);
            Assert.AreEqual(review.SourceId, source.Id);
            Assert.NotNull(review.Source);
            Assert.AreEqual(review.AuthorId, author.Id);
            Assert.NotNull(review.Author);
            Assert.AreEqual(review.Body, "Body");
            Assert.AreEqual(review.Title, "Title");
            Assert.AreEqual(review.Url, "http://www.test.com/review");
            Assert.AreEqual(review.PublishedOn, DateTime.Parse("6/9/1999"));
        }

        /// <summary>
        /// Tests the review to string method.
        /// </summary>
        [Test]
        public void ReviewToStringTest()
        {
            var source = Source.Create("Source");
            const string url = "http://www.test.com/review";
            var review = Review.Create(source, url);

            Assert.AreEqual(review.ToString(), url);

            const string title = "Test Title";
            const string body = "Test Body";
            const string authorName = "Test Author";
            var author = Person.Create(authorName);
            review = Review.Create(source, title, body, author);

            Assert.AreEqual(review.ToString(), "Test Title");
        }
    }
}