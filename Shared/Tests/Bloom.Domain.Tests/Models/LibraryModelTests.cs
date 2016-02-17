using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the library model class.
    /// </summary>
    [TestFixture]
    public class LibraryModelTests
    {
        private const string LibraryOwnerName = "Owner Name";
        private const string LibraryName = "Library";
        private const string LibraryFolderPath = "C:\\Music";

        /// <summary>
        /// Tests the library create method.
        /// </summary>
        [Test]
        public void CreateLibraryTest()
        {
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);

            Assert.AreNotEqual(library.Id, Guid.Empty);
            Assert.AreEqual(library.Name, LibraryName);
            Assert.AreEqual(library.FolderPath, LibraryFolderPath);
            Assert.AreEqual(library.OwnerId, owner.Id);
            Assert.AreEqual(library.Owner.Name, LibraryOwnerName);
            Assert.AreEqual(library.FileName, LibraryName + Common.Settings.LibraryFileExtension);
        }

        /// <summary>
        /// Tests the library properties.
        /// </summary>
        [Test]
        public void LibraryPropertiesTest()
        {
            var id = Guid.NewGuid();
            var now = DateTime.Now;
            var owner = Person.Create(LibraryOwnerName);
            var library = new Library
            {
                Id = id,
                Name = LibraryName,
                FileName = LibraryName + ".blm",
                FolderPath = LibraryFolderPath,
                Owner = owner,
                OwnerLastConnected = now
            };

            Assert.AreEqual(library.Id, id);
            Assert.AreEqual(library.Name, LibraryName);
            Assert.AreEqual(library.FilePath, LibraryFolderPath + "\\" + LibraryName + ".blm");
            Assert.AreEqual(library.OwnerLastConnected, now);
        }
    }
}