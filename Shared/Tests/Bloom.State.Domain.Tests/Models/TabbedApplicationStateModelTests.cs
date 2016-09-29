using System;
using System.Collections.Generic;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using NUnit.Framework;

namespace Bloom.State.Domain.Tests.Models
{
    /// <summary>
    /// Tests the tabbed application state model class.
    /// </summary>
    [TestFixture]
    public class TabbedApplicationStateModelTests
    {
        /// <summary>
        /// A test tabbed application state.
        /// </summary>
        /// <seealso cref="Bloom.State.Domain.Models.TabbedApplicationState" />
        public class TestTabbedApplicationState : TabbedApplicationState { }

        /// <summary>
        /// Tests the has library tabs method.
        /// </summary>
        [Test]
        public void HasLibraryTabsTests()
        {
            var person = Person.Create("Person Name");
            var user = User.Create(person);

            var buid1 = new Buid(Guid.NewGuid(), BloomEntity.Song, Guid.NewGuid());
            var tab1 = Tab.Create(ProcessType.Browser, user, buid1, 2, TabType.Song, "Header", "Grid");

            var tabbedApplicationState = new TestTabbedApplicationState { Tabs = new List<Tab>() };
            Assert.IsFalse(tabbedApplicationState.HasLibraryTabs(buid1.LibraryId));

            tabbedApplicationState.Tabs.Add(tab1);
            Assert.IsTrue(tabbedApplicationState.HasLibraryTabs(buid1.LibraryId));

            var buid2 = new Buid(Guid.NewGuid(), BloomEntity.Song, Guid.NewGuid());
            var tab2 = Tab.Create(ProcessType.Browser, user, buid2, 3, TabType.Album, "Header");
            
            Assert.IsFalse(tabbedApplicationState.HasLibraryTabs(buid2.LibraryId));

            tabbedApplicationState.Tabs.Add(tab2);
            Assert.IsTrue(tabbedApplicationState.HasLibraryTabs(buid2.LibraryId));

            var libraryIds = new List<Guid>
            {
                buid1.LibraryId,
                buid2.LibraryId
            };

            tabbedApplicationState = new TestTabbedApplicationState { Tabs = new List<Tab>() };
            Assert.IsFalse(tabbedApplicationState.HasLibraryTabs(buid1.LibraryId));
            Assert.IsFalse(tabbedApplicationState.HasLibraryTabs(buid2.LibraryId));
            
            var tab3 = Tab.Create(ProcessType.Browser, user, libraryIds, 4, TabType.Home, "Header");
            tabbedApplicationState.Tabs.Add(tab3);
            Assert.IsTrue(tabbedApplicationState.HasLibraryTabs(buid1.LibraryId));
            Assert.IsTrue(tabbedApplicationState.HasLibraryTabs(buid2.LibraryId));

            tabbedApplicationState = new TestTabbedApplicationState { Tabs = new List<Tab>() };
            Assert.IsFalse(tabbedApplicationState.HasLibraryTabs(buid1.LibraryId));
            Assert.IsFalse(tabbedApplicationState.HasLibraryTabs(buid2.LibraryId));

            var tab4 = Tab.Create(ProcessType.Browser, user, libraryIds, 5, TabType.NewMusic, "Header", "Grid");
            tabbedApplicationState.Tabs.Add(tab4);
            Assert.IsTrue(tabbedApplicationState.HasLibraryTabs(buid1.LibraryId));
            Assert.IsTrue(tabbedApplicationState.HasLibraryTabs(buid2.LibraryId));
        }

        /// <summary>
        /// Tests the get next tab order method.
        /// </summary>
        [Test]
        public void GetNextTabOrderTest()
        {
            var person = Person.Create("Person Name");
            var user = User.Create(person);
            var tabbedApplicationState = new TestTabbedApplicationState { Tabs = new List<Tab>() };

            var buid1 = new Buid(Guid.NewGuid(), BloomEntity.Song, Guid.NewGuid());
            var nextOrder1 = tabbedApplicationState.GetNextTabOrder();
            Assert.AreEqual(1, nextOrder1);

            var tab1 = Tab.Create(ProcessType.Browser, user, buid1, nextOrder1, TabType.Song, "Header", "Grid");
            tabbedApplicationState.Tabs.Add(tab1);

            var nextOrder2 = tabbedApplicationState.GetNextTabOrder();
            Assert.AreEqual(2, nextOrder2);

            var buid2 = new Buid(Guid.NewGuid(), BloomEntity.Song, Guid.NewGuid());
            var tab2 = Tab.Create(ProcessType.Browser, user, buid2, nextOrder2, TabType.Album, "Header");

            tabbedApplicationState.Tabs.Add(tab2);
            Assert.AreEqual(3, tabbedApplicationState.GetNextTabOrder());

            tabbedApplicationState.Tabs.Remove(tab2);
            Assert.AreEqual(2, tabbedApplicationState.GetNextTabOrder());
        }

        /// <summary>
        /// Tests the get next tab order method.
        /// </summary>
        [Test]
        public void CondenseTabOrdersTest()
        {
            var person = Person.Create("Person Name");
            var user = User.Create(person);
            var tabbedApplicationState = new TestTabbedApplicationState { Tabs = new List<Tab>() };

            var buid1 = new Buid(Guid.NewGuid(), BloomEntity.Song, Guid.NewGuid());
            var tab1 = Tab.Create(ProcessType.Browser, user, buid1, 1, TabType.Song, "Header", "Grid");
            tabbedApplicationState.Tabs.Add(tab1);

            var buid2 = new Buid(Guid.NewGuid(), BloomEntity.Album, Guid.NewGuid());
            var tab2 = Tab.Create(ProcessType.Browser, user, buid2, 3, TabType.Album, "Header");
            tabbedApplicationState.Tabs.Add(tab2);

            var buid3 = new Buid(Guid.NewGuid(), BloomEntity.Artist, Guid.NewGuid());
            var tab3 = Tab.Create(ProcessType.Browser, user, buid3, 5, TabType.Artist, "Header");
            tabbedApplicationState.Tabs.Add(tab3);

            tabbedApplicationState.CondenseTabOrders();

            Assert.AreEqual(1, tabbedApplicationState.Tabs[0].Order);
            Assert.AreEqual(2, tabbedApplicationState.Tabs[1].Order);
            Assert.AreEqual(3, tabbedApplicationState.Tabs[2].Order);
        }
    }
}
