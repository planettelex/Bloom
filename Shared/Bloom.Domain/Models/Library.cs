using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Bloom.Common;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music library.
    /// </summary>
    [Table(Name = "library")]
    public class Library
    {
        /// <summary>
        /// Creates a new library instance.
        /// </summary>
        /// <param name="owner">The library owner.</param>
        /// <param name="name">The library name.</param>
        /// <param name="folderPath">The library folder path.</param>
        public static Library Create(Person owner, string name, string folderPath)
        {
            return new Library
            {
                Id = Guid.NewGuid(),
                Name = name,
                FolderPath = folderPath,
                FileName = name + Settings.LibraryFileExtension,
                OwnerId = owner.Id,
                Owner = owner
            };
        }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the library name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the folder path.
        /// </summary>
        [Column(Name = "folder_path")]
        public string FolderPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        [Column(Name = "file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        [Column(Name = "owner_id")]
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the identifer owner.
        /// </summary>
        public Person Owner { get; set; }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string FilePath
        {
            get
            {
                return FolderPath.TrimEnd('\\') + "\\" + FileName;
            }
        }

        /// <summary>
        /// Gets or sets the library albums.
        /// </summary>
        public List<LibraryAlbum> Albums { get; set; }

        #region AddAlbum

        /// <summary>
        /// Creates and adds a library album to this library.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <returns>A new library album.</returns>
        /// <exception cref="System.ArgumentNullException">album</exception>
        public LibraryAlbum AddAlbum(Album album)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            if (Albums == null)
                Albums = new List<LibraryAlbum>();

            var libraryAlbum = LibraryAlbum.Create(this, album);
            Albums.Add(libraryAlbum);

            return libraryAlbum;
        }

        /// <summary>
        /// Creates and adds a library album to this library.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="mediaType">The type of media.</param>
        /// <returns>A new library album.</returns>
        /// <exception cref="System.ArgumentNullException">album</exception>
        public LibraryAlbum AddAlbum(Album album, MediaTypes mediaType)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            if (Albums == null)
                Albums = new List<LibraryAlbum>();

            var libraryAlbum = LibraryAlbum.Create(this, album);
            libraryAlbum.AddMedia(mediaType);
            Albums.Add(libraryAlbum);

            return libraryAlbum;
        }

        /// <summary>
        /// Creates and adds a library album to this library.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="mediaType">The type of media.</param>
        /// <param name="release">The album release.</param>
        /// <returns>A new library album.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">mediaType</exception>
        /// <exception cref="System.ArgumentNullException">album</exception>
        public LibraryAlbum AddAlbum(Album album, MediaTypes mediaType, AlbumRelease release)
        {
            if (release == null)
                return AddAlbum(album, mediaType);

            if (!release.MediaTypes.HasFlag(mediaType))
                throw new ArgumentOutOfRangeException("mediaType");

            if (album == null)
                throw new ArgumentNullException("album");

            if (Albums == null)
                Albums = new List<LibraryAlbum>();

            var libraryAlbum = LibraryAlbum.Create(this, album);
            libraryAlbum.AddMedia(mediaType, release);
            Albums.Add(libraryAlbum);

            return libraryAlbum;
        }

        #endregion

        /// <summary>
        /// Gets or sets the library artists.
        /// </summary>
        public List<LibraryArtist> Artists { get; set; }

        #region AddArtist

        /// <summary>
        /// Creates and adds a library artist to this library.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>A new library artist.</returns>
        /// <exception cref="System.ArgumentNullException">artist</exception>
        public LibraryArtist AddArtist(Artist artist)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");

            if (Artists == null)
                Artists = new List<LibraryArtist>();

            var libraryArtist = LibraryArtist.Create(this, artist);
            Artists.Add(libraryArtist);

            return libraryArtist;
        }

        #endregion

        /// <summary>
        /// Gets or sets the library people.
        /// </summary>
        public List<LibraryPerson> People { get; set; }

        #region AddPerson

        /// <summary>
        /// Creates and adds a library person to this library.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns>A new library person.</returns>
        /// <exception cref="System.ArgumentNullException">person</exception>
        public LibraryPerson AddPerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (People == null)
                People = new List<LibraryPerson>();

            var libraryPerson = LibraryPerson.Create(this, person);
            People.Add(libraryPerson);

            return libraryPerson;
        }

        #endregion

        /// <summary>
        /// Gets or sets the library playlists.
        /// </summary>
        public List<LibraryPlaylist> Playlists { get; set; }

        #region AddPlaylist

        /// <summary>
        /// Creates and adds a library playlist to this library.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        /// <returns>A new library playlist.</returns>
        /// <exception cref="System.ArgumentNullException">playlist</exception>
        public LibraryPlaylist AddPlaylist(Playlist playlist)
        {
            if (playlist == null)
                throw new ArgumentNullException("playlist");

            if (Playlists == null)
                Playlists = new List<LibraryPlaylist>();

            var libraryPlaylist = LibraryPlaylist.Create(this, playlist);
            Playlists.Add(libraryPlaylist);

            return libraryPlaylist;
        }

        #endregion

        /// <summary>
        /// Gets or sets the library songs.
        /// </summary>
        public List<LibrarySong> Songs { get; set; }

        #region AddSong

        /// <summary>
        /// Creates and adds a library song to this library.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <returns>A new library song. </returns>
        /// <exception cref="System.ArgumentNullException">song</exception>
        public LibrarySong AddSong(Song song)
        {
            if (song == null)
                throw new ArgumentNullException("song");

            if (Songs == null)
                Songs = new List<LibrarySong>();

            var librarySong = LibrarySong.Create(this, song);
            Songs.Add(librarySong);

            return librarySong;
        }

        /// <summary>
        /// Creates and adds a library song to this library.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="mediaType">The type of media.</param>
        /// <param name="uri">The media URI.</param>
        /// <returns>A new library song.</returns>
        /// <exception cref="System.ArgumentNullException">song</exception>
        /// <exception cref="System.ArgumentNullException">uri</exception>
        public LibrarySong AddSong(Song song, MediaTypes mediaType, string uri)
        {
            if (song == null)
                throw new ArgumentNullException("song");

            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentNullException("uri");

            if (Songs == null)
                Songs = new List<LibrarySong>();

            var librarySong = LibrarySong.Create(this, song);
            librarySong.AddMedia(mediaType, uri);
            Songs.Add(librarySong);

            return librarySong;
        }

        /// <summary>
        /// Creates and adds a library song to this library.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="digitalFormat">The digital format.</param>
        /// <param name="uri">The media URI.</param>
        /// <returns>A new library song.</returns>
        /// <exception cref="System.ArgumentNullException">song</exception>
        /// <exception cref="System.ArgumentNullException">uri</exception>
        public LibrarySong AddSong(Song song, DigitalFormats digitalFormat, string uri)
        {
            if (song == null)
                throw new ArgumentNullException("song");

            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentNullException("uri");

            if (Songs == null)
                Songs = new List<LibrarySong>();

            var librarySong = LibrarySong.Create(this, song);
            librarySong.AddMedia(MediaTypes.Digital, digitalFormat, uri);
            Songs.Add(librarySong);

            return librarySong;
        }

        #endregion
    }
}
