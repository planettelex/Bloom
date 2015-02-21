using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests
{
    /// <summary>
    /// Tests for the label model class.
    /// </summary>
    [TestFixture]
    public class LabelModelTests
    {
        const string LabelName = "Test Label";
        const string AlbumName = "Test Album";

        /// <summary>
        /// Tests the label create method.
        /// </summary>
        [Test]
        public void CreateLabelTest()
        {
            var label = Label.Create(LabelName);

            Assert.AreNotEqual(label.Id, Guid.Empty);
            Assert.AreEqual(label.Name, LabelName);
        }

        /// <summary>
        /// Tests adding personel to a label.
        /// </summary>
        [Test]
        public void AddPersonelToLabelTest()
        {
            const string personName = "Test Person";
            var label = Label.Create(LabelName);
            var person = Person.Create(personName);
            var labelPersonel = label.AddPersonel(person);

            Assert.AreEqual(label.Personel.Count, 1);
            Assert.AreEqual(labelPersonel.LabelId, label.Id);
            Assert.AreEqual(labelPersonel.PersonId, person.Id);
            Assert.AreEqual(labelPersonel.Person.Name, personName);
        }

        /// <summary>
        /// Tests adding personel with roles to a label.
        /// </summary>
        [Test]
        public void AddPersonelWithRolesToLabelTest()
        {
            const string personName = "Test Person";
            var label = Label.Create(LabelName);
            var person = Person.Create(personName);
            var labelPersonel = label.AddPersonel(person);
            const string role1Name = "Accountant";
            const string role2Name = "Chief Financial Officer";
            var role1 = Role.Create(role1Name);
            var role2 = Role.Create(role2Name);
            var labelPersonelRole1 = labelPersonel.AddRole(role1);
            var labelPersonelRole2 = labelPersonel.AddRole(role2);

            Assert.AreEqual(label.Personel.Count, 1);
            Assert.AreEqual(labelPersonel.LabelId, label.Id);
            Assert.AreEqual(labelPersonel.PersonId, person.Id);
            Assert.AreEqual(labelPersonel.Person.Name, person.Name);
            Assert.AreEqual(labelPersonel.Roles.Count, 2);
            Assert.AreEqual(labelPersonelRole1.RoleId, role1.Id);
            Assert.AreEqual(labelPersonelRole1.Role.Name, role1Name);
            Assert.AreEqual(labelPersonelRole2.RoleId, role2.Id);
            Assert.AreEqual(labelPersonelRole2.Role.Name, role2Name);
        }

        /// <summary>
        /// Tests adding an album release to a label.
        /// </summary>
        [Test]
        public void AddReleaseToLabelTest()
        {
            var releaseDate = DateTime.Now.AddDays(-900);
            var album = Album.Create(AlbumName);
            var label = Label.Create(LabelName);
            var albumRelease = label.AddRelease(album, releaseDate);

            Assert.AreEqual(album.Releases.Count, 1);
            Assert.AreEqual(label.Releases.Count, 1);
            Assert.AreEqual(albumRelease.AlbumId, album.Id);
            Assert.AreEqual(albumRelease.LabelId, label.Id);
            Assert.AreEqual(albumRelease.Label.Name, LabelName);
            Assert.AreEqual(albumRelease.ReleaseDate, releaseDate);
        }

        /// <summary>
        /// Tests adding an album release with media types to a label.
        /// </summary>
        [Test]
        public void AddReleaseWithMediaTypesToLabelTest()
        {
            var releaseDate = DateTime.Now.AddDays(-900);
            var album = Album.Create(AlbumName);
            var label = Label.Create(LabelName);
            var albumRelease = label.AddRelease(album, releaseDate, MediaTypes.CD | MediaTypes.Vinyl);

            Assert.AreEqual(album.Releases.Count, 1);
            Assert.AreEqual(label.Releases.Count, 1);
            Assert.AreEqual(albumRelease.AlbumId, album.Id);
            Assert.AreEqual(albumRelease.LabelId, label.Id);
            Assert.AreEqual(albumRelease.Label.Name, LabelName);
            Assert.AreEqual(albumRelease.ReleaseDate, releaseDate);
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.CD));
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.Vinyl));
            Assert.IsFalse(albumRelease.MediaTypes.HasFlag(MediaTypes.Cassette));
        }

        /// <summary>
        /// Tests adding an album release with media types and digital formats to a label.
        /// </summary>
        [Test]
        public void AddReleaseWithDigitalFormatsToLabelTest()
        {
            var releaseDate = DateTime.Now.AddDays(-900);
            var album = Album.Create(AlbumName);
            var label = Label.Create(LabelName);
            var albumRelease = label.AddRelease(album, releaseDate, MediaTypes.Digital | MediaTypes.CD | MediaTypes.Vinyl, DigitalFormats.MP3 | DigitalFormats.M4A | DigitalFormats.FLAC);

            Assert.AreEqual(album.Releases.Count, 1);
            Assert.AreEqual(label.Releases.Count, 1);
            Assert.AreEqual(albumRelease.AlbumId, album.Id);
            Assert.AreEqual(albumRelease.LabelId, label.Id);
            Assert.AreEqual(albumRelease.Label.Name, LabelName);
            Assert.AreEqual(albumRelease.ReleaseDate, releaseDate);
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.Digital));
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.CD));
            Assert.IsTrue(albumRelease.MediaTypes.HasFlag(MediaTypes.Vinyl));
            Assert.IsFalse(albumRelease.MediaTypes.HasFlag(MediaTypes.Cassette));
            Assert.IsTrue(albumRelease.DigitalFormats.HasFlag(DigitalFormats.MP3));
            Assert.IsTrue(albumRelease.DigitalFormats.HasFlag(DigitalFormats.M4A));
            Assert.IsTrue(albumRelease.DigitalFormats.HasFlag(DigitalFormats.FLAC));
            Assert.IsFalse(albumRelease.DigitalFormats.HasFlag(DigitalFormats.OGG));
        }
    }
}