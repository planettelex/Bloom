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
        /// <summary>
        /// Tests the photo create method.
        /// </summary>
        [Test]
        public void CreatePhotoTest()
        {
            const string url = "http://test.com/image.jpg";
            var photo = Photo.Create(url);

            Assert.AreNotEqual(photo.Id, Guid.Empty);
            Assert.AreEqual(photo.FilePath, url);
        }
    }
}