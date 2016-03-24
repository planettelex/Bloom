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
        private const string TestTitle = "Test Reference";
        private const string TestUrl = "http://www.test.com/reference";

        /// <summary>
        /// Tests the reference create method.
        /// </summary>
        [Test]
        public void CreateReferenceTests()
        {
            var source = Source.Create("Source");
            var reference = Reference.Create(TestUrl);

            Assert.AreNotEqual(reference.Id, Guid.Empty);
            Assert.AreEqual(reference.Url, TestUrl);
            
            reference = Reference.Create(TestTitle, TestUrl);

            Assert.AreNotEqual(reference.Id, Guid.Empty);
            Assert.AreEqual(reference.Title, TestTitle);
            Assert.AreEqual(reference.Url, TestUrl);

            reference = Reference.Create(source, TestUrl);

            Assert.AreNotEqual(reference.Id, Guid.Empty);
            Assert.AreEqual(reference.Url, TestUrl);
            Assert.NotNull(reference.Source);
            Assert.AreEqual(reference.SourceId, source.Id);

            reference = Reference.Create(source, TestTitle, TestUrl);

            Assert.AreNotEqual(reference.Id, Guid.Empty);
            Assert.AreEqual(reference.Title, TestTitle);
            Assert.AreEqual(reference.Url, TestUrl);
            Assert.NotNull(reference.Source);
            Assert.AreEqual(reference.SourceId, source.Id);
        }

        /// <summary>
        /// Tests the reference properties.
        /// </summary>
        [Test]
        public void ReferencePropertiesTest()
        {
            var id = Guid.NewGuid();
            var source = Source.Create("Source");
            var reference = new Reference
            {
                Id = id,
                Source = source,
                Title = TestTitle,
                Url = TestUrl
            };

            Assert.AreEqual(reference.Id, id);
            Assert.NotNull(reference.Source);
            Assert.AreEqual(reference.SourceId, source.Id);
            Assert.AreEqual(reference.Title, TestTitle);
            Assert.AreEqual(reference.Url, TestUrl);
        }

        /// <summary>
        /// Tests the reference to string method.
        /// </summary>
        [Test]
        public void ReferenceToStringTest()
        {
            var reference = Reference.Create(TestUrl);

            Assert.AreEqual(reference.ToString(), TestUrl);
        }
    }
}