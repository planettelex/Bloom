using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the photo model class.
    /// </summary>
    [TestFixture]
    public class PlaylistModelTests
    {
        private const string LibraryOwnerName = "Owner Name";
        private const string PlaylistName = "Test Playlist";

        /// <summary>
        /// Tests the playlist create method.
        /// </summary>
        [Test]
        public void CreatePlaylistTest()
        {
            var owner = Person.Create(LibraryOwnerName);
            var playlist = Playlist.Create(PlaylistName, owner);

            Assert.AreNotEqual(playlist.Id, Guid.Empty);
            Assert.AreEqual(playlist.Name, PlaylistName);
            Assert.AreEqual(playlist.CreatedById, owner.Id);
            Assert.AreEqual(playlist.CreatedBy.Name, owner.Name);
        }

        /// <summary>
        /// Tests adding an activity to a playlist.
        /// </summary>
        [Test]
        public void AddActivityToPlaylistTest()
        {
            const string activityName = "Test Activity";
            var activity = Activity.Create(activityName);
            var owner = Person.Create(LibraryOwnerName);
            var playlist = Playlist.Create(PlaylistName, owner);
            var playlistActivity = playlist.AddActivity(activity);

            Assert.AreEqual(playlist.Activities.Count, 1);
            Assert.AreEqual(playlistActivity.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistActivity.Playlist.Name, PlaylistName);
            Assert.AreEqual(playlistActivity.ActivityId, activity.Id);
            Assert.AreEqual(playlistActivity.Activity.Name, activityName);
        }

        /// <summary>
        /// Tests adding artwork to a playlist.
        /// </summary>
        [Test]
        public void AddArtworkToPlaylistTest()
        {
            const string url1 = "http://www.test.com/image1.jpg";
            const string url2 = "http://www.test.com/image2.jpg";
            var owner = Person.Create(LibraryOwnerName);
            var playlist = Playlist.Create(PlaylistName, owner);
            var playlistArtwork1 = playlist.AddArtwork(url1);
            var playlistArtwork2 = playlist.AddArtwork(url2);

            Assert.AreEqual(playlist.Artwork.Count, 2);
            Assert.AreEqual(playlistArtwork1.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistArtwork1.Url, url1);
            Assert.AreEqual(playlistArtwork1.Priority, 1);
            Assert.AreEqual(playlistArtwork2.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistArtwork2.Url, url2);
            Assert.AreEqual(playlistArtwork2.Priority, 2);
        }

        /// <summary>
        /// Tests adding a mood to a playlist.
        /// </summary>
        [Test]
        public void AddMoodToPlaylistTest()
        {
            const string moodName = "Test Mood";
            var mood = Mood.Create(moodName);
            var owner = Person.Create(LibraryOwnerName);
            var playlist = Playlist.Create(PlaylistName, owner);
            var playlistMood = playlist.AddMood(mood);

            Assert.AreEqual(playlist.Moods.Count, 1);
            Assert.AreEqual(playlistMood.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistMood.Playlist.Name, PlaylistName);
            Assert.AreEqual(playlistMood.MoodId, mood.Id);
            Assert.AreEqual(playlistMood.Mood.Name, moodName);
        }

        /// <summary>
        /// Tests adding a reference to a playlist.
        /// </summary>
        [Test]
        public void AddReferenceToPlaylistTest()
        {
            const string referenceName = "Test Reference";
            const string referenceUrl = "http://www.test.com/";
            var owner = Person.Create(LibraryOwnerName);
            var playlist = Playlist.Create(PlaylistName, owner);
            var reference = Reference.Create(referenceName, referenceUrl);
            var playlistReference = playlist.AddReference(reference);

            Assert.AreEqual(playlist.References.Count, 1);
            Assert.AreEqual(playlistReference.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistReference.ReferenceId, reference.Id);
            Assert.AreEqual(playlistReference.Reference.Name, referenceName);
            Assert.AreEqual(playlistReference.Reference.Url, referenceUrl);
        }

        /// <summary>
        /// Tests adding an tag to a playlist.
        /// </summary>
        [Test]
        public void AddTagToPlaylistTest()
        {
            const string tagName = "Test Tag";
            var owner = Person.Create(LibraryOwnerName);
            var playlist = Playlist.Create(PlaylistName, owner);
            var tag = Tag.Create(tagName);
            var playlistTag = playlist.AddTag(tag);

            Assert.AreEqual(playlist.Tags.Count, 1);
            Assert.AreEqual(playlistTag.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistTag.Playlist.Name, PlaylistName);
            Assert.AreEqual(playlistTag.TagId, tag.Id);
            Assert.AreEqual(playlistTag.Tag.Name, tagName);
        }

        /// <summary>
        /// Tests adding a track to a playlist.
        /// </summary>
        [Test]
        public void AddTrackToPlaylistTest()
        {
            const string song1Name = "Test Song 1";
            const string song2Name = "Test Song 2";
            const string song3Name = "Test Song 3";
            const string artistName = "Artist Name";
            var artist = Artist.Create(artistName);
            var owner = Person.Create(LibraryOwnerName);
            var playlist = Playlist.Create(PlaylistName, owner);
            var song1 = Song.Create(song1Name, artist);
            var song2 = Song.Create(song2Name, artist);
            var song3 = Song.Create(song3Name, artist);
            var playlistTrack1 = playlist.AddTrack(song1, 1);
            var playlistTrack2 = playlist.AddTrack(song2, 2);
            var playlistTrack3 = playlist.AddTrack(song3, 3);

            Assert.AreEqual(playlist.Tracks.Count, 3);
            Assert.AreEqual(playlistTrack1.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistTrack1.SongId, song1.Id);
            Assert.AreEqual(playlistTrack1.Song.Name, song1.Name);
            Assert.AreEqual(playlistTrack1.TrackNumber, 1);
            Assert.AreEqual(playlistTrack2.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistTrack2.SongId, song2.Id);
            Assert.AreEqual(playlistTrack2.Song.Name, song2.Name);
            Assert.AreEqual(playlistTrack2.TrackNumber, 2);
            Assert.AreEqual(playlistTrack3.PlaylistId, playlist.Id);
            Assert.AreEqual(playlistTrack3.SongId, song3.Id);
            Assert.AreEqual(playlistTrack3.Song.Name, song3.Name);
            Assert.AreEqual(playlistTrack3.TrackNumber, 3);
        }
    }
}