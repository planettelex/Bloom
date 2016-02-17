using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests the album model classes.
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
        /// Tests the album properties.
        /// </summary>
        [Test]
        public void AlbumPropertiesTest()
        {
            var id = Guid.NewGuid();
            var artistId = Guid.NewGuid();
            var tributeArtistId = Guid.NewGuid();

            var album = new Album
            {
                Id = id,
                Name = AlbumName,
                Edition = "Album Edition",
                ArtistId = artistId,
                Description = "Album description",
                IsBootleg = false,
                IsCompilation = true,
                IsLive = false,
                IsRemix = true,
                IsHoliday = false,
                IsMixedArtist = true,
                IsPromotional = false,
                IsTribute = true,
                IsSingleTrack = false,
                IsSoundtrack = true,
                Length = 12345,
                LengthType = LengthType.LP,
                LinerNotes = "Liner notes",
                TributeArtistId = tributeArtistId
            };

            Assert.AreEqual(album.Id, id);
            Assert.AreEqual(album.Name, AlbumName);
            Assert.AreEqual(album.Edition, "Album Edition");
            Assert.AreEqual(album.ArtistId, artistId);
            Assert.AreEqual(album.Description, "Album description");
            Assert.IsFalse(album.IsBootleg);
            Assert.IsTrue(album.IsCompilation);
            Assert.IsFalse(album.IsLive);
            Assert.IsTrue(album.IsRemix);
            Assert.IsFalse(album.IsHoliday);
            Assert.IsTrue(album.IsMixedArtist);
            Assert.IsFalse(album.IsPromotional);
            Assert.IsTrue(album.IsTribute);
            Assert.IsFalse(album.IsSingleTrack);
            Assert.IsTrue(album.IsSoundtrack);
            Assert.AreEqual(album.Length, 12345);
            Assert.AreEqual(album.LengthType, LengthType.LP);
            Assert.AreEqual(album.LinerNotes, "Liner notes");
            Assert.AreEqual(album.TributeArtistId, tributeArtistId);
        }

        /// <summary>
        /// Tests the album activity create method.
        /// </summary>
        [Test]
        public void CreateAlbumActivityTest()
        {
            var album = Album.Create(AlbumName);
            var activity = Activity.Create("Activity");
            var albumActivity = AlbumActivity.Create(album, activity);

            Assert.AreEqual(albumActivity.AlbumId, album.Id);
            Assert.AreEqual(albumActivity.ActivityId, activity.Id);
        }

        /// <summary>
        /// Tests the album mood create method.
        /// </summary>
        [Test]
        public void CreateAlbumMoodTest()
        {
            var album = Album.Create(AlbumName);
            var mood = Mood.Create("Mood");
            var albumMood = AlbumMood.Create(album, mood);

            Assert.AreEqual(albumMood.AlbumId, album.Id);
            Assert.AreEqual(albumMood.MoodId, mood.Id);
        }

        /// <summary>
        /// Tests the album artwork create method.
        /// </summary>
        [Test]
        public void CreateAlbumArtworkTest()
        {
            var album = Album.Create(AlbumName);
            var albumArtwork = AlbumArtwork.Create(album, "c:\\Music\\Image.jpg", 3);

            Assert.AreEqual(albumArtwork.AlbumId, album.Id);
            Assert.AreEqual(albumArtwork.FilePath, "c:\\Music\\Image.jpg");
            Assert.AreEqual(albumArtwork.Priority, 3);
        }

        /// <summary>
        /// Tests the album collaborator create method.
        /// </summary>
        [Test]
        public void CreateAlbumCollaboratorTest()
        {
            var album = Album.Create(AlbumName);
            var artist = Artist.Create("Artist");
            var albumCollaborator = AlbumCollaborator.Create(album, artist);

            Assert.AreEqual(albumCollaborator.AlbumId, album.Id);
            Assert.AreEqual(albumCollaborator.ArtistId, artist.Id);
            Assert.AreEqual(albumCollaborator.Artist.Name, "Artist");
        }

        /// <summary>
        /// Tests the album credit create method.
        /// </summary>
        [Test]
        public void CreateAlbumCreditTest()
        {
            var album = Album.Create(AlbumName);
            var person = Person.Create("Person");
            var albumCredit = AlbumCredit.Create(album, person);

            Assert.AreEqual(albumCredit.AlbumId, album.Id);
            Assert.AreEqual(albumCredit.PersonId, person.Id);
            Assert.AreEqual(albumCredit.Person.Name, "Person");
        }

        /// <summary>
        /// Tests the album credit role create method.
        /// </summary>
        [Test]
        public void CreateAlbumCreditRoleTest()
        {
            var album = Album.Create(AlbumName);
            var person = Person.Create("Person");
            var role = Role.Create("Role");
            var albumCredit = AlbumCredit.Create(album, person);
            var albumCreditRole = AlbumCreditRole.Create(albumCredit, role);

            Assert.AreEqual(albumCreditRole.AlbumCreditId, albumCredit.Id);
            Assert.AreEqual(albumCreditRole.RoleId, role.Id);
        }

        /// <summary>
        /// Tests the album reference create method.
        /// </summary>
        [Test]
        public void CreateAlbumReferenceTest()
        {
            var album = Album.Create(AlbumName);
            var reference = Reference.Create("Reference Title", "http://www.test.com");
            var albumReference = AlbumReference.Create(album, reference);

            Assert.AreEqual(albumReference.AlbumId, album.Id);
            Assert.AreEqual(albumReference.ReferenceId, reference.Id);
        }

        /// <summary>
        /// Tests the album release create methods.
        /// </summary>
        [Test]
        public void CreateAlbumReleaseTest()
        {
            var album = Album.Create(AlbumName);
            var label = Label.Create("Label Name");
            var releaseDate = DateTime.Parse("01/01/2001");
            const MediaTypes mediaTypes = MediaTypes.Digital;
            const DigitalFormats digitalFormats = DigitalFormats.MP3 & DigitalFormats.WAV;

            var release1 = AlbumRelease.Create(album, releaseDate);
            var release2 = AlbumRelease.Create(album, releaseDate, mediaTypes);
            var release3 = AlbumRelease.Create(album, releaseDate, mediaTypes, digitalFormats);
            var release4 = AlbumRelease.Create(album, releaseDate, mediaTypes, digitalFormats, label, "ABCDEFGHIJKLMNOP");

            Assert.AreEqual(release1.AlbumId, album.Id);
            Assert.AreEqual(release1.ReleaseDate, releaseDate);

            Assert.AreEqual(release2.AlbumId, album.Id);
            Assert.AreEqual(release2.ReleaseDate, releaseDate);
            Assert.AreEqual(release2.MediaTypes, mediaTypes);

            Assert.AreEqual(release3.AlbumId, album.Id);
            Assert.AreEqual(release3.ReleaseDate, releaseDate);
            Assert.AreEqual(release3.MediaTypes, mediaTypes);
            Assert.AreEqual(release3.DigitalFormats, digitalFormats);

            Assert.AreEqual(release4.AlbumId, album.Id);
            Assert.AreEqual(release4.LabelId, label.Id);
            Assert.AreEqual(release4.ReleaseDate, releaseDate);
            Assert.AreEqual(release4.MediaTypes, mediaTypes);
            Assert.AreEqual(release4.DigitalFormats, digitalFormats);
            Assert.AreEqual(release4.CatalogNumber, "ABCDEFGHIJKLMNOP");
        }

        /// <summary>
        /// Tests the album release properties.
        /// </summary>
        [Test]
        public void AlbumReleasePropertiesTest()
        {
            var id = Guid.NewGuid();
            var albumId = Guid.NewGuid();
            var labelId = Guid.NewGuid();
            var albumRelease = new AlbumRelease
            {
                Id = id,
                AlbumId = albumId,
                LabelId = labelId,
                ReleaseDate = DateTime.Parse("01/01/2001"),
                MediaTypes = MediaTypes.Vinyl & MediaTypes.CD,
                DigitalFormats = DigitalFormats.MP3,
                CatalogNumber = "0987654321"
            };

            Assert.AreEqual(albumRelease.Id, id);
            Assert.AreEqual(albumRelease.AlbumId, albumId);
            Assert.AreEqual(albumRelease.LabelId, labelId);
            Assert.AreEqual(albumRelease.ReleaseDate, DateTime.Parse("01/01/2001"));
            Assert.AreEqual(albumRelease.MediaTypes, MediaTypes.Vinyl & MediaTypes.CD);
            Assert.AreEqual(albumRelease.DigitalFormats, DigitalFormats.MP3);
            Assert.AreEqual(albumRelease.CatalogNumber, "0987654321");
        }

        /// <summary>
        /// Tests the album review create method.
        /// </summary>
        [Test]
        public void CreateAlbumReviewTest()
        {
            var album = Album.Create(AlbumName);
            var review = Review.Create("http://www.test.com/review-article");
            var albumReview = AlbumReview.Create(album, review);

            Assert.AreEqual(albumReview.AlbumId, album.Id);
            Assert.AreEqual(albumReview.ReviewId, review.Id);
        }

        /// <summary>
        /// Tests the album tag create method.
        /// </summary>
        [Test]
        public void CreateAlbumTagTest()
        {
            var album = Album.Create(AlbumName);
            var tag = Tag.Create("Tag");
            var albumTag = AlbumTag.Create(album, tag);

            Assert.AreEqual(albumTag.AlbumId, album.Id);
            Assert.AreEqual(albumTag.TagId, tag.Id);
        }

        /// <summary>
        /// Tests the album track create methods.
        /// </summary>
        [Test]
        public void CreateAlbumTrackTest()
        {
            var album = Album.Create(AlbumName);
            var artist = Artist.Create("Artist");
            var song = Song.Create("Song", artist);
            var albumTrack1 = AlbumTrack.Create(album, song, 3);
            var albumTrack2 = AlbumTrack.Create(album, song, 4, 2);

            Assert.AreNotEqual(albumTrack1.Id, Guid.Empty);
            Assert.AreEqual(albumTrack1.AlbumId, album.Id);
            Assert.AreEqual(albumTrack1.SongId, song.Id);
            Assert.AreEqual(albumTrack1.Song.Name, song.Name);
            Assert.AreEqual(albumTrack1.TrackNumber, 3);

            Assert.AreNotEqual(albumTrack2.Id, Guid.Empty);
            Assert.AreEqual(albumTrack2.AlbumId, album.Id);
            Assert.AreEqual(albumTrack2.SongId, song.Id);
            Assert.AreEqual(albumTrack2.Song.Name, song.Name);
            Assert.AreEqual(albumTrack2.TrackNumber, 4);
            Assert.AreEqual(albumTrack2.DiscNumber, 2);
        }

        /// <summary>
        /// Tests the album track properties.
        /// </summary>
        [Test]
        public void AlbumTrackPropertiesTest()
        {
            var id = Guid.NewGuid();
            var albumId = Guid.NewGuid();
            var songId = Guid.NewGuid();
            var albumTrack = new AlbumTrack
            {
                Id = id,
                AlbumId = albumId,
                SongId = songId,
                DiscNumber = 2,
                TrackNumber = 5,
                StartTime = 54321,
                StopTime = 54334
            };

            Assert.AreEqual(albumTrack.Id, id);
            Assert.AreEqual(albumTrack.AlbumId, albumId);
            Assert.AreEqual(albumTrack.SongId, songId);
            Assert.AreEqual(albumTrack.DiscNumber, 2);
            Assert.AreEqual(albumTrack.TrackNumber, 5);
            Assert.AreEqual(albumTrack.StartTime, 54321);
            Assert.AreEqual(albumTrack.StopTime, 54334);
        }
    }
}