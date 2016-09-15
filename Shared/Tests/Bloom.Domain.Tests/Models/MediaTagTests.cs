using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests the media tag model classes.
    /// </summary>
    [TestFixture]
    public class MediaTagTests
    {
        private const string ArtistName = "Test Artist";
        private const string SongName = "Test Song";
        private const string AlbumName = "Test Album";

        /// <summary>
        /// Tests the media tag create method.
        /// </summary>
        [Test]
        public void CreateMediaTagFromSongTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var mediaTag = MediaTag.Create(song);

            Assert.AreEqual(mediaTag.ArtistName, ArtistName);
            Assert.AreEqual(mediaTag.Title, SongName);
        }

        /// <summary>
        /// Tests the media tag create method.
        /// </summary>
        [Test]
        public void CreateMediaTagFromTrackTest()
        {
            var album = Album.Create(AlbumName);
            var artist = Artist.Create("Artist");
            var song = Song.Create("Song", artist);
            var albumTrack = AlbumTrack.Create(album, song, 3);
            var mediaTag = MediaTag.Create(albumTrack, album);

            Assert.AreEqual(mediaTag.ArtistName, "Artist");
            Assert.AreEqual(mediaTag.Title, "Song");
            Assert.AreEqual(mediaTag.AlbumName, AlbumName);
            Assert.AreEqual(mediaTag.TrackNumber, 3);
        }

        /// <summary>
        /// Tests the media tag properties.
        /// </summary>
        [Test]
        public void MediaTagPropertiesTest()
        {
            var mediaTag = new MediaTag
            {
                Title = "Title",
                AlbumName = "Album",
                ArtistName = "Artist",
                AlbumArtist = "Album Artist",
                TrackNumber = 5,
                TrackCount = 10,
                DiscNumber = 2,
                DiscCount = 4,
                Comments = "Comments",
                Composers = "Composers",
                Grouping = "Grouping",
                GenreName = "Genre",
                Bpm = 33.3,
                Year = 1999,
                PlayCount = 14,
                Rating = 3,
                MixedArtistAlbum = false
            };

            Assert.AreEqual(mediaTag.Title, "Title");
            Assert.AreEqual(mediaTag.AlbumName, "Album");
            Assert.AreEqual(mediaTag.ArtistName, "Artist");
            Assert.AreEqual(mediaTag.TrackNumber, 5);
            Assert.AreEqual(mediaTag.TrackCount, 10);
            Assert.AreEqual(mediaTag.DiscNumber, 2);
            Assert.AreEqual(mediaTag.DiscCount, 4);
            Assert.AreEqual(mediaTag.Comments, "Comments");
            Assert.AreEqual(mediaTag.Composers, "Composers");
            Assert.AreEqual(mediaTag.Grouping, "Grouping");
            Assert.AreEqual(mediaTag.GenreName, "Genre");
            Assert.AreEqual(mediaTag.Bpm, 33.3);
            Assert.AreEqual(mediaTag.Year, 1999);
            Assert.AreEqual(mediaTag.PlayCount, 14);
            Assert.AreEqual(mediaTag.Rating, 3);
            Assert.IsFalse(mediaTag.MixedArtistAlbum != null && mediaTag.MixedArtistAlbum.Value);
        }
    }
}
