using System;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using NUnit.Framework;

namespace Bloom.State.Domain.Tests.Models
{
    /// <summary>
    /// Tests the tab model class.
    /// </summary>
    [TestFixture]
    public class TabModelTests
    {
        /// <summary>
        /// Tests the tab create method.
        /// </summary>
        [Test]
        public void CreateTabTest()
        {
            var person = Person.Create("Person Name");
            var user = User.Create(person);
            var buid = new Buid(Guid.NewGuid(), BloomEntity.Song, Guid.NewGuid());
            var tab = Tab.Create(ProcessType.Browser, user, buid, 2, TabType.Song, "Header");

            Assert.AreEqual(user.PersonId, tab.UserId);
            Assert.AreEqual(ProcessType.Browser, tab.Process);
            Assert.AreEqual(TabType.Song, tab.Type);
            Assert.AreEqual(2, tab.Order);
            Assert.AreEqual("Header", tab.Header);
            Assert.AreEqual(buid.LibraryId, tab.LibraryId);
            Assert.AreEqual(buid.EntityId, tab.EntityId);
        }

        /// <summary>
        /// Tests the tab create method.
        /// </summary>
        [Test]
        public void CreateTabWithViewTest()
        {
            var person = Person.Create("Person Name");
            var user = User.Create(person);
            var buid = new Buid(Guid.NewGuid(), BloomEntity.Song, Guid.NewGuid());
            var tab = Tab.Create(ProcessType.Browser, user, buid, 2, TabType.Song, "Header", "Grid");

            Assert.AreEqual(user.PersonId, tab.UserId);
            Assert.AreEqual(ProcessType.Browser, tab.Process);
            Assert.AreEqual(TabType.Song, tab.Type);
            Assert.AreEqual(2, tab.Order);
            Assert.AreEqual("Header", tab.Header);
            Assert.AreEqual(buid.LibraryId, tab.LibraryId);
            Assert.AreEqual(buid.EntityId, tab.EntityId);
            Assert.AreEqual("Grid", tab.View);
        }
    }
}
