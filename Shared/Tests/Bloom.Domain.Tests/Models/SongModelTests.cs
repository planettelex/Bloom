using System;
using System.Collections.Generic;
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
            var originalSongId = Guid.NewGuid();
            var artist = Artist.Create("Artist");
            var genre = Genre.Create("Genre");
            var timeSignature = TimeSignature.Create(4, BeatLength.Quarter);
            var holiday = Holiday.Create("Holiday");
            var now = DateTime.Now;

            var song = new Song
            {
                Id = id,
                Name = SongName,
                Artist = artist,
                Version = "Song Version",
                Description = "Song description",
                Genre = genre,
                Lyrics = "Song lyrics",
                Notes = "Song notes",
                Length = 321,
                Bpm = 95.6,
                Key = MusicalKeys.A,
                TimeSignature = timeSignature,
                AboutDayOfWeek = DayOfWeek.Monday,
                AboutTimeOfYear = TimeOfYear.Spring,
                Holiday = holiday,
                IsLive = false,
                IsCover = false,
                HasExplicitContent = true,
                IsRemix = true,
                OriginalSongId = originalSongId,
                AddedOn = now,
                BestPlayedAtStart = 14000,
                BestPlayedAtStop = 99000,
                Rating = 4,
                LastPlayed = now,
                PlayCount = 1,
                SkipCount = 2,
                RemoveCount = 3
            };

            Assert.AreEqual(song.Id, id);
            Assert.AreEqual(song.Name, SongName);
            Assert.AreEqual(song.Version, "Song Version");
            Assert.AreEqual(song.ArtistId, artist.Id);
            Assert.NotNull(song.Artist);
            Assert.AreEqual(song.Description, "Song description");
            Assert.AreEqual(song.GenreId, genre.Id);
            Assert.NotNull(song.Genre);
            Assert.AreEqual(song.Lyrics, "Song lyrics");
            Assert.AreEqual(song.Notes, "Song notes");
            Assert.AreEqual(song.Length, 321);
            Assert.AreEqual(song.Bpm, 95.6);
            Assert.AreEqual(song.Key, MusicalKeys.A);
            Assert.AreEqual(song.TimeSignatureId, timeSignature.Id);
            Assert.NotNull(song.TimeSignature);
            Assert.AreEqual(song.AboutDayOfWeek, DayOfWeek.Monday);
            Assert.AreEqual(song.AboutTimeOfYear, TimeOfYear.Spring);
            Assert.IsTrue(song.IsHoliday);
            Assert.AreEqual(song.HolidayId, holiday.Id);
            Assert.NotNull(song.Holiday);
            Assert.IsFalse(song.IsLive);
            Assert.IsFalse(song.IsCover);
            Assert.IsTrue(song.HasExplicitContent);
            Assert.IsTrue(song.IsRemix);
            Assert.AreEqual(song.OriginalSongId, originalSongId);
            Assert.AreEqual(song.AddedOn, now);
            Assert.AreEqual(song.LastPlayed, now);
            Assert.AreEqual(song.BestPlayedAtStart, 14000);
            Assert.AreEqual(song.BestPlayedAtStop, 99000);
            Assert.AreEqual(song.Rating, 4);
            Assert.GreaterOrEqual(song.RatedOn, now);
            Assert.AreEqual(song.PlayCount, 1);
            Assert.AreEqual(song.SkipCount, 2);
            Assert.AreEqual(song.RemoveCount, 3);
        }

        /// <summary>
        /// Tests the song to string method.
        /// </summary>
        [Test]
        public void SongToStringTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);

            Assert.AreEqual(song.ToString(), "Test Artist: Test Song");
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
            var song = Song.Create(SongName, artist);
            var songCollaborator = SongCollaborator.Create(song, artist);

            Assert.AreEqual(songCollaborator.SongId, song.Id);
            Assert.AreEqual(songCollaborator.ArtistId, artist.Id);
            Assert.AreEqual(songCollaborator.Artist.Name, "Test Artist");
        }

        /// <summary>
        /// Tests the song collaborator properties.
        /// </summary>
        [Test]
        public void SongCollaboratorPropertiesTest()
        {
            var songId = Guid.NewGuid();
            var artist = Artist.Create(ArtistName);

            var songCollaborator = new SongCollaborator
            {
                SongId = songId,
                Artist = artist,
                IsFeatured = true
            };

            Assert.AreEqual(songCollaborator.SongId, songId);
            Assert.AreEqual(songCollaborator.ArtistId, artist.Id);
            Assert.NotNull(songCollaborator.Artist);
            Assert.IsTrue(songCollaborator.IsFeatured);
        }

        /// <summary>
        /// Tests the song collaborator to string method.
        /// </summary>
        [Test]
        public void SongCollaboratorToStringTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var songCollaborator = SongCollaborator.Create(song, artist);
            songCollaborator.IsFeatured = true;

            Assert.AreEqual(songCollaborator.ToString(), "Test Artist (Featured)");
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
        /// Tests the song credit properties.
        /// </summary>
        [Test]
        public void SongCreditPropertiesTest()
        {
            var songId = Guid.NewGuid();
            var person = Person.Create("Person");
            var roles = new List<Role>
            {
                Role.Create("Role 1"),
                Role.Create("Role 2")
            };

            var songCredit = new SongCredit
            {
                SongId = songId,
                Person = person,
                Roles = roles
            };

            Assert.AreEqual(songCredit.SongId, songId);
            Assert.AreEqual(songCredit.PersonId, person.Id);
            Assert.NotNull(songCredit.Person);
            Assert.NotNull(songCredit.Roles);
            Assert.AreEqual(2, songCredit.Roles.Count);
        }

        /// <summary>
        /// Tests the song credit to string method.
        /// </summary>
        [Test]
        public void AlbumCreditToStringTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var person = Person.Create("Person");
            var songCredit = SongCredit.Create(song, person);

            Assert.AreEqual(songCredit.ToString(), "Person");
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
            var source = Source.Create("Source");
            var review = Review.Create(source, "http://www.test.com/review-article");
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
        /// Test the song segment properties.
        /// </summary>
        [Test]
        public void SongSegmentPropertiesTest()
        {
            var id = Guid.NewGuid();
            var songId = Guid.NewGuid();
            var timeSignature = TimeSignature.Create(2, BeatLength.Quarter);
            var segment = new SongSegment
            {
                Id = id,
                SongId = songId,
                Name = "Segment Name",
                Bpm = 123,
                Key = MusicalKeys.B,
                TimeSignature = timeSignature,
                StartTime = 02,
                StopTime = 999
            };

            Assert.AreEqual(segment.Id, id);
            Assert.AreEqual(segment.SongId, songId);
            Assert.AreEqual(segment.Name, "Segment Name");
            Assert.AreEqual(segment.Bpm, 123);
            Assert.AreEqual(segment.Key, MusicalKeys.B);
            Assert.AreEqual(segment.TimeSignatureId, timeSignature.Id);
            Assert.NotNull(segment.TimeSignature);
            Assert.AreEqual(segment.StartTime, 02);
            Assert.AreEqual(segment.StopTime, 999);
        }

        /// <summary>
        /// Tests the song segment to string method.
        /// </summary>
        [Test]
        public void SongSegmentToStringTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var segment1 = SongSegment.Create(song, 333, 444);
            segment1.Name = "Segment Name";
            var segment2 = SongSegment.Create(song, 555, 666);

            Assert.AreEqual(segment1.ToString(), "Segment Name");
            Assert.AreEqual(segment2.ToString(), segment2.Id.ToString());
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

        /// <summary>
        /// Tests the create recording session method.
        /// </summary>
        [Test]
        public void CreateRecordingSessionTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var recordingSession = RecordingSession.Create(song, DateTime.Parse("3/4/1956"));
            Assert.AreNotEqual(recordingSession.Id, Guid.Empty);
            Assert.AreEqual(recordingSession.SongId, song.Id);
            Assert.AreEqual(recordingSession.OccurredOn, DateTime.Parse("3/4/1956"));
        }

        /// <summary>
        /// Tests the recording session to string test.
        /// </summary>
        [Test]
        public void RecordingSessionToStringTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var recordingSession = RecordingSession.Create(song, DateTime.Parse("3/4/1956"));
            Assert.AreEqual(recordingSession.ToString(), DateTime.Parse("3/4/1956").ToShortDateString());
        }

        /// <summary>
        /// Tests the song media create methods.
        /// </summary>
        [Test]
        public void CreateSongMediaTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var songMedia1 = SongMedia.Create(song, MediaTypes.Vinyl);
            var songMedia2 = SongMedia.Create(song, DigitalFormats.MP3, "c:\\song.mp3");

            Assert.AreNotEqual(songMedia1.Id, Guid.Empty);
            Assert.AreEqual(songMedia1.SongId, song.Id);
            Assert.AreEqual(songMedia1.MediaType, MediaTypes.Vinyl);
            Assert.AreNotEqual(songMedia2.Id, Guid.Empty);
            Assert.AreEqual(songMedia2.MediaType, MediaTypes.Digital);
            Assert.AreEqual(songMedia2.DigitalFormat, DigitalFormats.MP3);
        }

        /// <summary>
        /// Tests the song media properties.
        /// </summary>
        [Test]
        public void SongMediaPropertiesTest()
        {
            var id = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);

            var songMedia = new SongMedia
            {
                Id = id,
                SongId = song.Id,
                MediaType = MediaTypes.Digital,
                DigitalFormat = DigitalFormats.FLAC,
                FilePath = "c:\\song.mp3",
                IsCompressed = true,
                IsDamaged = false,
                IsProtected = false,
                FileSize = 12345,
                SampleRate = 44000,
                BitRate = 128,
                VolumeOffset = 3,
                ReceivedFromPersonId = personId
            };

            Assert.AreEqual(id, songMedia.Id);
            Assert.AreEqual(song.Id, songMedia.SongId);
            Assert.AreEqual(MediaTypes.Digital, songMedia.MediaType);
            Assert.AreEqual(DigitalFormats.FLAC, songMedia.DigitalFormat);
            Assert.AreEqual("c:\\song.mp3", songMedia.FilePath);
            Assert.AreEqual(12345, songMedia.FileSize);
            Assert.AreEqual(44000, songMedia.SampleRate);
            Assert.AreEqual(128, songMedia.BitRate);
            Assert.AreEqual(3, songMedia.VolumeOffset);
            Assert.AreEqual(personId, songMedia.ReceivedFromPersonId);
        }

        /// <summary>
        /// Tests the song media to string method.
        /// </summary>
        [Test]
        public void SongMediaToStringTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var songMedia1 = SongMedia.Create(song, MediaTypes.CD);
            var songMedia2 = SongMedia.Create(song, DigitalFormats.MP3, "c:\\song.mp3");

            Assert.AreEqual(songMedia1.ToString(), "CD");
            Assert.AreEqual(songMedia2.ToString(), "c:\\song.mp3");
        }
    }
}