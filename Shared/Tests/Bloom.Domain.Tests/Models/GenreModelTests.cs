using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the genre model class.
    /// </summary>
    [TestFixture]
    public class GenreModelTests
    {
        private const string GenreName = "Test Genre";

        /// <summary>
        /// Tests the genre create method.
        /// </summary>
        [Test]
        public void CreateGenreTest()
        {
            
            var genre = Genre.Create(GenreName);

            Assert.AreNotEqual(genre.Id, Guid.Empty);
            Assert.AreEqual(genre.Name, GenreName);
        }

        /// <summary>
        /// Tests the genre create method with a parent genre.
        /// </summary>
        [Test]
        public void CreateGenreWithParentTest()
        {
            const string parentGenreName = "Parent Genre";
            var parentGenre = Genre.Create(parentGenreName);
            var genre = Genre.Create(GenreName, parentGenre);

            Assert.AreNotEqual(genre.Id, Guid.Empty);
            Assert.AreEqual(genre.Name, GenreName);
            Assert.AreEqual(genre.ParentGenreId, parentGenre.Id);
            Assert.AreEqual(genre.ParentGenre.Name, parentGenreName);
        }
    }
}