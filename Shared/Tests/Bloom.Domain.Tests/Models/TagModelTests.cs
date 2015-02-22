using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the tag model class.
    /// </summary>
    [TestFixture]
    public class TagModelTests
    {
        /// <summary>
        /// Tests the tag create method.
        /// </summary>
        [Test]
        public void CreateTagTest()
        {
            const string tagName = "Test Tag";
            var tag = Tag.Create(tagName);

            Assert.AreNotEqual(tag.Id, Guid.Empty);
            Assert.AreEqual(tag.Name, tagName);
        }
    }
}