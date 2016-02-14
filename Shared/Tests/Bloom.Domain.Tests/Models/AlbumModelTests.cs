using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the album model class.
    /// </summary>
    [TestFixture]
    public class AlbumModelTests
    {
        private const string AlbumName = "Test Album";

        /// <summary>
        /// Tests the album create method.
        /// </summary>
        [Test]
        public void CreateAlbumTest()
        {
            var album = Album.Create(AlbumName);

            Assert.AreNotEqual(album.Id, Guid.Empty);
            Assert.AreEqual(album.Name, AlbumName);
        }

        /// <summary>
        /// Tests the album create with artist method overload.
        /// </summary>
        [Test]
        public void CreateAlbumWithArtistTest()
        {
            const string artistName = "Test Artist";
            var artist = Artist.Create(artistName);
            var album = Album.Create(AlbumName, artist);

            Assert.AreNotEqual(album.Id, Guid.Empty);
            Assert.AreEqual(album.Name, AlbumName);
            Assert.IsNotNull(album.Artist);
            Assert.AreEqual(album.ArtistId, artist.Id);
            Assert.AreEqual(album.Artist.Name, artistName);
        }

        

        /// <summary>
        /// Tests adding artwork to an album.
        /// </summary>
        [Test]
        public void AddArtworkToAlbumTest()
        {
            const string url1 = "http://www.test.com/image1.jpg";
            const string url2 = "http://www.test.com/image2.jpg";
            var album = Album.Create(AlbumName);
            // todo

            Assert.AreEqual(album.Artwork.Count, 2);
        }

        /// <summary>
        /// Tests adding a collaborator to an album.
        /// </summary>
        [Test]
        public void AddCollaboratorToAlbumTest()
        {
            const string artistName = "Test Collaborator";
            var album = Album.Create(AlbumName);
            var artist = Artist.Create(artistName);
            // todo

            Assert.AreEqual(album.Collaborators.Count, 1);
        }

        /// <summary>
        /// Tests adding a credit to an album.
        /// </summary>
        [Test]
        public void AddCreditToAlbumTest()
        {
            const string personName = "Test Person";
            var album = Album.Create(AlbumName);
            var person = Person.Create(personName);
            // todo

            Assert.AreEqual(album.Credits.Count, 1);
        }

        /// <summary>
        /// Tests adding a credit with roles to an album.
        /// </summary>
        [Test]
        public void AddCreditWithRolesToAlbumTest()
        {
            const string personName = "Test Person";
            const string role1Name = "Producer";
            const string role2Name = "Engineer";
            var album = Album.Create(AlbumName);
            var person = Person.Create(personName);
            // todo
            var role1 = Role.Create(role1Name);
            var role2 = Role.Create(role2Name);
        }

      

        /// <summary>
        /// Tests adding a review to an album.
        /// </summary>
        [Test]
        public void AddReviewToAlbumTest()
        {
            const string reviewUrl = "http://www.test.com/review";
            const string reviewTitle = "Review Title";
            const string reviewBody = "Review Body";
            const string reviewAuthorName = "Review Author";
            var album = Album.Create(AlbumName);
            var reviewAuthor = Person.Create(reviewAuthorName);
            var review1 = Review.Create(reviewUrl);
            // todo
            var review2 = Review.Create(reviewTitle, reviewBody, reviewAuthor);
            
        }

        /// <summary>
        /// Tests adding a track to an album.
        /// </summary>
        [Test]
        public void AddTrackToAlbumTest()
        {
            const string song1Name = "Test Song 1";
            const string song2Name = "Test Song 2";
            const string song3Name = "Test Song 3";
            const string artistName = "Artist Name";
            var artist = Artist.Create(artistName);
            var album = Album.Create(AlbumName);
            var song1 = Song.Create(song1Name, artist);
            var song2 = Song.Create(song2Name, artist);
            var song3 = Song.Create(song3Name, artist);
            // todo

            Assert.AreEqual(album.Tracks.Count, 3);
           
        }
    }
}