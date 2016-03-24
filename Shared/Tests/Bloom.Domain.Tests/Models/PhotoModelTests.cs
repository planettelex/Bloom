using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the photo model class.
    /// </summary>
    [TestFixture]
    public class PhotoModelTests
    {
        private const string TestFilePath = "c:\\tests\\image.jpg";

        /// <summary>
        /// Tests the photo create method.
        /// </summary>
        [Test]
        public void CreatePhotoTest()
        {
            var photo = Photo.Create(TestFilePath);

            Assert.AreNotEqual(photo.Id, Guid.Empty);
            Assert.AreEqual(photo.FilePath, TestFilePath);
        }

        /// <summary>
        /// Tests the photos properties.
        /// </summary>
        [Test]
        public void PhotoPropertiesTest()
        {
            var id = Guid.NewGuid();
            var photo = new Photo
            {
                Id = id,
                Caption = "Caption",
                FilePath = TestFilePath,
                TakenOn = DateTime.Parse("10/10/2010")
            };

            Assert.AreEqual(photo.Id, id);
            Assert.AreEqual(photo.Caption, "Caption");
            Assert.AreEqual(photo.FilePath, TestFilePath);
            Assert.AreEqual(photo.TakenOn, DateTime.Parse("10/10/2010"));
        }

        /// <summary>
        /// Tests the photo to string method.
        /// </summary>
        [Test]
        public void PhotoToStringTest()
        {
            var photo = Photo.Create(TestFilePath);

            Assert.AreEqual(photo.ToString(), TestFilePath);
        }
    }
}