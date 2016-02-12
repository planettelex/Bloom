using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the publication model class.
    /// </summary>
    [TestFixture]
    public class PublicationModelTests
    {
        /// <summary>
        /// Tests the holiday create method.
        /// </summary>
        [Test]
        public void CreatePublicationTest()
        {
            const string publicationName = "Test Publication";
            var publication = Source.Create(publicationName);

            Assert.AreNotEqual(publication.Id, Guid.Empty);
            Assert.AreEqual(publication.Name, publicationName);
        }
    }
}