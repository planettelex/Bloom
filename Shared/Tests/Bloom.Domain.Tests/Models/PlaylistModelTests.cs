using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the photo model class.
    /// </summary>
    [TestFixture]
    public class PlaylistModelTests
    {
        private const string LibraryOwnerName = "Owner Name";
        private const string PlaylistName = "Test Playlist";

        /// <summary>
        /// Tests the playlist create method.
        /// </summary>
        [Test]
        public void CreatePlaylistTest()
        {
            var owner = Person.Create(LibraryOwnerName);
            var playlist = Playlist.Create(PlaylistName, owner);

            Assert.AreNotEqual(playlist.Id, Guid.Empty);
            Assert.AreEqual(playlist.Name, PlaylistName);
            Assert.AreEqual(playlist.CreatedById, owner.Id);
            Assert.AreEqual(playlist.CreatedBy.Name, owner.Name);
        }
    }
}