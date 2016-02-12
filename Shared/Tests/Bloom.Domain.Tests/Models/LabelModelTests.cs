using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
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
            // todo

            Assert.AreEqual(label.Personnel.Count, 1);
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
            //todo
            const string role1Name = "Accountant";
            const string role2Name = "Chief Financial Officer";
            var role1 = Role.Create(role1Name);
            var role2 = Role.Create(role2Name);


            Assert.AreEqual(label.Personnel.Count, 1);
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
            // todo

            Assert.AreEqual(album.Releases.Count, 1);
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
            // todo

            Assert.AreEqual(album.Releases.Count, 1);
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
           //AddRelease(album, releaseDate, MediaTypes.Digital | MediaTypes.CD | MediaTypes.Vinyl, DigitalFormats.MP3 | DigitalFormats.M4A | DigitalFormats.FLAC);

            Assert.AreEqual(album.Releases.Count, 1);
            //Assert.IsFalse(albumRelease.DigitalFormats.HasFlag(DigitalFormats.OGG));
        }
    }
}