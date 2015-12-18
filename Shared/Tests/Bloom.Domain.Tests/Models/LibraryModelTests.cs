using System;
using Bloom.Domain.Enums;
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
        private const string ArtistName = "Test Artist";
        private const string AlbumName = "Test Album";
        private const string SongName = "Test Song";

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
            Assert.AreEqual(library.FilePath, library.FolderPath + "\\" + library.Name + Common.Settings.LibraryFileExtension);
        }
    }
}