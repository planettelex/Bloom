using System;
using System.Collections.Generic;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the artist model classes.
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
        /// Tests the artist properties.
        /// </summary>
        [Test]
        public void ArtistPropertiesTest()
        {
            var id = Guid.NewGuid();
            var artist = new Artist
            {
                Id = id,
                Name = ArtistName,
                Bio = "Bio",
                StartedOn = DateTime.Parse("1/1/2001"),
                EndedOn = DateTime.Parse("12/12/2012"),
                Twitter = "Twitter",
                IsSolo = true,
                Notes = "Notes",
                Follow = true,
                Rating = 3,
                PlayCount = 100,
                SkipCount = 5,
                RemoveCount = 3
            };

            var photo = Photo.Create("c:\\images\\photo.jpg");
            artist.ProfileImage = photo;

            Assert.AreEqual(artist.Id, id);
            Assert.AreEqual(artist.Name, ArtistName);
            Assert.AreEqual(artist.Bio, "Bio");
            Assert.AreEqual(artist.Notes, "Notes");
            Assert.AreEqual(artist.Twitter, "Twitter");
            Assert.AreEqual(artist.StartedOn, DateTime.Parse("1/1/2001"));
            Assert.AreEqual(artist.EndedOn, DateTime.Parse("12/12/2012"));
            Assert.AreEqual(artist.IsSolo, true);
            Assert.AreEqual(artist.Follow, true);
            Assert.AreEqual(artist.Rating, 3);
            Assert.AreEqual(artist.PlayCount, 100);
            Assert.AreEqual(artist.SkipCount, 5);
            Assert.AreEqual(artist.RemoveCount, 3);
            Assert.AreEqual(artist.ProfileImage.Id, photo.Id);
            Assert.AreEqual(artist.ProfileImage.FilePath, "c:\\images\\photo.jpg");

            var photo2 = Photo.Create("c:\\images\\photo2.jpg");
            artist.ProfileImage = photo2;

            Assert.AreEqual(artist.ProfileImage.Id, photo2.Id);
            Assert.AreEqual(artist.ProfileImage.FilePath, "c:\\images\\photo2.jpg");

            artist.Photos.Clear();
            artist.ProfileImage = photo;

            Assert.AreEqual(artist.ProfileImage.Id, photo.Id);
            Assert.AreEqual(artist.ProfileImage.FilePath, "c:\\images\\photo.jpg");
        }

        /// <summary>
        /// Tests the artist member create method.
        /// </summary>
        [Test]
        public void CreateArtistMemberTest()
        {
            var artist = Artist.Create(ArtistName);
            var person = Person.Create("Member");
            var artistMember = ArtistMember.Create(artist, person, 1);

            Assert.AreNotEqual(artistMember.Id, Guid.Empty);
            Assert.AreEqual(artistMember.ArtistId, artist.Id);
            Assert.AreEqual(artistMember.PersonId, person.Id);
            Assert.AreEqual("Member", artistMember.Person.Name);
            Assert.AreEqual(1, artistMember.Priority);
        }

        /// <summary>
        /// Tests the artist member properties.
        /// </summary>
        [Test]
        public void ArtistMemberPropertiesTest()
        {
            var id = Guid.NewGuid();
            var artistId = Guid.NewGuid();
            var person = Person.Create("Person");
            var roles = new List<Role>
            {
                Role.Create("Role 1"),
                Role.Create("Role 2")
            };

            var artistMember = new ArtistMember
            {
                Id = id,
                ArtistId = artistId,
                Person = person,
                Priority = 1,
                Started = DateTime.Parse("1/9/1969"),
                Ended = DateTime.Parse("10/20/1980"),
                Roles = roles
            };

            Assert.AreEqual(id, artistMember.Id);
            Assert.AreEqual(artistId, artistMember.ArtistId);
            Assert.AreEqual(person.Id, artistMember.PersonId);
            Assert.NotNull(artistMember.Person);
            Assert.AreEqual(1, artistMember.Priority);
            Assert.AreEqual(DateTime.Parse("1/9/1969"), artistMember.Started);
            Assert.AreEqual(DateTime.Parse("10/20/1980"), artistMember.Ended);
            Assert.NotNull(artistMember.Roles);
            Assert.AreEqual(2, artistMember.Roles.Count);
        }

        /// <summary>
        /// Tests the artist member to string method.
        /// </summary>
        [Test]
        public void ArtistMemberToStringTest()
        {
            var artist = Artist.Create(ArtistName);
            var person = Person.Create("Person");
            var artistMember = ArtistMember.Create(artist, person, 1);

            Assert.AreEqual(artistMember.ToString(), "Person");
        }

        /// <summary>
        /// Tests the artist member role create method.
        /// </summary>
        [Test]
        public void CreateArtistMemberRoleTest()
        {
            var artist = Artist.Create(ArtistName);
            var person = Person.Create("Member");
            var artistMember = ArtistMember.Create(artist, person, 1);
            var role = Role.Create("Role");
            var artistMemberRole = ArtistMemberRole.Create(artistMember, role);

            Assert.AreEqual(artistMemberRole.ArtistMemberId, artistMember.Id);
            Assert.AreEqual(artistMemberRole.RoleId, role.Id);
        }

        /// <summary>
        /// Tests the artist photo create method.
        /// </summary>
        [Test]
        public void CreateArtistPhotoTest()
        {
            var artist = Artist.Create(ArtistName);
            var photo = Photo.Create("c:\\images\\photo.jpg");
            var artistPhoto = ArtistPhoto.Create(artist, photo, 2);

            Assert.AreEqual(artistPhoto.ArtistId, artist.Id);
            Assert.AreEqual(artistPhoto.PhotoId, photo.Id);
            Assert.AreEqual(artistPhoto.Priority, 2);
        }

        /// <summary>
        /// Tests the artist reference create method.
        /// </summary>
        [Test]
        public void CreateArtistReferenceTest()
        {
            var artist = Artist.Create(ArtistName);
            var reference = Reference.Create("Reference Title", "http://www.test.com");
            var artistReference = ArtistReference.Create(artist, reference);

            Assert.AreEqual(artistReference.ArtistId, artist.Id);
            Assert.AreEqual(artistReference.ReferenceId, reference.Id);
        }

        /// <summary>
        /// Tests the artist to string method.
        /// </summary>
        [Test]
        public void ArtistToStringTest()
        {
            var artist = Artist.Create(ArtistName);
            Assert.AreEqual(artist.ToString(), artist.Name);
        }
    }
}