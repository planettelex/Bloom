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
            const string url = "http://www.test.com/review";
            var review = Review.Create(url);

            Assert.AreNotEqual(review.Id, Guid.Empty);
            Assert.AreEqual(review.Url, url);
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
            var author = Person.Create(authorName);
            var review = Review.Create(title, body, author);

            Assert.AreNotEqual(review.Id, Guid.Empty);
            Assert.AreEqual(review.Title, title);
            Assert.AreEqual(review.Body, body);
            Assert.AreEqual(review.AuthorId, author.Id);
            Assert.AreEqual(review.Author.Name, authorName);
        }
    }
}