using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the song model classes.
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

        [Test]
        public void SongPropertiesTest()
        {
            var id = Guid.NewGuid();
            var artistId = Guid.NewGuid();

            var song = new Song
            {
                Id = id,
                Name = SongName,
                ArtistId = artistId,
                Version = "Song Version",
                Description = "Song description",
                Lyrics = "Song lyrics",
                Length = 321
            };

            Assert.AreEqual(song.Id, id);
            Assert.AreEqual(song.Name, SongName);
            Assert.AreEqual(song.Version, "Song Version");
            Assert.AreEqual(song.ArtistId, artistId);
            Assert.AreEqual(song.Description, "Song description");
            Assert.AreEqual(song.Lyrics, "Song lyrics");
            Assert.AreEqual(song.Length, 321);
        }

        /// <summary>
        /// Tests the song activity create method.
        /// </summary>
        [Test]
        public void CreateSongActivityTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var activity = Activity.Create("Activity");
            var songActivity = SongActivity.Create(song, activity);

            Assert.AreEqual(songActivity.SongId, song.Id);
            Assert.AreEqual(songActivity.ActivityId, activity.Id);
        }

        /// <summary>
        /// Tests the song mood create method.
        /// </summary>
        [Test]
        public void CreateSongMoodTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var mood = Mood.Create("Mood");
            var songMood = SongMood.Create(song, mood);

            Assert.AreEqual(songMood.SongId, song.Id);
            Assert.AreEqual(songMood.MoodId, mood.Id);
        }

        /// <summary>
        /// Tests the song collaborator create method.
        /// </summary>
        [Test]
        public void CreateSongCollaboratorTest()
        {
            var artist = Artist.Create(ArtistName);
            var collaborator = Artist.Create("Collaborator");
            var song = Song.Create(SongName, artist);
            var songCollaborator = SongCollaborator.Create(song, collaborator);

            Assert.AreEqual(songCollaborator.SongId, song.Id);
            Assert.AreEqual(songCollaborator.ArtistId, collaborator.Id);
            Assert.AreEqual(songCollaborator.Artist.Name, "Collaborator");
        }

        /// <summary>
        /// Tests the song credit create method.
        /// </summary>
        [Test]
        public void CreateSongCreditTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var person = Person.Create("Person");
            var songCredit = SongCredit.Create(song, person);

            Assert.AreEqual(songCredit.SongId, song.Id);
            Assert.AreEqual(songCredit.PersonId, person.Id);
            Assert.AreEqual(songCredit.Person.Name, "Person");
        }

        /// <summary>
        /// Tests the song credit role create method.
        /// </summary>
        [Test]
        public void CreateSongCreditRoleTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var person = Person.Create("Person");
            var role = Role.Create("Role");
            var songCredit = SongCredit.Create(song, person);
            var songCreditRole = SongCreditRole.Create(songCredit, role);

            Assert.AreEqual(songCreditRole.SongCreditId, songCredit.Id);
            Assert.AreEqual(songCreditRole.RoleId, role.Id);
        }

        /// <summary>
        /// Tests the song reference create method.
        /// </summary>
        [Test]
        public void CreateSongReferenceTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var reference = Reference.Create("Reference Title", "http://www.test.com");
            var songReference = SongReference.Create(song, reference);

            Assert.AreEqual(songReference.SongId, song.Id);
            Assert.AreEqual(songReference.ReferenceId, reference.Id);
        }

        /// <summary>
        /// Tests the song review create method.
        /// </summary>
        [Test]
        public void CreateSongReviewTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var review = Review.Create("http://www.test.com/review-article");
            var songReview = SongReview.Create(song, review);

            Assert.AreEqual(songReview.SongId, song.Id);
            Assert.AreEqual(songReview.ReviewId, review.Id);
        }

        /// <summary>
        /// Tests the song segment create method.
        /// </summary>
        [Test]
        public void CreateSongSegmentTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var segment = SongSegment.Create(song, 333, 444);

            Assert.AreEqual(segment.SongId, song.Id);
            Assert.AreEqual(segment.StartTime, 333);
            Assert.AreEqual(segment.StopTime, 444);
        }

        /// <summary>
        /// Tests the song tag create method.
        /// </summary>
        [Test]
        public void CreateSongTagTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var tag = Tag.Create("Tag");
            var songTag = SongTag.Create(song, tag);

            Assert.AreEqual(songTag.SongId, song.Id);
            Assert.AreEqual(songTag.TagId, tag.Id);
        }

        /// <summary>
        /// Tests the create time signature method.
        /// </summary>
        [Test]
        public void CreateTimeSignatureTest()
        {
            var timeSignature = TimeSignature.Create(4, BeatLength.Quarter);
            Assert.AreEqual(timeSignature.BeatsPerMeasure, 4);
            Assert.AreEqual(timeSignature.BeatLength, BeatLength.Quarter);
        }

        /// <summary>
        /// Tests the time signature properties.
        /// </summary>
        [Test]
        public void TimeSignaturePropertiesTest()
        {
            var timeSignature = new TimeSignature
            {
                BeatsPerMeasure = 6,
                BeatLength = BeatLength.Eighth
            };
            Assert.AreEqual(timeSignature.BeatsPerMeasure, 6);
            Assert.AreEqual(timeSignature.BeatLength, BeatLength.Eighth);
            Assert.AreEqual(timeSignature.ToString(), "6/8");
        }
    }
}