using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests
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
            var artistMember = artist.AddMember(person);

            Assert.AreEqual(artist.Members.Count, 1);
            Assert.AreEqual(artistMember.ArtistId, artist.Id);
            Assert.AreEqual(artistMember.PersonId, person.Id);
            Assert.AreEqual(artistMember.Person.Name, person.Name);
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
            var artistPhoto1 = artist.AddPhoto(photo1);
            var artistPhoto2 = artist.AddPhoto(photo2);

            Assert.AreEqual(artist.Photos.Count, 2);
            Assert.AreEqual(artistPhoto1.ArtistId, artist.Id);
            Assert.AreEqual(artistPhoto1.PhotoId, photo1.Id);
            Assert.AreEqual(artistPhoto1.Photo.Url, url1);
            Assert.AreEqual(artistPhoto1.Priority, 1);
            Assert.AreEqual(artistPhoto2.ArtistId, artist.Id);
            Assert.AreEqual(artistPhoto2.PhotoId, photo2.Id);
            Assert.AreEqual(artistPhoto2.Photo.Url, url2);
            Assert.AreEqual(artistPhoto2.Priority, 2);
        }

        /// <summary>
        /// Tests adding a reference to an artist.
        /// </summary>
        [Test]
        public void AddReferenceToArtistTest()
        {
            const string referenceName = "Test Reference";
            const string referenceUrl = "http://www.test.com/";
            var artist = Artist.Create(ArtistName);
            var reference = Reference.Create(referenceName, referenceUrl);
            var artistReference = artist.AddReference(reference);

            Assert.AreEqual(artist.References.Count, 1);
            Assert.AreEqual(artistReference.ArtistId, artist.Id);
            Assert.AreEqual(artistReference.ReferenceId, reference.Id);
            Assert.AreEqual(artistReference.Reference.Name, referenceName);
            Assert.AreEqual(artistReference.Reference.Url, referenceUrl);
        }
    }
}