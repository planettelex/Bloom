using System;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using NUnit.Framework;

namespace Bloom.State.Domain.Tests.Models
{
    /// <summary>
    /// Tests the library connection model class.
    /// </summary>
    [TestFixture]
    public class LibraryConnectionModelTests
    {
        /// <summary>
        /// Tests the library connection create method.
        /// </summary>
        [Test]
        public void CreateLibraryConnectionTest()
        {
            var before = DateTime.Now;
            var owner = Person.Create("Person Name");
            var library = Library.Create(owner, "Test Library", "c:\\music\\libraries");
            var libraryConnection = LibraryConnection.Create(library);
            var after = DateTime.Now;

            Assert.AreEqual(libraryConnection.LibraryId, library.Id);
            Assert.AreEqual(libraryConnection.LibraryName, library.Name);
            Assert.AreEqual(libraryConnection.FilePath, library.FilePath);
            Assert.IsNotNull(libraryConnection.Library);
            Assert.AreEqual(libraryConnection.Library.Id, library.Id);
            Assert.AreEqual(libraryConnection.Library.Name, library.Name);
            Assert.AreEqual(libraryConnection.Library.FilePath, library.FilePath);
            Assert.AreEqual(libraryConnection.OwnerId, owner.Id);
            Assert.AreEqual(libraryConnection.OwnerName, "Person Name");
            Assert.AreEqual(libraryConnection.LastConnectionBy, owner.Id);
            Assert.LessOrEqual(before, libraryConnection.LastConnected);
            Assert.GreaterOrEqual(after, libraryConnection.LastConnected);
        }

        /// <summary>
        /// Tests the library connection properties.
        /// </summary>
        [Test]
        public void LibraryConnectionPropertiesTest()
        {
            var owner = Person.Create("Person Name");
            var library = Library.Create(owner, "Test Library", "c:\\music\\libraries");
            var libraryConnection = new LibraryConnection
            {
                Library = library,
                LibraryId = library.Id,
                LibraryName = library.Name,
                FilePath = library.FilePath,
                OwnerId = owner.Id,
                OwnerName = owner.Name
            };

            Assert.AreEqual(library.Id, libraryConnection.LibraryId);
            Assert.AreEqual("Test Library", libraryConnection.LibraryName);
            Assert.AreEqual("c:\\music\\libraries\\Test Library.blm", libraryConnection.FilePath);
            Assert.AreEqual(owner.Id, libraryConnection.OwnerId);
            Assert.AreEqual("Person Name", libraryConnection.OwnerName);
        }

        /// <summary>
        /// Tests the set owner method.
        /// </summary>
        [Test]
        public void SetOwnerTest()
        {
            var owner = Person.Create("Person Name");
            var library = Library.Create(owner, "Test Library", "c:\\music\\libraries");
            var libraryConnection = LibraryConnection.Create(library);

            var newOwner = Person.Create("New Owner");
            libraryConnection.SetOwner(newOwner);

            Assert.AreEqual(newOwner.Id, libraryConnection.OwnerId);
            Assert.AreEqual("New Owner", libraryConnection.OwnerName);
            Assert.AreEqual(newOwner.Id, libraryConnection.Library.OwnerId);
            Assert.AreEqual(newOwner.Id, libraryConnection.Library.Owner.Id);
            Assert.AreEqual("New Owner", libraryConnection.Library.Owner.Name);
        }
    }
}
