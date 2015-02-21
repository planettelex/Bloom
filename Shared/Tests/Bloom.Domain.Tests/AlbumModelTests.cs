using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests
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
        /// Tests adding an activity to an album.
        /// </summary>
        [Test]
        public void AddActivityToAlbumTest()
        {
            const string activityName = "Test Activity";
            var album = Album.Create(AlbumName);
            var activity = Activity.Create(activityName);
            var albumActivity = album.AddActivity(activity);

            Assert.AreEqual(album.Activities.Count, 1);
            Assert.AreEqual(albumActivity.AlbumId, album.Id);
            Assert.AreEqual(albumActivity.ActivityId, activity.Id);
            Assert.AreEqual(albumActivity.Activity.Name, activityName);
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
            var albumArtwork1 = album.AddArtwork(url1);
            var albumArtwork2 = album.AddArtwork(url2);

            Assert.AreEqual(album.Artwork.Count, 2);
            Assert.AreEqual(albumArtwork1.AlbumId, album.Id);
            Assert.AreEqual(albumArtwork1.Url, url1);
            Assert.AreEqual(albumArtwork1.Priority, 1);
            Assert.AreEqual(albumArtwork2.AlbumId, album.Id);
            Assert.AreEqual(albumArtwork2.Url, url2);
            Assert.AreEqual(albumArtwork2.Priority, 2);
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
            var albumCollaborator = album.AddCollaborator(artist);

            Assert.AreEqual(album.Collaborators.Count, 1);
            Assert.AreEqual(albumCollaborator.AlbumId, album.Id);
            Assert.AreEqual(albumCollaborator.ArtistId, artist.Id);
            Assert.AreEqual(albumCollaborator.Artist.Name, artistName);
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
            var albumCredit = album.AddCredit(person);

            Assert.AreEqual(album.Credits.Count, 1);
            Assert.AreEqual(albumCredit.AlbumId, album.Id);
            Assert.AreEqual(albumCredit.PersonId, person.Id);
            Assert.AreEqual(albumCredit.Person.Name, personName);
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
            var albumCredit = album.AddCredit(person);
            var role1 = Role.Create(role1Name);
            var role2 = Role.Create(role2Name);
            var albumCreditRole1 = albumCredit.AddRole(role1);
            var albumCreditRole2 = albumCredit.AddRole(role2);

            Assert.AreEqual(album.Credits.Count, 1);
            Assert.AreEqual(albumCredit.AlbumId, album.Id);
            Assert.AreEqual(albumCredit.PersonId, person.Id);
            Assert.AreEqual(albumCredit.Person.Name, personName);
            Assert.AreEqual(albumCredit.Roles.Count, 2);
            Assert.AreEqual(albumCreditRole1.RoleId, role1.Id);
            Assert.AreEqual(albumCreditRole1.Role.Name, role1Name);
            Assert.AreEqual(albumCreditRole2.RoleId, role2.Id);
            Assert.AreEqual(albumCreditRole2.Role.Name, role2Name);
        }

        /// <summary>
        /// Tests adding a mood to an album.
        /// </summary>
        [Test]
        public void AddMoodToAlbumTest()
        {
            const string moodName = "Test Mood";
            var album = Album.Create(AlbumName);
            var mood = Mood.Create(moodName);
            var albumMood = album.AddMood(mood);

            Assert.AreEqual(album.Moods.Count, 1);
            Assert.AreEqual(albumMood.AlbumId, album.Id);
            Assert.AreEqual(albumMood.MoodId, mood.Id);
            Assert.AreEqual(albumMood.Mood.Name, moodName);
        }

        /// <summary>
        /// Tests adding a reference to an album.
        /// </summary>
        [Test]
        public void AddReferenceToAlbumTest()
        {
            const string referenceName = "Test Reference";
            const string referenceUrl = "http://www.test.com/";
            var album = Album.Create(AlbumName);
            var reference = Reference.Create(referenceName, referenceUrl);
            var albumReference = album.AddReference(reference);

            Assert.AreEqual(album.References.Count, 1);
            Assert.AreEqual(albumReference.AlbumId, album.Id);
            Assert.AreEqual(albumReference.ReferenceId, reference.Id);
            Assert.AreEqual(albumReference.Reference.Name, referenceName);
            Assert.AreEqual(albumReference.Reference.Url, referenceUrl);
        }

        /// <summary>
        /// Tests adding a release to an album.
        /// </summary>
        [Test]
        public void AddReleaseToAlbumTest()
        {
            var releaseDate = DateTime.Now.AddDays(-900);
            var album = Album.Create(AlbumName);
            var albumRelease = album.AddRelease(releaseDate);

            Assert.AreEqual(album.Releases.Count, 1);
            Assert.AreEqual(albumRelease.AlbumId, album.Id);
            Assert.AreEqual(albumRelease.ReleaseDate, releaseDate);
        }

        /// <summary>
        /// Tests adding a release with media types to an album.
        /// </summary>
        [Test]
        public void AddReleaseWithMediaTypesToAlbumTest()
        {
            var releaseDate = DateTime.Now.AddDays(-900);
            var album = Album.Create(AlbumName);
            var albumRelease = album.AddRelease(releaseDate, MediaTypes.CD | MediaTypes.Vinyl);

            Assert.AreEqual(album.Releases.Count, 1);
            Assert.AreEqual(albumRelease.AlbumId, album.Id);
            Assert.AreEqual(albumRelease.ReleaseDate, releaseDate);
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.CD));
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.Vinyl));
            Assert.IsFalse(albumRelease.MediaTypes.HasFlag(MediaTypes.Cassette));
        }

        /// <summary>
        /// Tests adding a release with media types and digital formats to an album.
        /// </summary>
        [Test]
        public void AddReleaseWithDigitalFormatsToAlbumTest()
        {
            var releaseDate = DateTime.Now.AddDays(-900);
            var album = Album.Create(AlbumName);
            var albumRelease = album.AddRelease(releaseDate, MediaTypes.Digital | MediaTypes.CD | MediaTypes.Vinyl, DigitalFormats.MP3 | DigitalFormats.M4A | DigitalFormats.FLAC);

            Assert.AreEqual(album.Releases.Count, 1);
            Assert.AreEqual(albumRelease.AlbumId, album.Id);
            Assert.AreEqual(albumRelease.ReleaseDate, releaseDate);
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.Digital));
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.CD));
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.Vinyl));
            Assert.IsFalse(albumRelease.MediaTypes.HasFlag(MediaTypes.Cassette));
            Assert.IsTrue(albumRelease.DigitalFormats.HasFlag(DigitalFormats.MP3));
            Assert.IsTrue(albumRelease.DigitalFormats.HasFlag(DigitalFormats.M4A));
            Assert.IsTrue(albumRelease.DigitalFormats.HasFlag(DigitalFormats.FLAC));
            Assert.IsFalse(albumRelease.DigitalFormats.HasFlag(DigitalFormats.OGG));
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
            var albumReview1 = album.AddReview(review1);
            var review2 = Review.Create(reviewTitle, reviewBody, reviewAuthor);
            var albumReview2 = album.AddReview(review2);

            Assert.AreEqual(album.Reviews.Count, 2);
            Assert.AreEqual(albumReview1.AlbumId, album.Id);
            Assert.AreEqual(albumReview1.ReviewId, review1.Id);
            Assert.AreEqual(albumReview1.Review.Url, reviewUrl);
            Assert.AreEqual(albumReview2.AlbumId, album.Id);
            Assert.AreEqual(albumReview2.ReviewId, review2.Id);
            Assert.AreEqual(albumReview2.Review.AuthorId, reviewAuthor.Id);
            Assert.AreEqual(albumReview2.Review.Author.Name, reviewAuthor.Name);
            Assert.AreEqual(albumReview2.Review.Title, reviewTitle);
            Assert.AreEqual(albumReview2.Review.Body, reviewBody);
        }

        /// <summary>
        /// Tests adding an tag to an album.
        /// </summary>
        [Test]
        public void AddTagToAlbumTest()
        {
            const string tagName = "Test Tag";
            var album = Album.Create(AlbumName);
            var tag = Tag.Create(tagName);
            var albumTag = album.AddTag(tag);

            Assert.AreEqual(album.Tags.Count, 1);
            Assert.AreEqual(albumTag.AlbumId, album.Id);
            Assert.AreEqual(albumTag.TagId, tag.Id);
            Assert.AreEqual(albumTag.Tag.Name, tagName);
        }

        /// <summary>
        /// Tests adding an track to an album.
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
            var albumTrack1 = album.AddTrack(song1, 1);
            var albumTrack2 = album.AddTrack(song2, 2);
            var albumTrack3 = album.AddTrack(song3, 3);

            Assert.AreEqual(album.Tracks.Count, 3);
            Assert.AreEqual(albumTrack1.AlbumId, album.Id);
            Assert.AreEqual(albumTrack1.SongId, song1.Id);
            Assert.AreEqual(albumTrack1.Song.Name, song1.Name);
            Assert.AreEqual(albumTrack1.TrackNumber, 1);
            Assert.AreEqual(albumTrack2.AlbumId, album.Id);
            Assert.AreEqual(albumTrack2.SongId, song2.Id);
            Assert.AreEqual(albumTrack2.Song.Name, song2.Name);
            Assert.AreEqual(albumTrack2.TrackNumber, 2);
            Assert.AreEqual(albumTrack3.AlbumId, album.Id);
            Assert.AreEqual(albumTrack3.SongId, song3.Id);
            Assert.AreEqual(albumTrack3.Song.Name, song3.Name);
            Assert.AreEqual(albumTrack3.TrackNumber, 3);
        }
    }
}