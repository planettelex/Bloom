using System.IO;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Services.Tests
{
    /// <summary>
    /// Tests the media tag service class.
    /// </summary>
    [TestFixture]
    public class MediaTagServiceTests
    {
        private IMediaTagService _mediaTagService;
        private string _testFolder;
        private const string MediaFolder = "..\\..\\TestMedia\\";
        // Test mp3 files
        private const string SomethingMp3 = "Something.mp3";
        private const string JungleBoogieMp3 = "Jungle Boogie.mp3";
        private const string BloomMp3 = "Bloom.mp3";
        private const string TestTagMp3 = "TestTag.mp3";
        private const string TestImageMp3 = "TestImage.mp3";
        // Test flac files
        private const string GuessAgainFlac = "Guess Again!.flac";
        private const string TestFlac = "Test.flac";
        // Test aac files
        private const string DiktatorOfTheFreeWorldAac = "Diktator of the Free World.m4a";
        private const string HandsInTheRainAac = "Hands in the Rain.m4a";
        private const string TestTagAac = "TestTag.m4a";
        private const string TestImageAac = "TestImage.m4a";
        
        /// <summary>
        /// Sets up the tests.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _mediaTagService = new MediaTagService();
            _testFolder = Data.Settings.TestsDataPath;
            if (!Directory.Exists(_testFolder))
                Directory.CreateDirectory(_testFolder);

            // Copy Jungle Boogie for writing mp3 tag tests
            var testMp3TagFilePath = Path.Combine(_testFolder, TestTagMp3);
            File.Copy(MediaFolder + JungleBoogieMp3, testMp3TagFilePath, true);

            // Copy Bloom for writing mp3 image tests
            var testMp3ImageFilePath = Path.Combine(_testFolder, TestImageMp3);
            File.Copy(MediaFolder + BloomMp3, testMp3ImageFilePath, true);

            // Copy Guess Again! for writing flac tests
            var testFlacTagFilePath = Path.Combine(_testFolder, TestFlac);
            File.Copy(MediaFolder + GuessAgainFlac, testFlacTagFilePath, true);

            // Copy Diktator of the Free World for writing aac tag tests
            var testAacTagFilePath = Path.Combine(_testFolder, TestTagAac);
            File.Copy(MediaFolder + DiktatorOfTheFreeWorldAac, testAacTagFilePath, true);

            // Copy Hands in the Rain for writing aac image tests
            var testAacImageFilePath = Path.Combine(_testFolder, TestImageAac);
            File.Copy(MediaFolder + HandsInTheRainAac, testAacImageFilePath, true);
        }

        /// <summary>
        /// Tests reading mp3 tags.
        /// </summary>
        [Test]
        public void ReadMp3TagTests()
        {
            var mediaTag = _mediaTagService.ReadMediaTag(MediaFolder + SomethingMp3);
            Assert.IsNotNull(mediaTag);
            Assert.AreEqual("Something", mediaTag.Title);
            Assert.AreEqual("Beatles, The", mediaTag.ArtistName);
            Assert.AreEqual("Beatles, The", mediaTag.AlbumArtist);
            Assert.AreEqual("Abbey Road", mediaTag.AlbumName);
            Assert.AreEqual("Classic Rock", mediaTag.GenreName);
            Assert.AreEqual("George Harrison", mediaTag.Composers);
            Assert.IsNull(mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(1969, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(1, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(1, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(2, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(17, mediaTag.TrackCount);
            Assert.IsNotNull(mediaTag.Comments);
            Assert.Greater(mediaTag.Comments.Length, 0);
            Assert.AreEqual(65.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);

            mediaTag = _mediaTagService.ReadMediaTag(MediaFolder + JungleBoogieMp3);
            Assert.IsNotNull(mediaTag);
            Assert.AreEqual("Jungle Boogie", mediaTag.Title);
            Assert.AreEqual("Kool & the Gang", mediaTag.ArtistName);
            Assert.IsNull(mediaTag.AlbumArtist);
            Assert.AreEqual("Pulp Fiction Soundtrack", mediaTag.AlbumName);
            Assert.AreEqual("Classic Rock", mediaTag.GenreName);
            Assert.AreEqual("Ronald Bell", mediaTag.Composers);
            Assert.AreEqual("Soundtrack", mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(1994, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(1, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(1, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(3, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(16, mediaTag.TrackCount);
            Assert.IsNotNull(mediaTag.Comments);
            Assert.Greater(mediaTag.Comments.Length, 0);
            Assert.AreEqual(106.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);

            mediaTag = _mediaTagService.ReadMediaTag(MediaFolder + BloomMp3);
            Assert.IsNotNull(mediaTag);
            Assert.AreEqual("Bloom", mediaTag.Title);
            Assert.AreEqual("Radiohead", mediaTag.ArtistName);
            Assert.AreEqual("Radiohead", mediaTag.AlbumArtist);
            Assert.AreEqual("The King of Limbs", mediaTag.AlbumName);
            Assert.AreEqual("Alternative", mediaTag.GenreName);
            Assert.AreEqual("Thom Yorke, Jonny Greenwood, Colin Greenwood, Ed O'Brien, Phil Selway", mediaTag.Composers);
            Assert.IsNull(mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(2011, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(1, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(1, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(1, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(8, mediaTag.TrackCount);
            Assert.IsNotNull(mediaTag.Comments);
            Assert.Greater(mediaTag.Comments.Length, 0);
            Assert.AreEqual(113.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);
        }

        /// <summary>
        /// Tests reading flac tags.
        /// </summary>
        [Test]
        public void ReadFlacTagTest()
        {
            var mediaTag = _mediaTagService.ReadMediaTag(MediaFolder + GuessAgainFlac);
            Assert.NotNull(mediaTag);
            Assert.AreEqual("Guess Again!", mediaTag.Title);
            Assert.AreEqual("Thom Yorke", mediaTag.ArtistName);
            Assert.IsNull(mediaTag.AlbumArtist);
            Assert.AreEqual("Tomorrow's Modern Boxes", mediaTag.AlbumName);
            Assert.AreEqual("Electronic", mediaTag.GenreName);
            Assert.IsNull(mediaTag.Composers);
            Assert.IsNull(mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(2014, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(1, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(1, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(2, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(8, mediaTag.TrackCount);
            Assert.IsNull(mediaTag.Comments);
            Assert.AreEqual(0.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);
        }

        /// <summary>
        /// Tests reading aac tags.
        /// </summary>
        [Test]
        public void ReadAacTagTest()
        {
            var mediaTag = _mediaTagService.ReadMediaTag(MediaFolder + DiktatorOfTheFreeWorldAac);
            Assert.NotNull(mediaTag);
            Assert.AreEqual("Diktator of the Free World", mediaTag.Title);
            Assert.AreEqual("Kula Shaker", mediaTag.ArtistName);
            Assert.AreEqual("Kula Shaker", mediaTag.AlbumArtist);
            Assert.AreEqual("Revenge of the King", mediaTag.AlbumName);
            Assert.AreEqual("Rock", mediaTag.GenreName);
            Assert.AreEqual("Crispian Mills", mediaTag.Composers);
            Assert.AreEqual("EP", mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(2006, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(1, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(1, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(2, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(5, mediaTag.TrackCount);
            Assert.IsNotNull(mediaTag.Comments);
            Assert.Greater(mediaTag.Comments.Length, 0);
            Assert.AreEqual(0.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);
        }

        /// <summary>
        /// Tests reading images embedded in mp3 files.
        /// </summary>
        [Test]
        public void ReadMp3ImageTests()
        {
            var mediaImage = _mediaTagService.ReadMediaImage(MediaFolder + SomethingMp3);
            Assert.IsNotNull(mediaImage);
            Assert.GreaterOrEqual(mediaImage.VerticalResolution, 72.0);
            Assert.GreaterOrEqual(mediaImage.HorizontalResolution, 72.0);
            Assert.GreaterOrEqual(mediaImage.Height, 300.0);
            Assert.GreaterOrEqual(mediaImage.Width, 300.0);

            mediaImage = _mediaTagService.ReadMediaImage(MediaFolder + BloomMp3);
            Assert.IsNotNull(mediaImage);
            Assert.GreaterOrEqual(mediaImage.VerticalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.HorizontalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.Height, 300.0);
            Assert.GreaterOrEqual(mediaImage.Width, 300.0);
        }

        /// <summary>
        /// Tests reading images embedded in flac files.
        /// </summary>
        [Test]
        public void ReadFlacImageTest()
        {
            var mediaImage = _mediaTagService.ReadMediaImage(MediaFolder + GuessAgainFlac);
            Assert.IsNotNull(mediaImage);
            Assert.GreaterOrEqual(mediaImage.VerticalResolution, 72.0);
            Assert.GreaterOrEqual(mediaImage.HorizontalResolution, 72.0);
            Assert.GreaterOrEqual(mediaImage.Height, 800.0);
            Assert.GreaterOrEqual(mediaImage.Width, 800.0);
        }

        /// <summary>
        /// Tests reading images embedded in aac files.
        /// </summary>
        [Test]
        public void ReadAacImageTest()
        {
            var mediaImage = _mediaTagService.ReadMediaImage(MediaFolder + DiktatorOfTheFreeWorldAac);
            Assert.IsNotNull(mediaImage);
            Assert.GreaterOrEqual(mediaImage.VerticalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.HorizontalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.Height, 600.0);
            Assert.GreaterOrEqual(mediaImage.Width, 600.0);
        }

        /// <summary>
        /// Tests writing mp3 tags.
        /// </summary>
        [Test]
        public void WriteMp3TagTest()
        {
            var testMp3FilePath = Path.Combine(_testFolder, TestTagMp3);

            var mediaTag = _mediaTagService.ReadMediaTag(testMp3FilePath);
            Assert.IsNotNull(mediaTag);
            Assert.AreEqual("Jungle Boogie", mediaTag.Title);
            Assert.AreEqual("Kool & the Gang", mediaTag.ArtistName);
            Assert.IsNull(mediaTag.AlbumArtist);
            Assert.AreEqual("Pulp Fiction Soundtrack", mediaTag.AlbumName);
            Assert.AreEqual("Classic Rock", mediaTag.GenreName);
            Assert.AreEqual("Ronald Bell", mediaTag.Composers);
            Assert.AreEqual("Soundtrack", mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(1994, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(1, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(1, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(3, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(16, mediaTag.TrackCount);
            Assert.IsNotNull(mediaTag.Comments);
            Assert.Greater(mediaTag.Comments.Length, 0);
            Assert.AreEqual(106.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);

            var mediaTagUpdate = new MediaTag
            {
                Title = "Test MP3 Title",
                ArtistName = "Test MP3 Artist",
                AlbumArtist = "Test MP3 Album Artist",
                AlbumName = "Test MP3 Album",
                GenreName = "Test MP3 Genre",
                Grouping = "Test MP3 Grouping",
                Comments = "Test MP3 Comments",
                Composers = "Test MP3 Composers",
                TrackNumber = 10,
                TrackCount = 100,
                DiscNumber = 2,
                DiscCount = 3,
                Bpm = 66.6,
                Year = 2016
            };

            _mediaTagService.WriteMediaTag(testMp3FilePath, mediaTagUpdate);

            mediaTag = _mediaTagService.ReadMediaTag(testMp3FilePath);
            Assert.IsNotNull(mediaTag);
            Assert.AreEqual("Test MP3 Title", mediaTag.Title);
            Assert.AreEqual("Test MP3 Artist", mediaTag.ArtistName);
            Assert.AreEqual("Test MP3 Album Artist", mediaTag.AlbumArtist);
            Assert.AreEqual("Test MP3 Album", mediaTag.AlbumName);
            Assert.AreEqual("Test MP3 Genre", mediaTag.GenreName);
            Assert.AreEqual("Test MP3 Composers", mediaTag.Composers);
            Assert.AreEqual("Test MP3 Grouping", mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(2016, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(2, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(3, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(10, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(100, mediaTag.TrackCount);
            Assert.AreEqual("Test MP3 Comments", mediaTag.Comments);
            Assert.AreEqual(67.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);
        }

        /// <summary>
        /// Tests writing flac tags.
        /// </summary>
        [Test]
        public void WriteFlacTagTest()
        {
            var testFlacFilePath = Path.Combine(_testFolder, TestFlac);

            var mediaTag = _mediaTagService.ReadMediaTag(testFlacFilePath);
            Assert.NotNull(mediaTag);
            Assert.AreEqual("Guess Again!", mediaTag.Title);
            Assert.AreEqual("Thom Yorke", mediaTag.ArtistName);
            Assert.IsNull(mediaTag.AlbumArtist);
            Assert.AreEqual("Tomorrow's Modern Boxes", mediaTag.AlbumName);
            Assert.AreEqual("Electronic", mediaTag.GenreName);
            Assert.IsNull(mediaTag.Composers);
            Assert.IsNull(mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(2014, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(1, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(1, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(2, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(8, mediaTag.TrackCount);
            Assert.IsNull(mediaTag.Comments);
            Assert.AreEqual(0.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);

            var mediaTagUpdate = new MediaTag
            {
                Title = "Test Flac Title",
                ArtistName = "Test Flac Artist",
                AlbumArtist = "Test Flac Album Artist",
                AlbumName = "Test Flac Album",
                GenreName = "Test Flac Genre",
                Grouping = "Test Flac Grouping",
                Comments = "Test Flac Comments",
                Composers = "Test Flac Composers",
                TrackNumber = 10,
                TrackCount = 100,
                DiscNumber = 2,
                DiscCount = 3,
                Bpm = 66.6,
                Year = 2016
            };

            _mediaTagService.WriteMediaTag(testFlacFilePath, mediaTagUpdate);

            mediaTag = _mediaTagService.ReadMediaTag(testFlacFilePath);
            Assert.IsNotNull(mediaTag);
            Assert.AreEqual("Test Flac Title", mediaTag.Title);
            Assert.AreEqual("Test Flac Artist", mediaTag.ArtistName);
            Assert.AreEqual("Test Flac Album Artist", mediaTag.AlbumArtist);
            Assert.AreEqual("Test Flac Album", mediaTag.AlbumName);
            Assert.AreEqual("Test Flac Genre", mediaTag.GenreName);
            Assert.AreEqual("Test Flac Composers", mediaTag.Composers);
            Assert.AreEqual("Test Flac Grouping", mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(2016, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(2, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(3, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(10, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(100, mediaTag.TrackCount);
            Assert.AreEqual("Test Flac Comments", mediaTag.Comments);
            Assert.AreEqual(67.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);
        }

        /// <summary>
        /// Tests writing aac tags.
        /// </summary>
        [Test]
        public void WriteAacTagTest()
        {
            var testFlacFilePath = Path.Combine(_testFolder, TestTagAac);

            var mediaTag = _mediaTagService.ReadMediaTag(testFlacFilePath);
            Assert.NotNull(mediaTag);
            Assert.AreEqual("Diktator of the Free World", mediaTag.Title);
            Assert.AreEqual("Kula Shaker", mediaTag.ArtistName);
            Assert.AreEqual("Kula Shaker", mediaTag.AlbumArtist);
            Assert.AreEqual("Revenge of the King", mediaTag.AlbumName);
            Assert.AreEqual("Rock", mediaTag.GenreName);
            Assert.AreEqual("Crispian Mills", mediaTag.Composers);
            Assert.AreEqual("EP", mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(2006, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(1, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(1, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(2, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(5, mediaTag.TrackCount);
            Assert.IsNotNull(mediaTag.Comments);
            Assert.Greater(mediaTag.Comments.Length, 0);
            Assert.AreEqual(0.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);

            var mediaTagUpdate = new MediaTag
            {
                Title = "Test AAC Title",
                ArtistName = "Test AAC Artist",
                AlbumArtist = "Test AAC Album Artist",
                AlbumName = "Test AAC Album",
                GenreName = "Test AAC Genre",
                Grouping = "Test AAC Grouping",
                Comments = "Test AAC Comments",
                Composers = "Test AAC Composers",
                TrackNumber = 10,
                TrackCount = 100,
                DiscNumber = 2,
                DiscCount = 3,
                Bpm = 66.6,
                Year = 2016
            };

            _mediaTagService.WriteMediaTag(testFlacFilePath, mediaTagUpdate);

            mediaTag = _mediaTagService.ReadMediaTag(testFlacFilePath);
            Assert.IsNotNull(mediaTag);
            Assert.AreEqual("Test AAC Title", mediaTag.Title);
            Assert.AreEqual("Test AAC Artist", mediaTag.ArtistName);
            Assert.AreEqual("Test AAC Album Artist", mediaTag.AlbumArtist);
            Assert.AreEqual("Test AAC Album", mediaTag.AlbumName);
            Assert.AreEqual("Test AAC Genre", mediaTag.GenreName);
            Assert.AreEqual("Test AAC Composers", mediaTag.Composers);
            Assert.AreEqual("Test AAC Grouping", mediaTag.Grouping);
            Assert.IsNull(mediaTag.MixedArtistAlbum);
            Assert.IsNotNull(mediaTag.Year);
            Assert.AreEqual(2016, mediaTag.Year);
            Assert.IsNotNull(mediaTag.DiscNumber);
            Assert.AreEqual(2, mediaTag.DiscNumber);
            Assert.IsNotNull(mediaTag.DiscCount);
            Assert.AreEqual(3, mediaTag.DiscCount);
            Assert.IsNotNull(mediaTag.TrackNumber);
            Assert.AreEqual(10, mediaTag.TrackNumber);
            Assert.IsNotNull(mediaTag.TrackCount);
            Assert.AreEqual(100, mediaTag.TrackCount);
            Assert.AreEqual("Test AAC Comments", mediaTag.Comments);
            Assert.AreEqual(67.0, mediaTag.Bpm);
            Assert.IsNull(mediaTag.Rating);
            Assert.IsNull(mediaTag.PlayCount);
        }

        /// <summary>
        /// Tests writing mp3 embedded images.
        /// </summary>
        [Test]
        public void WriteMp3ImageTest()
        {
            var testImageFilePath = Path.Combine(_testFolder, TestImageMp3);
            var mediaImage = _mediaTagService.ReadMediaImage(testImageFilePath);
            Assert.IsNotNull(mediaImage);
            Assert.GreaterOrEqual(mediaImage.VerticalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.HorizontalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.Height, 300.0);
            Assert.GreaterOrEqual(mediaImage.Width, 300.0);

            var mediaImageUpdate = _mediaTagService.ReadMediaImage(MediaFolder + SomethingMp3);
            _mediaTagService.WriteMediaImage(testImageFilePath, mediaImageUpdate);

            mediaImage = _mediaTagService.ReadMediaImage(testImageFilePath);
            Assert.IsNotNull(mediaImage);
            Assert.LessOrEqual(mediaImage.VerticalResolution, 300.0);
            Assert.LessOrEqual(mediaImage.HorizontalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.Height, 300.0);
            Assert.GreaterOrEqual(mediaImage.Width, 300.0);
        }

        /// <summary>
        /// Tests writing flac embedded images.
        /// </summary>
        [Test]
        public void WriteFlacImageTest()
        {
            var testImageFilePath = Path.Combine(_testFolder, TestFlac);
            var mediaImage = _mediaTagService.ReadMediaImage(testImageFilePath);
            Assert.IsNotNull(mediaImage);
            Assert.GreaterOrEqual(mediaImage.VerticalResolution, 72.0);
            Assert.GreaterOrEqual(mediaImage.HorizontalResolution, 72.0);
            Assert.GreaterOrEqual(mediaImage.Height, 800.0);
            Assert.GreaterOrEqual(mediaImage.Width, 800.0);

            var mediaImageUpdate = _mediaTagService.ReadMediaImage(MediaFolder + SomethingMp3);
            _mediaTagService.WriteMediaImage(testImageFilePath, mediaImageUpdate);

            mediaImage = _mediaTagService.ReadMediaImage(testImageFilePath);
            Assert.IsNotNull(mediaImage);
            Assert.LessOrEqual(mediaImage.VerticalResolution, 300.0);
            Assert.LessOrEqual(mediaImage.HorizontalResolution, 300.0);
            Assert.Less(mediaImage.Height, 800.0);
            Assert.Less(mediaImage.Width, 800.0);
        }

        /// <summary>
        /// Tests writing images embedded in aac files.
        /// </summary>
        [Test]
        public void WriteAacImageTest()
        {
            var testImageFilePath = Path.Combine(_testFolder, TestImageAac);
            var mediaImage = _mediaTagService.ReadMediaImage(testImageFilePath);
            Assert.IsNotNull(mediaImage);
            Assert.GreaterOrEqual(mediaImage.VerticalResolution, 72.0);
            Assert.GreaterOrEqual(mediaImage.HorizontalResolution, 72.0);
            Assert.GreaterOrEqual(mediaImage.Height, 300.0);
            Assert.GreaterOrEqual(mediaImage.Width, 300.0);

            var mediaImageUpdate = _mediaTagService.ReadMediaImage(MediaFolder + DiktatorOfTheFreeWorldAac);
            _mediaTagService.WriteMediaImage(testImageFilePath, mediaImageUpdate);

            mediaImage = _mediaTagService.ReadMediaImage(testImageFilePath);
            Assert.IsNotNull(mediaImage);
            Assert.GreaterOrEqual(mediaImage.VerticalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.HorizontalResolution, 300.0);
            Assert.GreaterOrEqual(mediaImage.Height, 600.0);
            Assert.GreaterOrEqual(mediaImage.Width, 600.0);
        }
    }
}
