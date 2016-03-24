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

        /// <summary>
        /// Tests the genres properties.
        /// </summary>
        [Test]
        public void GenrePropertiesTest()
        {
            var parentGenre = Genre.Create("Parent");
            var id = Guid.NewGuid();
            var genre = new Genre
            {
                Id = id,
                Name = GenreName,
                Description = "Genre description.",
                ParentGenre = parentGenre
            };

            Assert.AreEqual(genre.Id, id);
            Assert.AreEqual(genre.Name, GenreName);
            Assert.AreEqual(genre.Description, "Genre description.");
            Assert.NotNull(genre.ParentGenre);
            Assert.AreEqual(genre.ParentGenreId, parentGenre.Id);
        }

        /// <summary>
        /// Tests the genre to string method.
        /// </summary>
        [Test]
        public void GenreToStringTest()
        {
            var genre = Genre.Create(GenreName);

            Assert.AreEqual(genre.ToString(), GenreName);
        }
    }
}