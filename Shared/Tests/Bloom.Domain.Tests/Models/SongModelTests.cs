using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the song model class.
    /// </summary>
    [TestFixture]
    public class SongModelTests
    {
        private const string ArtistName = "Test Artist";
        private const string SongName = "Test Song";

        /// <summary>
        /// Tests the song create method.
        /// </summary>
        [Test]
        public void CreateSongTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);

            Assert.AreNotEqual(song.Id, Guid.Empty);
            Assert.AreEqual(song.Name, SongName);
            Assert.AreEqual(song.ArtistId, artist.Id);
            Assert.AreEqual(song.Artist.Name, ArtistName);
        }

        /// <summary>
        /// Tests adding a recording session to a song.
        /// </summary>
        [Test]
        public void AddRecordingSessionToSongTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var occurredOn = DateTime.Now.AddYears(-20);
            var recordingSession = song.AddRecordingSession(occurredOn);

            
            Assert.AreEqual(song.RecordingSessions.Count, 1);
            Assert.AreNotEqual(recordingSession.Id, Guid.Empty);
            Assert.AreEqual(recordingSession.SongId, song.Id);
            Assert.AreEqual(recordingSession.OccurredOn, occurredOn);
        }

        /// <summary>
        /// Tests adding an activity to a song.
        /// </summary>
        [Test]
        public void AddActivityToSongTest()
        {
            const string activityName = "Test Activity";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var activity = Activity.Create(activityName);
            var songActivity = song.AddActivity(activity);

            Assert.AreEqual(song.Activities.Count, 1);
            Assert.AreEqual(songActivity.SongId, song.Id);
            Assert.AreEqual(songActivity.ActivityId, activity.Id);
            Assert.AreEqual(songActivity.Activity.Name, activityName);
        }

        /// <summary>
        /// Tests adding a collaborator to a song.
        /// </summary>
        [Test]
        public void AddCollaboratorToSongTest()
        {
            const string collaboratorName = "Test Collaborator";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var collaborator = Artist.Create(collaboratorName);
            var songCollaborator = song.AddCollaborator(collaborator);

            Assert.AreEqual(song.Collaborators.Count, 1);
            Assert.AreEqual(songCollaborator.SongId, song.Id);
            Assert.AreEqual(songCollaborator.ArtistId, collaborator.Id);
            Assert.AreEqual(songCollaborator.Artist.Name, collaboratorName);
        }

        /// <summary>
        /// Tests adding a credit to a song.
        /// </summary>
        [Test]
        public void AddCreditToSongTest()
        {
            const string personName = "Test Person";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var person = Person.Create(personName);
            var songCredit = song.AddCredit(person);

            Assert.AreEqual(song.Credits.Count, 1);
            Assert.AreEqual(songCredit.SongId, song.Id);
            Assert.AreEqual(songCredit.PersonId, person.Id);
            Assert.AreEqual(songCredit.Person.Name, personName);
        }

        /// <summary>
        /// Tests adding a credit with roles to a song.
        /// </summary>
        [Test]
        public void AddCreditWithRolesToSongTest()
        {
            const string personName = "Test Person";
            const string role1Name = "Producer";
            const string role2Name = "Engineer";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var person = Person.Create(personName);
            var songCredit = song.AddCredit(person);
            var role1 = Role.Create(role1Name);
            var role2 = Role.Create(role2Name);
            var songCreditRole1 = songCredit.AddRole(role1);
            var songCreditRole2 = songCredit.AddRole(role2);

            Assert.AreEqual(song.Credits.Count, 1);
            Assert.AreEqual(songCredit.SongId, song.Id);
            Assert.AreEqual(songCredit.PersonId, person.Id);
            Assert.AreEqual(songCredit.Person.Name, personName);
            Assert.AreEqual(songCredit.Roles.Count, 2);
            Assert.AreEqual(songCreditRole1.RoleId, role1.Id);
            Assert.AreEqual(songCreditRole1.Role.Name, role1Name);
            Assert.AreEqual(songCreditRole2.RoleId, role2.Id);
            Assert.AreEqual(songCreditRole2.Role.Name, role2Name);
        }

        /// <summary>
        /// Tests adding a mood to a song.
        /// </summary>
        [Test]
        public void AddMoodToSongTest()
        {
            const string moodName = "Test Mood";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var mood = Mood.Create(moodName);
            var songMood = song.AddMood(mood);

            Assert.AreEqual(song.Moods.Count, 1);
            Assert.AreEqual(songMood.SongId, song.Id);
            Assert.AreEqual(songMood.MoodId, mood.Id);
            Assert.AreEqual(songMood.Mood.Name, moodName);
        }

        /// <summary>
        /// Tests adding a reference to a song.
        /// </summary>
        [Test]
        public void AddReferenceToSongTest()
        {
            const string referenceName = "Test Reference";
            const string referenceUrl = "http://www.test.com/";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var reference = Reference.Create(referenceName, referenceUrl);
            var songReference = song.AddReference(reference);

            Assert.AreEqual(song.References.Count, 1);
            Assert.AreEqual(songReference.SongId, song.Id);
            Assert.AreEqual(songReference.ReferenceId, reference.Id);
            Assert.AreEqual(songReference.Reference.Name, referenceName);
            Assert.AreEqual(songReference.Reference.Url, referenceUrl);
        }

        /// <summary>
        /// Tests adding a review to a song.
        /// </summary>
        [Test]
        public void AddReviewToSongTest()
        {
            const string reviewUrl = "http://www.test.com/review";
            const string reviewTitle = "Review Title";
            const string reviewBody = "Review Body";
            const string reviewAuthorName = "Review Author";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var reviewAuthor = Person.Create(reviewAuthorName);
            var review1 = Review.Create(reviewUrl);
            var albumReview1 = song.AddReview(review1);
            var review2 = Review.Create(reviewTitle, reviewBody, reviewAuthor);
            var albumReview2 = song.AddReview(review2);

            Assert.AreEqual(song.Reviews.Count, 2);
            Assert.AreEqual(albumReview1.SongId, song.Id);
            Assert.AreEqual(albumReview1.ReviewId, review1.Id);
            Assert.AreEqual(albumReview1.Review.Url, reviewUrl);
            Assert.AreEqual(albumReview2.SongId, song.Id);
            Assert.AreEqual(albumReview2.ReviewId, review2.Id);
            Assert.AreEqual(albumReview2.Review.AuthorId, reviewAuthor.Id);
            Assert.AreEqual(albumReview2.Review.Author.Name, reviewAuthor.Name);
            Assert.AreEqual(albumReview2.Review.Title, reviewTitle);
            Assert.AreEqual(albumReview2.Review.Body, reviewBody);
        }

        /// <summary>
        /// Tests adding a segment to a song.
        /// </summary>
        [Test]
        public void AddSegmentToSongTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            song.Length = 100000;
            var songSegment = song.AddSegment(0, 50000);

            Assert.AreNotEqual(songSegment.Id, Guid.Empty);
            Assert.AreEqual(song.Segments.Count, 1);
            Assert.AreEqual(songSegment.SongId, song.Id);
            Assert.AreEqual(songSegment.StartTime, 0);
            Assert.AreEqual(songSegment.StopTime, 50000);
        }

        /// <summary>
        /// Tests adding an tag to a song.
        /// </summary>
        [Test]
        public void AddTagToSongTest()
        {
            const string tagName = "Test Tag";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var tag = Tag.Create(tagName);
            var albumTag = song.AddTag(tag);

            Assert.AreEqual(song.Tags.Count, 1);
            Assert.AreEqual(albumTag.SongId, song.Id);
            Assert.AreEqual(albumTag.TagId, tag.Id);
            Assert.AreEqual(albumTag.Tag.Name, tagName);
        }
    }
}