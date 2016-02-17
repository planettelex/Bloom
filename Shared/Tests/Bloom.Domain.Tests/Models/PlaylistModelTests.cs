using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests the playlist model classes.
    /// </summary>
    [TestFixture]
    public class PlaylistModelTests
    {
        private const string PlaylistName = "Test Playlist";

        /// <summary>
        /// Tests the playlist create method.
        /// </summary>
        [Test]
        public void CreatePlaylistTest()
        {
            var owner = Person.Create("Person");
            var playlist = Playlist.Create(PlaylistName, owner);

            Assert.AreNotEqual(playlist.Id, Guid.Empty);
            Assert.AreEqual(playlist.Name, PlaylistName);
            Assert.AreEqual(playlist.CreatedById, owner.Id);
            Assert.AreEqual(playlist.CreatedBy.Name, "Person");
        }

        /// <summary>
        /// Tests the playlist properties.
        /// </summary>
        [Test]
        public void PlaylistPropertiesTest()
        {
            var id = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var now = DateTime.Now;

            var playlist = new Playlist
            {
                Id = id,
                Name = PlaylistName,
                Description = "Playlist description",
                Length = 13579,
                CreatedById = personId,
                CreatedOn = now
            };

            Assert.AreEqual(playlist.Id, id);
            Assert.AreEqual(playlist.Name, PlaylistName);
            Assert.AreEqual(playlist.Description, "Playlist description");
            Assert.AreEqual(playlist.Length, 13579);
            Assert.AreEqual(playlist.CreatedById, personId);
            Assert.AreEqual(playlist.CreatedOn, now);
        }

        /// <summary>
        /// Tests the playlist activity create method.
        /// </summary>
        [Test]
        public void CreatePlaylistActivityTest()
        {
            var owner = Person.Create("Person");
            var playlist = Playlist.Create(PlaylistName, owner);
            var activity = Activity.Create("Activity");
            var playlistActivity = PlaylistActivity.Create(playlist, activity);

            Assert.AreEqual(playlistActivity.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistActivity.ActivityId, activity.Id);
        }

        /// <summary>
        /// Tests the playlist mood create method.
        /// </summary>
        [Test]
        public void CreatePlaylistMoodTest()
        {
            var owner = Person.Create("Person");
            var playlist = Playlist.Create(PlaylistName, owner);
            var mood = Mood.Create("Mood");
            var playlistMood = PlaylistMood.Create(playlist, mood);

            Assert.AreEqual(playlistMood.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistMood.MoodId, mood.Id);
        }

        /// <summary>
        /// Tests the playlist artwork create method.
        /// </summary>
        [Test]
        public void CreatePlaylistArtworkTest()
        {
            var owner = Person.Create("Person");
            var playlist = Playlist.Create(PlaylistName, owner);
            var playlistArtwork = PlaylistArtwork.Create(playlist, "c:\\Music\\Image.jpg", 3);

            Assert.AreEqual(playlistArtwork.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistArtwork.FilePath, "c:\\Music\\Image.jpg");
            Assert.AreEqual(playlistArtwork.Priority, 3);
        }

        /// <summary>
        /// Tests the playlist reference create method.
        /// </summary>
        [Test]
        public void CreatePlaylistReferenceTest()
        {
            var owner = Person.Create("Person");
            var playlist = Playlist.Create(PlaylistName, owner);
            var reference = Reference.Create("Reference Title", "http://www.test.com");
            var playlistReference = PlaylistReference.Create(playlist, reference);

            Assert.AreEqual(playlistReference.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistReference.ReferenceId, reference.Id);
        }

        /// <summary>
        /// Tests the playlist tag create method.
        /// </summary>
        [Test]
        public void CreatePlaylistTagTest()
        {
            var owner = Person.Create("Person");
            var playlist = Playlist.Create(PlaylistName, owner);
            var tag = Tag.Create("Tag");
            var playlistTag = PlaylistTag.Create(playlist, tag);

            Assert.AreEqual(playlistTag.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistTag.TagId, tag.Id);
        }

        /// <summary>
        /// Tests the playlist track create methods.
        /// </summary>
        [Test]
        public void CreatePlaylistTrackTest()
        {
            var owner = Person.Create("Person");
            var playlist = Playlist.Create(PlaylistName, owner);
            var artist = Artist.Create("Artist");
            var song = Song.Create("Song", artist);
            var playlistTrack = PlaylistTrack.Create(playlist, song, 6);

            Assert.AreEqual(playlistTrack.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistTrack.SongId, song.Id);
            Assert.AreEqual(playlistTrack.Song.Name, song.Name);
            Assert.AreEqual(playlistTrack.TrackNumber, 6);
        }
    }
}