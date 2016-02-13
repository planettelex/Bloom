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