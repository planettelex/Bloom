using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the song model class.
    /// </summary>
    [TestFixture]
    public class SongModelTests
    {
        private const string ArtistName = "Test Artist";
        private const string SongName = "Test Song";

        /// <summary>
        /// Tests the song create method.
        /// </summary>
        [Test]
        public void CreateSongTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);

            Assert.AreNotEqual(song.Id, Guid.Empty);
            Assert.AreEqual(song.Name, SongName);
            Assert.AreEqual(song.ArtistId, artist.Id);
            Assert.AreEqual(song.Artist.Name, ArtistName);
        }

        /// <summary>
        /// Tests adding a collaborator to a song.
        /// </summary>
        [Test]
        public void AddCollaboratorToSongTest()
        {
            const string collaboratorName = "Test Collaborator";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var collaborator = Artist.Create(collaboratorName);
            // todo

            Assert.AreEqual(song.Collaborators.Count, 1);
        }

        /// <summary>
        /// Tests adding a credit to a song.
        /// </summary>
        [Test]
        public void AddCreditToSongTest()
        {
            const string personName = "Test Person";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var person = Person.Create(personName);
            // todo

            Assert.AreEqual(song.Credits.Count, 1);
        }

        /// <summary>
        /// Tests adding a credit with roles to a song.
        /// </summary>
        [Test]
        public void AddCreditWithRolesToSongTest()
        {
            const string personName = "Test Person";
            const string role1Name = "Producer";
            const string role2Name = "Engineer";
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var person = Person.Create(personName);
            // todo
            var role1 = Role.Create(role1Name);
            var role2 = Role.Create(role2Name);
            
        }

        /// <summary>
        /// Tests adding a segment to a song.
        /// </summary>
        [Test]
        public void AddSegmentToSongTest()
        {
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            song.Length = 100000;
            //todo

            //Assert.AreNotEqual(songSegment.Id, Guid.Empty);
            Assert.AreEqual(song.Segments.Count, 1);

        }

    }
}