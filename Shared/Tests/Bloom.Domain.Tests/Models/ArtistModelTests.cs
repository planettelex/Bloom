using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the artist model class.
    /// </summary>
    [TestFixture]
    public class ArtistModelTests
    {
        private const string ArtistName = "Test Artist";

        /// <summary>
        /// Tests the artist create method.
        /// </summary>
        [Test]
        public void CreateArtistTest()
        {
            var artist = Artist.Create(ArtistName);

            Assert.AreNotEqual(artist.Id, Guid.Empty);
            Assert.AreEqual(artist.Name, ArtistName);
        }

        /// <summary>
        /// Tests adding a member to an artist.
        /// </summary>
        [Test]
        public void AddMemberToArtistTest()
        {
            const string personName = "Test Person";
            var artist = Artist.Create(ArtistName);
            var person = Person.Create(personName);
            // todo

            Assert.AreEqual(artist.Members.Count, 1);
        }

        /// <summary>
        /// Tests adding photo to an artist.
        /// </summary>
        [Test]
        public void AddPhotoToArtistTest()
        {
            const string url1 = "http://www.test.com/image1.jpg";
            const string url2 = "http://www.test.com/image2.jpg";
            var artist = Artist.Create(ArtistName);
            var photo1 = Photo.Create(url1);
            var photo2 = Photo.Create(url2);
            // todo

            Assert.AreEqual(artist.Photos.Count, 2);
        }
    }
}