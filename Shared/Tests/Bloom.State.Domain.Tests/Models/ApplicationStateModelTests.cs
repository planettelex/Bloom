using System;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using NUnit.Framework;

namespace Bloom.State.Domain.Tests.Models
{
    /// <summary>
    /// Tests the application state model class.
    /// </summary>
    [TestFixture]
    public class ApplicationStateModelTests
    {
        public class TestApplicationState : ApplicationState { }

        /// <summary>
        /// Tests the set user method.
        /// </summary>
        [Test]
        public void SetUserTest()
        {
            var before = DateTime.Now;
            var person = Person.Create("Person Name");
            var user = User.Create(person);
            var state = new TestApplicationState();
            state.SetUser(user);
            var after = DateTime.Now;

            Assert.IsNotNull(state.User);
            Assert.AreEqual(person.Id, state.User.PersonId);
            Assert.AreEqual("Person Name", state.User.Name);
            Assert.LessOrEqual(before, state.User.LastLogin);
            Assert.GreaterOrEqual(after, state.User.LastLogin);
        }

        /// <summary>
        /// Tests the library connection methods.
        /// </summary>
        [Test]
        public void LibraryConnectionTests()
        {
            var state = new TestApplicationState();
            Assert.IsNull(state.Connections);

            var owner = Person.Create("Owner Name");
            var library1 = Library.Create(owner, "Test Library 1", "c:\\library1.blm");
            var libraryConnection1 = LibraryConnection.Create(library1);
            var library2 = Library.Create(owner, "Test Library 2", "c:\\library2.blm");
            var libraryConnection2 = LibraryConnection.Create(library2);

            state.AddConnection(libraryConnection1);
            Assert.IsNotNull(state.Connections);
            Assert.AreEqual(1, state.Connections.Count);

            state.AddConnection(libraryConnection2);
            Assert.AreEqual(2, state.Connections.Count);

            var connection = state.GetConnection(library1.Id);
            Assert.IsNotNull(connection);
            Assert.AreEqual(library1.Id, connection.LibraryId);
            Assert.IsNotNull(connection.Library);
            Assert.AreEqual("Test Library 1", connection.LibraryName);
            Assert.AreEqual("Test Library 1", connection.Library.Name);

            state.RemoveConnection(libraryConnection1);
            var noConnection = state.GetConnection(library1.Id);
            Assert.IsNull(noConnection);
            Assert.AreEqual(1, state.Connections.Count);
        }
    }
}
