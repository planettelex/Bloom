using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the tag repository class.
    /// </summary>
    [TestFixture]
    public class TagRepositoryTests
    {
        private const string TestFileName = "TagRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ITagRepository _tagRepository;
        private ISongRepository _songRepository;
        private IArtistRepository _artistRepository;
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private IAlbumRepository _albumRepository;
        private IPlaylistRepository _playlistRepository;
        private Guid _roadTrip2016Id;
        private Guid _cousinFranksWeddingId;
        private Guid _standUpComedyId;
        private Song _youShookMe;
        private Song _dazedAndConfused;
        private Song _wholeLottaLove;
        private Song _blackSabbathSong;
        private Song _candyBars;
        private Album _ledZeppelin;
        private Album _ledZeppelinII;
        private Album _blackSabbathAlbum;
        private Album _mitchAllTogether;
        private Playlist _playlist;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            var photoRepository = new PhotoRespository();
            _roleRepository = new RoleRepository();
            _personRepository = new PersonRepository(photoRepository);
            _artistRepository = new ArtistRepository(_roleRepository, photoRepository, _personRepository);
            _songRepository = new SongRepository(_roleRepository, _personRepository);
            _albumRepository = new AlbumRepository(_roleRepository, _personRepository);
            _playlistRepository = new PlaylistRepository(_personRepository);
            _tagRepository = new TagRepository();

            var testFolder = Settings.TestsDataPath;
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);

            var testFilePath = Path.Combine(testFolder, TestFileName);
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            _dataSource.Create(testFilePath);
            PopulateDataSource();
        }

        /// <summary>
        /// Populates the data source.
        /// </summary>
        private void PopulateDataSource()
        {
            var ledZeppelin = Artist.Create("Led Zeppelin");
            _artistRepository.AddArtist(_dataSource, ledZeppelin);

            _youShookMe = Song.Create("You Shook Me", ledZeppelin);
            _songRepository.AddSong(_dataSource, _youShookMe);
            _dazedAndConfused = Song.Create("Dazed and Confused", ledZeppelin);
            _songRepository.AddSong(_dataSource, _dazedAndConfused);

            _ledZeppelin = Album.Create("Led Zeppelin", ledZeppelin);
            _albumRepository.AddAlbum(_dataSource, _ledZeppelin);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(_ledZeppelin, _youShookMe, 3));
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(_ledZeppelin, _dazedAndConfused, 4));

            _wholeLottaLove = Song.Create("Whole Lotta Love", ledZeppelin);
            _songRepository.AddSong(_dataSource, _wholeLottaLove);

            _ledZeppelinII = Album.Create("Led Zeppelin II", ledZeppelin);
            _albumRepository.AddAlbum(_dataSource, _ledZeppelinII);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(_ledZeppelinII, _wholeLottaLove, 1));

            var blackSabbath = Artist.Create("Black Sabbath");
            _artistRepository.AddArtist(_dataSource, blackSabbath);

            _blackSabbathSong = Song.Create("Black Sabbath", blackSabbath);
            _songRepository.AddSong(_dataSource, _blackSabbathSong);

            _blackSabbathAlbum = Album.Create("Black Sabbath", blackSabbath);
            _albumRepository.AddAlbum(_dataSource, _blackSabbathAlbum);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(_blackSabbathAlbum, _blackSabbathSong, 1));

            var mitchHedberg = Artist.Create("Mitch Hedberg");
            _artistRepository.AddArtist(_dataSource, mitchHedberg);

            _candyBars = Song.Create("Candy Bars", mitchHedberg);
            _songRepository.AddSong(_dataSource, _candyBars);

            _mitchAllTogether = Album.Create("Mitch All Together", mitchHedberg);
            _albumRepository.AddAlbum(_dataSource, _mitchAllTogether);
            _albumRepository.AddAlbumTrack(_dataSource, AlbumTrack.Create(_mitchAllTogether, _candyBars, 5));

            var johnDoe = Person.Create("John Doe");
            _playlist = Playlist.Create("Playlist", johnDoe);
            _playlistRepository.AddPlaylist(_dataSource, _playlist);
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(_playlist, _wholeLottaLove, 1));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(_playlist, _blackSabbathSong, 2));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(_playlist, _dazedAndConfused, 3));

            var roadTrip2016 = Tag.Create("Road Trip 2016");
            _roadTrip2016Id = roadTrip2016.Id;
            _tagRepository.AddTag(_dataSource, roadTrip2016);
            var cousinFranksWedding = Tag.Create("Cousin Frank's Wedding");
            _cousinFranksWeddingId = cousinFranksWedding.Id;
            _tagRepository.AddTag(_dataSource, cousinFranksWedding);
            var standUpComedy = Tag.Create("Stand-Up Comedy");
            _standUpComedyId = standUpComedy.Id;
            _tagRepository.AddTag(_dataSource, standUpComedy);

            _tagRepository.AddTagTo(_dataSource, roadTrip2016, _wholeLottaLove);
            _tagRepository.AddTagTo(_dataSource, roadTrip2016, _youShookMe);
            _tagRepository.AddTagTo(_dataSource, roadTrip2016, _ledZeppelin);
            _tagRepository.AddTagTo(_dataSource, roadTrip2016, _ledZeppelinII);
            _tagRepository.AddTagTo(_dataSource, roadTrip2016, _blackSabbathAlbum);
            _tagRepository.AddTagTo(_dataSource, roadTrip2016, _playlist);

            _tagRepository.AddTagTo(_dataSource, cousinFranksWedding, _wholeLottaLove);
            _tagRepository.AddTagTo(_dataSource, cousinFranksWedding, _ledZeppelin);
            _tagRepository.AddTagTo(_dataSource, cousinFranksWedding, _ledZeppelinII);
            _tagRepository.AddTagTo(_dataSource, cousinFranksWedding, _playlist);

            _tagRepository.AddTagTo(_dataSource, standUpComedy, _mitchAllTogether);
        }

        /// <summary>
        /// Tests the get tag method.
        /// </summary>
        [Test]
        public void GetTagTest()
        {
            var roadTrip2016 = _tagRepository.GetTag(_dataSource, _roadTrip2016Id);
            Assert.NotNull(roadTrip2016);
            Assert.AreEqual(_roadTrip2016Id, roadTrip2016.Id);
            Assert.AreEqual("Road Trip 2016", roadTrip2016.Name);
        }

        /// <summary>
        /// Tests the list tags method.
        /// </summary>
        [Test]
        public void ListTagsTest()
        {
            var tags = _tagRepository.ListTags(_dataSource);
            Assert.NotNull(tags);
            Assert.AreEqual(3, tags.Count);
            Assert.AreEqual(_cousinFranksWeddingId, tags[0].Id);
            Assert.AreEqual("Cousin Frank's Wedding", tags[0].Name);
            Assert.AreEqual(_roadTrip2016Id, tags[1].Id);
            Assert.AreEqual("Road Trip 2016", tags[1].Name);
            Assert.AreEqual(_standUpComedyId, tags[2].Id);
            Assert.AreEqual("Stand-Up Comedy", tags[2].Name);
        }

        /// <summary>
        /// Tests the list tags for song method.
        /// </summary>
        [Test]
        public void ListTagsForSongTest()
        {
            var tags = _tagRepository.ListTags(_dataSource, _wholeLottaLove);
            Assert.NotNull(tags);
            Assert.AreEqual(2, tags.Count);
            Assert.AreEqual(_cousinFranksWeddingId, tags[0].Id);
            Assert.AreEqual("Cousin Frank's Wedding", tags[0].Name);
            Assert.AreEqual(_roadTrip2016Id, tags[1].Id);
            Assert.AreEqual("Road Trip 2016", tags[1].Name);
        }

        /// <summary>
        /// Tests the list tags for album method.
        /// </summary>
        [Test]
        public void ListTagsForAlbumTest()
        {
            var tags = _tagRepository.ListTags(_dataSource, _ledZeppelin);
            Assert.NotNull(tags);
            Assert.AreEqual(2, tags.Count);
            Assert.AreEqual(_cousinFranksWeddingId, tags[0].Id);
            Assert.AreEqual("Cousin Frank's Wedding", tags[0].Name);
            Assert.AreEqual(_roadTrip2016Id, tags[1].Id);
            Assert.AreEqual("Road Trip 2016", tags[1].Name);
        }

        /// <summary>
        /// Tests the list tags for playlist method.
        /// </summary>
        [Test]
        public void ListTagsForPlaylistTest()
        {
            var tags = _tagRepository.ListTags(_dataSource, _playlist);
            Assert.NotNull(tags);
            Assert.AreEqual(2, tags.Count);
            Assert.AreEqual(_cousinFranksWeddingId, tags[0].Id);
            Assert.AreEqual("Cousin Frank's Wedding", tags[0].Name);
            Assert.AreEqual(_roadTrip2016Id, tags[1].Id);
            Assert.AreEqual("Road Trip 2016", tags[1].Name);
        }

        /// <summary>
        /// Tests the delete tag from song method.
        /// </summary>
        [Test]
        public void DeleteTagFromSongTest()
        {
            var standupComedy = _tagRepository.GetTag(_dataSource, _standUpComedyId);
            Assert.NotNull(standupComedy);
            var tags = _tagRepository.ListTags(_dataSource, _wholeLottaLove);
            Assert.NotNull(tags);
            Assert.AreEqual(2, tags.Count);

            _tagRepository.AddTagTo(_dataSource, standupComedy, _wholeLottaLove);

            tags = _tagRepository.ListTags(_dataSource, _wholeLottaLove);
            Assert.AreEqual(3, tags.Count);
            Assert.AreEqual(_standUpComedyId, tags[2].Id);

            _tagRepository.DeleteTagFrom(_dataSource, standupComedy, _wholeLottaLove);

            tags = _tagRepository.ListTags(_dataSource, _wholeLottaLove);
            Assert.AreEqual(2, tags.Count);
            Assert.AreEqual(_roadTrip2016Id, tags[1].Id);
        }

        /// <summary>
        /// Tests the delete tag from album method.
        /// </summary>
        [Test]
        public void DeleteTagFromAlbumTest()
        {
            var cousinFranksWedding = _tagRepository.GetTag(_dataSource, _cousinFranksWeddingId);
            Assert.NotNull(cousinFranksWedding);
            var tags = _tagRepository.ListTags(_dataSource, _blackSabbathAlbum);
            Assert.NotNull(tags);
            Assert.AreEqual(1, tags.Count);

            _tagRepository.AddTagTo(_dataSource, cousinFranksWedding, _blackSabbathAlbum);

            tags = _tagRepository.ListTags(_dataSource, _blackSabbathAlbum);
            Assert.AreEqual(2, tags.Count);
            Assert.AreEqual(_cousinFranksWeddingId, tags[0].Id);

            _tagRepository.DeleteTagFrom(_dataSource, cousinFranksWedding, _blackSabbathAlbum);

            tags = _tagRepository.ListTags(_dataSource, _blackSabbathAlbum);
            Assert.AreEqual(1, tags.Count);
            Assert.AreEqual(_roadTrip2016Id, tags[0].Id);
        }

        /// <summary>
        /// Tests the delete tag from playlist method.
        /// </summary>
        [Test]
        public void DeleteTagFromPlaylistTest()
        {
            var standupComedy = _tagRepository.GetTag(_dataSource, _standUpComedyId);
            Assert.NotNull(standupComedy);
            var tags = _tagRepository.ListTags(_dataSource, _playlist);
            Assert.NotNull(tags);
            Assert.AreEqual(2, tags.Count);

            _tagRepository.AddTagTo(_dataSource, standupComedy, _playlist);

            tags = _tagRepository.ListTags(_dataSource, _playlist);
            Assert.AreEqual(3, tags.Count);
            Assert.AreEqual(_standUpComedyId, tags[2].Id);

            _tagRepository.DeleteTagFrom(_dataSource, standupComedy, _playlist);

            tags = _tagRepository.ListTags(_dataSource, _playlist);
            Assert.AreEqual(2, tags.Count);
            Assert.AreEqual(_roadTrip2016Id, tags[1].Id);
        }

        /// <summary>
        /// Tests the delete tag method.
        /// </summary>
        [Test]
        public void DeleteTagTest()
        {
            var weird = Tag.Create("Weird");
            _tagRepository.AddTag(_dataSource, weird);
            _tagRepository.AddTagTo(_dataSource, weird, _wholeLottaLove);
            _tagRepository.AddTagTo(_dataSource, weird, _blackSabbathSong);
            _tagRepository.AddTagTo(_dataSource, weird, _ledZeppelinII);
            _tagRepository.AddTagTo(_dataSource, weird, _mitchAllTogether);
            _tagRepository.AddTagTo(_dataSource, weird, _playlist);

            weird = _tagRepository.GetTag(_dataSource, weird.Id);
            Assert.NotNull(weird);

            _tagRepository.DeleteTag(_dataSource, weird);

            weird = _tagRepository.GetTag(_dataSource, weird.Id);
            Assert.IsNull(weird);
        }
    }
}
