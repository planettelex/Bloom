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

        /// <summary>
        /// Tests adding a library album to a library.
        /// </summary>
        [Test]
        public void AddAlbumToLibraryTest()
        {
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var album = Album.Create(AlbumName);
            var libraryAlbum = library.AddAlbum(album);

            Assert.AreEqual(library.Albums.Count, 1);
            Assert.AreEqual(libraryAlbum.LibraryId, library.Id);
            Assert.AreEqual(libraryAlbum.AlbumId, album.Id);
            Assert.AreEqual(libraryAlbum.Album.Name, AlbumName);
        }

        /// <summary>
        /// Tests adding a library album with a media type to a library.
        /// </summary>
        [Test]
        public void AddAlbumWithMediaTypeToLibraryTest()
        {
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var album = Album.Create(AlbumName);
            var libraryAlbum = library.AddAlbum(album, MediaTypes.CD);

            Assert.AreEqual(library.Albums.Count, 1);
            Assert.AreEqual(libraryAlbum.LibraryId, library.Id);
            Assert.AreEqual(libraryAlbum.AlbumId, album.Id);
            Assert.AreEqual(libraryAlbum.Album.Name, AlbumName);
            Assert.AreEqual(libraryAlbum.Media.Count, 1);
            Assert.AreEqual(libraryAlbum.Media[0].LibraryId, library.Id);
            Assert.AreEqual(libraryAlbum.Media[0].AlbumId, album.Id);
            Assert.AreEqual(libraryAlbum.Media[0].Album.Name, AlbumName);
            Assert.AreEqual(libraryAlbum.Media[0].MediaType, MediaTypes.CD);
        }

        /// <summary>
        /// Tests adding a library album with a media type and album release to a library.
        /// </summary>
        [Test]
        public void AddAlbumWithMediaTypeAndReleaseToLibraryTest()
        {
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var album = Album.Create(AlbumName);
            var releaseDate = DateTime.Now.AddDays(-900);
            var release = album.AddRelease(releaseDate, MediaTypes.CD | MediaTypes.Digital);
            var libraryAlbum = library.AddAlbum(album, MediaTypes.CD, release);

            Assert.AreEqual(library.Albums.Count, 1);
            Assert.AreEqual(libraryAlbum.LibraryId, library.Id);
            Assert.AreEqual(libraryAlbum.AlbumId, album.Id);
            Assert.AreEqual(libraryAlbum.Album.Name, AlbumName);
            Assert.AreEqual(libraryAlbum.Media.Count, 1);
            Assert.AreEqual(libraryAlbum.Media[0].LibraryId, library.Id);
            Assert.AreEqual(libraryAlbum.Media[0].AlbumId, album.Id);
            Assert.AreEqual(libraryAlbum.Media[0].Album.Name, AlbumName);
            Assert.AreEqual(libraryAlbum.Media[0].MediaType, MediaTypes.CD);
        }

        /// <summary>
        /// Tests adding a library artist to a library.
        /// </summary>
        [Test]
        public void AddArtistToLibraryTest()
        {
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var artist = Artist.Create(ArtistName);
            var libraryArtist = library.AddArtist(artist);

            Assert.AreEqual(library.Artists.Count, 1);
            Assert.AreEqual(libraryArtist.LibraryId, library.Id);
            Assert.AreEqual(libraryArtist.ArtistId, artist.Id);
            Assert.AreEqual(libraryArtist.Artist.Name, ArtistName);
        }

        /// <summary>
        /// Tests adding a library person to a library.
        /// </summary>
        [Test]
        public void AddPersonToLibraryTest()
        {
            const string personName = "Test Person";
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var person = Person.Create(personName);
            var libraryPerson = library.AddPerson(person);

            Assert.AreEqual(library.People.Count, 1);
            Assert.AreEqual(libraryPerson.LibraryId, library.Id);
            Assert.AreEqual(libraryPerson.PersonId, person.Id);
            Assert.AreEqual(libraryPerson.Person.Name, personName);
        }

        /// <summary>
        /// Tests adding a library playlist to a library.
        /// </summary>
        [Test]
        public void AddPlaylistToLibraryTest()
        {
            const string playlistName = "Test Playlist";
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var playlist = Playlist.Create(playlistName, owner);
            var libraryPlaylist = library.AddPlaylist(playlist);

            Assert.AreEqual(library.Playlists.Count, 1);
            Assert.AreEqual(libraryPlaylist.LibraryId, library.Id);
            Assert.AreEqual(libraryPlaylist.PlaylistId, playlist.Id);
            Assert.AreEqual(libraryPlaylist.Playlist.Name, playlistName);
        }

        /// <summary>
        /// Tests adding a library song to a library.
        /// </summary>
        [Test]
        public void AddSongToLibraryTest()
        {
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var librarySong = library.AddSong(song);

            Assert.AreEqual(library.Songs.Count, 1);
            Assert.AreEqual(librarySong.LibraryId, library.Id);
            Assert.AreEqual(librarySong.SongId, song.Id);
            Assert.AreEqual(librarySong.Song.Name, SongName);
        }

        /// <summary>
        /// Tests adding a library song with a media type to a library.
        /// </summary>
        [Test]
        public void AddSongWithMediaTypeToLibraryTest()
        {
            const string uri = "blm://artist/album/song";
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var librarySong = library.AddSong(song, MediaTypes.CD, uri);

            Assert.AreEqual(library.Songs.Count, 1);
            Assert.AreEqual(librarySong.LibraryId, library.Id);
            Assert.AreEqual(librarySong.SongId, song.Id);
            Assert.AreEqual(librarySong.Song.Name, SongName);
            Assert.AreEqual(librarySong.Media.Count, 1);
            Assert.AreEqual(librarySong.Media[0].LibraryId, library.Id);
            Assert.AreEqual(librarySong.Media[0].SongId, song.Id);
            Assert.AreEqual(librarySong.Media[0].Song.Name, SongName);
            Assert.AreEqual(librarySong.Media[0].MediaType, MediaTypes.CD);
        }

        /// <summary>
        /// Tests adding a library song with a digital format to a library.
        /// </summary>
        [Test]
        public void AddSongWithMediaTypeAndDigitalFormatToLibraryTest()
        {
            const string uri = "blm://artist/album/song";
            var owner = Person.Create(LibraryOwnerName);
            var library = Library.Create(owner, LibraryName, LibraryFolderPath);
            var artist = Artist.Create(ArtistName);
            var song = Song.Create(SongName, artist);
            var librarySong = library.AddSong(song, DigitalFormats.MP3, uri);

            Assert.AreEqual(library.Songs.Count, 1);
            Assert.AreEqual(librarySong.LibraryId, library.Id);
            Assert.AreEqual(librarySong.SongId, song.Id);
            Assert.AreEqual(librarySong.Song.Name, SongName);
            Assert.AreEqual(librarySong.Media.Count, 1);
            Assert.AreEqual(librarySong.Media[0].LibraryId, library.Id);
            Assert.AreEqual(librarySong.Media[0].SongId, song.Id);
            Assert.AreEqual(librarySong.Media[0].Song.Name, SongName);
            Assert.AreEqual(librarySong.Media[0].MediaType, MediaTypes.Digital);
            Assert.AreEqual(librarySong.Media[0].DigitalFormat, DigitalFormats.MP3);
        }
    }
}