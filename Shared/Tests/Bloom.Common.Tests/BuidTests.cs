using System;
using NUnit.Framework;

namespace Bloom.Common.Tests
{
    /// <summary>
    /// Tests for the Buid class.
    /// </summary>
    [TestFixture]
    public class BuidTests
    {
        /// <summary>
        /// Tests the Buid properties.
        /// </summary>
        [Test]
        public void BuidPropertiesTest()
        {
            var libraryId = Guid.NewGuid();
            var entityId = Guid.NewGuid();
            var buid = new Buid(libraryId, BloomEntity.Song, entityId);

            Assert.AreEqual(buid.LibraryId, libraryId);
            Assert.AreEqual(buid.EntityId, entityId);
            Assert.AreEqual(buid.EntityType, BloomEntity.Song);
            Assert.AreEqual(buid.ToString(), libraryId + "|" + (int)BloomEntity.Song + "|" + entityId);
        }

        /// <summary>
        /// Tests the Buid.Empty property.
        /// </summary>
        [Test]
        public void EmptyBuidTest()
        {
            var emptyBuid = Buid.Empty;

            Assert.AreEqual(Guid.Empty, emptyBuid.LibraryId);
            Assert.AreEqual(BloomEntity.None, emptyBuid.EntityType);
            Assert.AreEqual(Guid.Empty, emptyBuid.EntityId);
        }
    }
}
