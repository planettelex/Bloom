using System;
using System.Collections.Generic;
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
            var artist = Artist.Create("Test Artist");
            var holiday = Holiday.Create("Holiday");
            var tributeArtistId = Guid.NewGuid();
            var originalAlbumId = Guid.NewGuid();

            var album = new Album
            {
                Id = id,
                Name = AlbumName,
                UnofficialName = "Unofficial Name",
                Edition = "Album Edition",
                Artist = artist,
                DiscCount = 1,
                TrackCounts = "13",
                Description = "Album description",
                Holiday = holiday,
                IsBootleg = false,
                IsCompilation = true,
                IsLive = false,
                IsRemix = true,
                IsMixedArtist = true,
                IsPromotional = false,
                IsTribute = true,
                IsSingleTrack = false,
                IsSoundtrack = true,
                Length = 12345,
                LengthType = LengthType.LP,
                LinerNotes = "Liner notes",
                TributeArtistId = tributeArtistId,
                OriginalAlbumId = originalAlbumId,
                Rating = 4,
                FirstReleasedOn = DateTime.Parse("1/1/2001")
            };

            Assert.AreEqual(album.Id, id);
            Assert.AreEqual(album.Name, AlbumName);
            Assert.AreEqual(album.Edition, "Album Edition");
            Assert.AreEqual(album.ArtistId, artist.Id);
            Assert.NotNull(album.Artist);
            Assert.AreEqual(album.DiscCount, 1);
            Assert.AreEqual(album.TrackCounts, "13");
            Assert.AreEqual(album.GetTrackCount(1), 13);
            Assert.AreEqual(album.Description, "Album description");
            Assert.IsFalse(album.IsBootleg);
            Assert.IsTrue(album.IsCompilation);
            Assert.IsFalse(album.IsLive);
            Assert.IsTrue(album.IsRemix);
            Assert.IsTrue(album.IsHoliday);
            Assert.AreEqual(holiday.Id, album.HolidayId);
            Assert.NotNull(album.Holiday);
            Assert.IsTrue(album.IsMixedArtist);
            Assert.IsFalse(album.IsPromotional);
            Assert.IsTrue(album.IsTribute);
            Assert.IsFalse(album.IsSingleTrack);
            Assert.IsTrue(album.IsSoundtrack);
            Assert.AreEqual(album.Length, 12345);
            Assert.AreEqual(album.LengthType, LengthType.LP);
            Assert.AreEqual(album.LinerNotes, "Liner notes");
            Assert.AreEqual(album.TributeArtistId, tributeArtistId);
            Assert.AreEqual(album.OriginalAlbumId, originalAlbumId);
            Assert.AreEqual(album.Rating, 4);
            Assert.AreEqual(album.FirstReleasedOn, DateTime.Parse("1/1/2001"));
        }

        /// <summary>
        /// Tests the get and set album methods.
        /// </summary>
        [Test]
        public void AlbumTrackTests()
        {
            const string artistName = "Test Artist";
            var artist = Artist.Create(artistName);
            var album = Album.Create(AlbumName, artist);

            album.SetTrackCount(1, 12);
            Assert.AreEqual(12, album.GetTrackCount(1));

            album = Album.Create(AlbumName, artist);
            album.SetTrackCount(2, 13);
            Assert.IsNull(album.GetTrackCount(1));
            Assert.AreEqual(13, album.GetTrackCount(2));

            album = Album.Create(AlbumName, artist);
            album.SetTrackCount(3, 14);
            Assert.IsNull(album.GetTrackCount(1));
            Assert.IsNull(album.GetTrackCount(2));
            Assert.AreEqual(14, album.GetTrackCount(3));

            album = Album.Create(AlbumName, artist);
            album.SetTrackCount(1, 10);
            album.SetTrackCount(2, 11);
            album.SetTrackCount(3, 12);
            Assert.AreEqual(10, album.GetTrackCount(1));
            Assert.AreEqual(11, album.GetTrackCount(2));
            Assert.AreEqual(12, album.GetTrackCount(3));

            album = Album.Create(AlbumName, artist);
            album.SetTrackCount(3, 12);
            album.SetTrackCount(2, 11);
            album.SetTrackCount(1, 10);
            Assert.AreEqual(10, album.GetTrackCount(1));
            Assert.AreEqual(11, album.GetTrackCount(2));
            Assert.AreEqual(12, album.GetTrackCount(3));

            album = Album.Create(AlbumName, artist);
            album.SetTrackCount(1, 8);
            album.SetTrackCount(3, 10);
            album.SetTrackCount(2, 9);
            Assert.AreEqual(8, album.GetTrackCount(1));
            Assert.AreEqual(9, album.GetTrackCount(2));
            Assert.AreEqual(10, album.GetTrackCount(3));
        }

        /// <summary>
        /// Tests the album to string method.
        /// </summary>
        [Test]
        public void AlbumToStringTest()
        {
            var album1 = Album.Create(AlbumName);
            var album2 = Album.Create("Album 2", Artist.Create("Artist"));
            Assert.AreEqual(album1.ToString(), AlbumName);
            Assert.AreEqual(album2.ToString(), "Artist: Album 2");
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
        /// Tests the album artwork to string method.
        /// </summary>
        [Test]
        public void AlbumArtworkToStringTest()
        {
            var album = Album.Create(AlbumName);
            var albumArtwork = AlbumArtwork.Create(album, "c:\\Music\\Image.jpg", 3);

            Assert.AreEqual(albumArtwork.ToString(), "c:\\Music\\Image.jpg");
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
        /// Tests the album collaborator properties.
        /// </summary>
        [Test]
        public void AlbumCollaboratorPropertiesTest()
        {
            var albumId = Guid.NewGuid();
            var artist = Artist.Create("Artist");

            var albumCollaborator = new AlbumCollaborator
            {
                AlbumId = albumId,
                Artist = artist,
                IsFeatured = true
            };

            Assert.AreEqual(albumCollaborator.AlbumId, albumId);
            Assert.AreEqual(albumCollaborator.ArtistId, artist.Id);
            Assert.NotNull(albumCollaborator.Artist);
            Assert.IsTrue(albumCollaborator.IsFeatured);
        }

        /// <summary>
        /// Tests the album collaborator to string method.
        /// </summary>
        [Test]
        public void AlbumCollaboratorToStringTest()
        {
            var album = Album.Create(AlbumName);
            var artist = Artist.Create("Artist");
            var albumCollaborator = AlbumCollaborator.Create(album, artist);
            albumCollaborator.IsFeatured = true;

            Assert.AreEqual(albumCollaborator.ToString(), "Artist (Featured)");
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
        /// Tests the album credit properties.
        /// </summary>
        [Test]
        public void AlbumCreditPropertiesTest()
        {
            var albumId = Guid.NewGuid();
            var person = Person.Create("Person");
            var roles = new List<Role>
            {
                Role.Create("Role 1"),
                Role.Create("Role 2")
            };

            var albumCredit = new AlbumCredit
            {
                AlbumId = albumId,
                Person = person,
                Roles = roles
            };

            Assert.AreEqual(albumCredit.AlbumId, albumId);
            Assert.AreEqual(albumCredit.PersonId, person.Id);
            Assert.NotNull(albumCredit.Person);
            Assert.NotNull(albumCredit.Roles);
            Assert.AreEqual(2, albumCredit.Roles.Count);
        }

        /// <summary>
        /// Tests the album credit to string method.
        /// </summary>
        [Test]
        public void AlbumCreditToStringTest()
        {
            var album = Album.Create(AlbumName);
            var person = Person.Create("Person");
            var albumCredit = AlbumCredit.Create(album, person);

            Assert.AreEqual(albumCredit.ToString(), "Person");
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
            const MediaTypes mediaTypes = MediaTypes.CD | MediaTypes.Digital;
            const DigitalFormats digitalFormats = DigitalFormats.MP3 | DigitalFormats.WAV;

            var release1 = AlbumRelease.Create(album, releaseDate);
            var release2 = AlbumRelease.Create(album, releaseDate, mediaTypes);
            var release3 = AlbumRelease.Create(album, releaseDate, digitalFormats);
            var release4 = AlbumRelease.Create(album, releaseDate, mediaTypes, digitalFormats);
            var release5 = AlbumRelease.Create(album, releaseDate, digitalFormats, label, "123456789");
            var release6 = AlbumRelease.Create(album, releaseDate, mediaTypes, label, "ZYXWVUTSRQPONMLK");
            var release7 = AlbumRelease.Create(album, releaseDate, mediaTypes, digitalFormats, label, "ABCDEFGHIJKLMNOP");

            Assert.AreEqual(release1.AlbumId, album.Id);
            Assert.AreEqual(release1.ReleaseDate, releaseDate);

            Assert.AreEqual(release2.AlbumId, album.Id);
            Assert.AreEqual(release2.ReleaseDate, releaseDate);
            Assert.AreEqual(release2.MediaTypes, mediaTypes);
            Assert.IsTrue(release2.MediaTypes.HasFlag(MediaTypes.CD));
            Assert.IsTrue(release2.MediaTypes.HasFlag(MediaTypes.Digital));
            Assert.IsFalse(release2.MediaTypes.HasFlag(MediaTypes.EightTrack));

            Assert.AreEqual(release3.AlbumId, album.Id);
            Assert.AreEqual(release3.ReleaseDate, releaseDate);
            Assert.AreEqual(release3.MediaTypes, MediaTypes.Digital);
            Assert.AreEqual(release3.DigitalFormats, digitalFormats);
            Assert.IsNotNull(release3.DigitalFormats);
            Assert.IsTrue(release3.DigitalFormats != null && release3.DigitalFormats.Value.HasFlag(DigitalFormats.MP3));
            Assert.IsTrue(release3.DigitalFormats.Value.HasFlag(DigitalFormats.WAV));
            Assert.IsFalse(release3.DigitalFormats.Value.HasFlag(DigitalFormats.OGG));

            Assert.AreEqual(release4.AlbumId, album.Id);
            Assert.AreEqual(release4.ReleaseDate, releaseDate);
            Assert.AreEqual(release4.MediaTypes, mediaTypes);
            Assert.AreEqual(release4.DigitalFormats, digitalFormats);

            Assert.AreEqual(release5.AlbumId, album.Id);
            Assert.AreEqual(release5.ReleaseDate, releaseDate);
            Assert.AreEqual(release5.MediaTypes, MediaTypes.Digital);
            Assert.AreEqual(release5.DigitalFormats, digitalFormats);
            Assert.AreEqual(release5.CatalogNumber, "123456789");

            Assert.AreEqual(release6.AlbumId, album.Id);
            Assert.AreEqual(release6.LabelId, label.Id);
            Assert.AreEqual(release6.ReleaseDate, releaseDate);
            Assert.AreEqual(release6.MediaTypes, mediaTypes);
            Assert.AreEqual(release6.CatalogNumber, "ZYXWVUTSRQPONMLK");

            Assert.AreEqual(release7.AlbumId, album.Id);
            Assert.AreEqual(release7.LabelId, label.Id);
            Assert.AreEqual(release7.ReleaseDate, releaseDate);
            Assert.AreEqual(release7.MediaTypes, mediaTypes);
            Assert.AreEqual(release7.DigitalFormats, digitalFormats);
            Assert.AreEqual(release7.CatalogNumber, "ABCDEFGHIJKLMNOP");
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
        /// Tests the album release to string method.
        /// </summary>
        [Test]
        public void AlbumReleaseToStringTest()
        {
            var album = Album.Create(AlbumName);
            var releaseDate = DateTime.Parse("01/01/2001");
            
            var release = AlbumRelease.Create(album, releaseDate);

            Assert.AreEqual(release.ToString(), releaseDate.ToShortDateString());
        }

        /// <summary>
        /// Tests the album review create method.
        /// </summary>
        [Test]
        public void CreateAlbumReviewTest()
        {
            var album = Album.Create(AlbumName);
            var source = Source.Create("Source");
            var review = Review.Create(source, "http://www.test.com/review-article");
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
            var song = Song.Create("Song", Artist.Create("Artist"));
            var albumTrack = new AlbumTrack
            {
                Id = id,
                AlbumId = albumId,
                Song = song,
                DiscNumber = 2,
                TrackNumber = 5,
                StartTime = 54321,
                StopTime = 54334
            };

            Assert.AreEqual(albumTrack.Id, id);
            Assert.AreEqual(albumTrack.AlbumId, albumId);
            Assert.AreEqual(albumTrack.SongId, song.Id);
            Assert.AreEqual(albumTrack.DiscNumber, 2);
            Assert.AreEqual(albumTrack.TrackNumber, 5);
            Assert.AreEqual(albumTrack.StartTime, 54321);
            Assert.AreEqual(albumTrack.StopTime, 54334);
        }

        /// <summary>
        /// Tests the album track to string method.
        /// </summary>
        [Test]
        public void AlbumTrackToStringTest()
        {
            var album = Album.Create(AlbumName);
            var artist = Artist.Create("Artist");
            var song = Song.Create("Song", artist);
            var albumTrack = AlbumTrack.Create(album, song, 1);

            Assert.AreEqual(albumTrack.ToString(), "1-1: Song");
        }

        /// <summary>
        /// Tests the album media create methods.
        /// </summary>
        [Test]
        public void CreateAlbumMediaTest()
        {
            var album = Album.Create(AlbumName);
            var albumMedia1 = AlbumMedia.Create(album, MediaTypes.CD);
            var albumMedia2 = AlbumMedia.Create(album, DigitalFormats.MP3);

            Assert.AreNotEqual(albumMedia1.Id, Guid.Empty);
            Assert.AreEqual(albumMedia1.AlbumId, album.Id);
            Assert.AreEqual(albumMedia1.MediaType, MediaTypes.CD);
            Assert.AreNotEqual(albumMedia2.Id, Guid.Empty);
            Assert.AreEqual(albumMedia2.MediaType, MediaTypes.Digital);
            Assert.AreEqual(albumMedia2.DigitalFormat, DigitalFormats.MP3);
        }

        /// <summary>
        /// Tests the album media properties.
        /// </summary>
        [Test]
        public void AlbumMediaPropertiesTest()
        {
            var id = Guid.NewGuid();
            var album = Album.Create("Album");
            var onLoanToPersonId = Guid.NewGuid();
            var release = AlbumRelease.Create(album, DateTime.Parse("11/11/2011"));

            var albumMedia = new AlbumMedia
            {
                Id = id,
                AlbumId = album.Id,
                MediaType = MediaTypes.Digital,
                DigitalFormat = DigitalFormats.FLAC,
                MediaCondition = Condition.GoodPlus,
                PackagingCondition = Condition.Good,
                ApproximateValue = 22.95m,
                PurchasedPrice = 12.99m,
                PurchasedOn = DateTime.Parse("2/2/2002"),
                OnLoanToPersonId = onLoanToPersonId,
                Release = release
            };

            Assert.AreEqual(id, albumMedia.Id);
            Assert.AreEqual(album.Id, albumMedia.AlbumId);
            Assert.AreEqual(MediaTypes.Digital, albumMedia.MediaType);
            Assert.AreEqual(DigitalFormats.FLAC, albumMedia.DigitalFormat);
            Assert.AreEqual(Condition.GoodPlus, albumMedia.MediaCondition);
            Assert.AreEqual(Condition.Good, albumMedia.PackagingCondition);
            Assert.AreEqual(22.95m, albumMedia.ApproximateValue);
            Assert.AreEqual(12.99m, albumMedia.PurchasedPrice);
            Assert.AreEqual(DateTime.Parse("2/2/2002"), albumMedia.PurchasedOn);
            Assert.AreEqual(onLoanToPersonId, albumMedia.OnLoanToPersonId);
            Assert.AreEqual(release.Id, albumMedia.ReleaseId);
            Assert.NotNull(albumMedia.Release);
        }

        /// <summary>
        /// Tests the album media to string method.
        /// </summary>
        [Test]
        public void AlbumMediaToStringTest()
        {
            var album = Album.Create(AlbumName);
            var albumMedia1 = AlbumMedia.Create(album, MediaTypes.CD);
            var albumMedia2 = AlbumMedia.Create(album, DigitalFormats.MP3);

            Assert.AreEqual(albumMedia1.ToString(), "CD");
            Assert.AreEqual(albumMedia2.ToString(), "MP3");
        }
    }
}