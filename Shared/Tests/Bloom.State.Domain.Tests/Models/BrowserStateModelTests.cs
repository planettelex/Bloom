using System;
using System.Windows;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using NUnit.Framework;

namespace Bloom.State.Domain.Tests.Models
{
    /// <summary>
    /// Tests the browser application state model class.
    /// </summary>
    [TestFixture]
    public class BrowserStateModelTests
    {
        /// <summary>
        /// Tests the class constructor.
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            var state = new BrowserState();

            Assert.AreEqual("Bloom.Browser", state.ProcessName);
            Assert.NotNull(state.SkinName);
            Assert.Greater(state.SkinName.Length, 1);
            Assert.Greater(state.SidebarWidth, 0);
            Assert.AreNotEqual(WindowState.Minimized , state.WindowState);
            Assert.NotNull(state.Connections);
            Assert.NotNull(state.Tabs);
            Assert.AreEqual(Guid.Empty, state.SelectedTabId);
        }

        /// <summary>
        /// Tests the sidebar width property and reset method.
        /// </summary>
        [Test]
        public void SidebarWidthTest()
        {
            var state = new BrowserState();
            Assert.Greater(state.SidebarWidth, 0);
            var originalSidebarWidth = state.SidebarWidth;

            state.SidebarWidth = 5000;
            Assert.AreEqual(5000, state.SidebarWidth);

            state.ResetSidebarWidth();
            Assert.AreEqual(originalSidebarWidth, state.SidebarWidth);
        }

        /// <summary>
        /// Tests the set user method.
        /// </summary>
        [Test]
        public void SetUserTest()
        {
            var state = new BrowserState();
            Assert.IsNull(state.User);

            var person1 = Person.Create("Person One");
            var user1 = User.Create(person1);
            state.SetUser(user1);

            Assert.IsNotNull(state.User);
            Assert.AreEqual(person1.Id, state.UserId);
            Assert.AreEqual(person1.Id, state.User.PersonId);
            Assert.AreEqual("Person One", state.User.Name);

            var person2 = Person.Create("Person Two");
            var user2 = User.Create(person2);
            state.SetUser(user2);

            Assert.AreEqual(person2.Id, state.UserId);
            Assert.AreEqual(person2.Id, state.User.PersonId);
            Assert.AreEqual("Person Two", state.User.Name);

            state.SetUser(null);
            Assert.IsNull(state.User);
            Assert.AreEqual(Guid.Empty, state.UserId);
        }
    }
}
