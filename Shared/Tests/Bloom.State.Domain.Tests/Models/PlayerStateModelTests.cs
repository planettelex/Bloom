using System;
using System.Windows;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using NUnit.Framework;

namespace Bloom.State.Domain.Tests.Models
{
    /// <summary>
    /// Tests the player application state model class.
    /// </summary>
    [TestFixture]
    public class PlayerStateModelTests
    {
        /// <summary>
        /// Tests the class constructor.
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            var state = new PlayerState();

            Assert.AreEqual("Bloom.Player", state.ProcessName);
            Assert.NotNull(state.SkinName);
            Assert.Greater(state.SkinName.Length, 1);
            Assert.Greater(state.RecentWidth, 0);
            Assert.Greater(state.UpcomingWidth, 0);
            Assert.AreNotEqual(WindowState.Minimized , state.WindowState);
            Assert.NotNull(state.Connections);
        }

        /// <summary>
        /// Tests the recent and upcoming width properties and reset methods.
        /// </summary>
        [Test]
        public void SidebarWidthTest()
        {
            var state = new PlayerState();
            Assert.Greater(state.RecentWidth, 0);
            Assert.Greater(state.UpcomingWidth, 0);
            var originalRecentWidth = state.RecentWidth;
            var originalUpcomingWidth = state.UpcomingWidth;

            state.RecentWidth = 3000;
            Assert.AreEqual(3000, state.RecentWidth);

            state.UpcomingWidth = 1500;
            Assert.AreEqual(1500, state.UpcomingWidth);

            state.ResetRecentWidth();
            Assert.AreEqual(originalRecentWidth, state.RecentWidth);

            state.ResetUpcomingWidth();
            Assert.AreEqual(originalUpcomingWidth, state.UpcomingWidth);
        }

        /// <summary>
        /// Tests the set user method.
        /// </summary>
        [Test]
        public void SetUserTest()
        {
            var state = new PlayerState();
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
