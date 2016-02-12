using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the reference model class.
    /// </summary>
    [TestFixture]
    public class ReferenceModelTests
    {
        /// <summary>
        /// Tests the reference create method.
        /// </summary>
        [Test]
        public void CreateReferenceTest()
        {
            const string referenceTitle = "Test Reference";
            const string url = "http://www.test.com/reference";
            var reference = Reference.Create(referenceTitle, url);

            Assert.AreNotEqual(reference.Id, Guid.Empty);
            Assert.AreEqual(reference.Title, referenceTitle);
        }
    }
}