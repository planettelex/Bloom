using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the mood repository class.
    /// </summary>
    [TestFixture]
    public class MoodRepositoryTests
    {
        private const string TestFileName = "MoodRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IMoodRepository _moodRepository;
        private ISongRepository _songRepository;
        private IArtistRepository _artistRepository;
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private IAlbumRepository _albumRepository;
        private IPlaylistRepository _playlistRepository;
        private Guid _excitedId;
        private Guid _broodingId;
        private Guid _happyId;
        private Song _youShookMe;
        private Song _dazedAndConfused;
        private Song _wholeLottaLove;
        private Song _blackSabbathSong;
        private Album _ledZeppelin;
        private Album _ledZeppelinII;
        private Album _blackSabbathAlbum;
        private Playlist _playlist;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
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
            _moodRepository = new MoodRepository();

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

            var johnDoe = Person.Create("John Doe");
            _playlist = Playlist.Create("Playlist", johnDoe);
            _playlistRepository.AddPlaylist(_dataSource, _playlist);
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(_playlist, _wholeLottaLove, 1));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(_playlist, _blackSabbathSong, 2));
            _playlistRepository.AddPlaylistTrack(_dataSource, PlaylistTrack.Create(_playlist, _dazedAndConfused, 3));

            var excited = Mood.Create("Excited");
            _excitedId = excited.Id;
            _moodRepository.AddMood(_dataSource, excited);
            var brooding = Mood.Create("Brooding");
            _broodingId = brooding.Id;
            _moodRepository.AddMood(_dataSource, brooding);
            var happy = Mood.Create("Happy");
            _happyId = happy.Id;
            _moodRepository.AddMood(_dataSource, happy);

            _moodRepository.AddMoodTo(_dataSource, excited, _wholeLottaLove);
            _moodRepository.AddMoodTo(_dataSource, excited, _youShookMe);
            _moodRepository.AddMoodTo(_dataSource, excited, _ledZeppelin);
            _moodRepository.AddMoodTo(_dataSource, excited, _ledZeppelinII);
            _moodRepository.AddMoodTo(_dataSource, excited, _playlist);

            _moodRepository.AddMoodTo(_dataSource, happy, _wholeLottaLove);
            _moodRepository.AddMoodTo(_dataSource, happy, _ledZeppelin);
            _moodRepository.AddMoodTo(_dataSource, happy, _ledZeppelinII);
            _moodRepository.AddMoodTo(_dataSource, happy, _playlist);

            _moodRepository.AddMoodTo(_dataSource, brooding, _dazedAndConfused);
            _moodRepository.AddMoodTo(_dataSource, brooding, _blackSabbathSong);
            _moodRepository.AddMoodTo(_dataSource, brooding, _blackSabbathAlbum);
        }

        /// <summary>
        /// Tests the get mood method.
        /// </summary>
        [Test]
        public void GetMoodTest()
        {
            var brooding = _moodRepository.GetMood(_dataSource, _broodingId);
            Assert.NotNull(brooding);
            Assert.AreEqual(_broodingId, brooding.Id);
            Assert.AreEqual("Brooding", brooding.Name);
        }

        /// <summary>
        /// Tests the list moods method.
        /// </summary>
        [Test]
        public void ListMoodsTest()
        {
            var moods = _moodRepository.ListMoods(_dataSource);
            Assert.NotNull(moods);
            Assert.AreEqual(3, moods.Count);
            Assert.AreEqual(_broodingId, moods[0].Id);
            Assert.AreEqual("Brooding", moods[0].Name);
            Assert.AreEqual(_excitedId, moods[1].Id);
            Assert.AreEqual("Excited", moods[1].Name);
            Assert.AreEqual(_happyId, moods[2].Id);
            Assert.AreEqual("Happy", moods[2].Name);
        }

        /// <summary>
        /// Tests the list moods for song method.
        /// </summary>
        [Test]
        public void ListMoodsForSongTest()
        {
            var moods = _moodRepository.ListMoods(_dataSource, _wholeLottaLove);
            Assert.NotNull(moods);
            Assert.AreEqual(2, moods.Count);
            Assert.AreEqual(_excitedId, moods[0].Id);
            Assert.AreEqual("Excited", moods[0].Name);
            Assert.AreEqual(_happyId, moods[1].Id);
            Assert.AreEqual("Happy", moods[1].Name);
        }

        /// <summary>
        /// Tests the list moods for album method.
        /// </summary>
        [Test]
        public void ListMoodsForAlbumTest()
        {
            var moods = _moodRepository.ListMoods(_dataSource, _ledZeppelin);
            Assert.NotNull(moods);
            Assert.AreEqual(2, moods.Count);
            Assert.AreEqual(_excitedId, moods[0].Id);
            Assert.AreEqual("Excited", moods[0].Name);
            Assert.AreEqual(_happyId, moods[1].Id);
            Assert.AreEqual("Happy", moods[1].Name);
        }

        /// <summary>
        /// Tests the list moods for playlist method.
        /// </summary>
        [Test]
        public void ListMoodsForPlaylistTest()
        {
            var moods = _moodRepository.ListMoods(_dataSource, _playlist);
            Assert.NotNull(moods);
            Assert.AreEqual(2, moods.Count);
            Assert.AreEqual(_excitedId, moods[0].Id);
            Assert.AreEqual("Excited", moods[0].Name);
            Assert.AreEqual(_happyId, moods[1].Id);
            Assert.AreEqual("Happy", moods[1].Name);
        }

        /// <summary>
        /// Tests the delete mood from song method.
        /// </summary>
        [Test]
        public void DeleteMoodFromSongTest()
        {
            var brooding = _moodRepository.GetMood(_dataSource, _broodingId);
            Assert.NotNull(brooding);
            var moods = _moodRepository.ListMoods(_dataSource, _wholeLottaLove);
            Assert.NotNull(moods);
            Assert.AreEqual(2, moods.Count);

            _moodRepository.AddMoodTo(_dataSource, brooding, _wholeLottaLove);

            moods = _moodRepository.ListMoods(_dataSource, _wholeLottaLove);
            Assert.AreEqual(3, moods.Count);
            Assert.AreEqual(_broodingId, moods[0].Id);

            _moodRepository.DeleteMoodFrom(_dataSource, brooding, _wholeLottaLove);

            moods = _moodRepository.ListMoods(_dataSource, _wholeLottaLove);
            Assert.AreEqual(2, moods.Count);
            Assert.AreEqual(_excitedId, moods[0].Id);
        }

        /// <summary>
        /// Tests the delete mood from album method.
        /// </summary>
        [Test]
        public void DeleteMoodFromAlbumTest()
        {
            var happy = _moodRepository.GetMood(_dataSource, _happyId);
            Assert.NotNull(happy);
            var moods = _moodRepository.ListMoods(_dataSource, _blackSabbathAlbum);
            Assert.NotNull(moods);
            Assert.AreEqual(1, moods.Count);

            _moodRepository.AddMoodTo(_dataSource, happy, _blackSabbathAlbum);

            moods = _moodRepository.ListMoods(_dataSource, _blackSabbathAlbum);
            Assert.AreEqual(2, moods.Count);
            Assert.AreEqual(_happyId, moods[1].Id);

            _moodRepository.DeleteMoodFrom(_dataSource, happy, _blackSabbathAlbum);

            moods = _moodRepository.ListMoods(_dataSource, _blackSabbathAlbum);
            Assert.AreEqual(1, moods.Count);
            Assert.AreEqual(_broodingId, moods[0].Id);
        }

        /// <summary>
        /// Tests the delete mood from playlist method.
        /// </summary>
        [Test]
        public void DeleteMoodFromPlaylistTest()
        {
            var brooding = _moodRepository.GetMood(_dataSource, _broodingId);
            Assert.NotNull(brooding);
            var moods = _moodRepository.ListMoods(_dataSource, _playlist);
            Assert.NotNull(moods);
            Assert.AreEqual(2, moods.Count);

            _moodRepository.AddMoodTo(_dataSource, brooding, _playlist);

            moods = _moodRepository.ListMoods(_dataSource, _playlist);
            Assert.AreEqual(3, moods.Count);
            Assert.AreEqual(_broodingId, moods[0].Id);

            _moodRepository.DeleteMoodFrom(_dataSource, brooding, _playlist);

            moods = _moodRepository.ListMoods(_dataSource, _playlist);
            Assert.AreEqual(2, moods.Count);
            Assert.AreEqual(_excitedId, moods[0].Id);
        }

        /// <summary>
        /// Tests the delete mood method.
        /// </summary>
        [Test]
        public void DeleteMoodTest()
        {
            var pumped = Mood.Create("Pumped");
            _moodRepository.AddMood(_dataSource, pumped);
            _moodRepository.AddMoodTo(_dataSource, pumped, _wholeLottaLove);
            _moodRepository.AddMoodTo(_dataSource, pumped, _blackSabbathSong);
            _moodRepository.AddMoodTo(_dataSource, pumped, _ledZeppelinII);
            _moodRepository.AddMoodTo(_dataSource, pumped, _blackSabbathAlbum);
            _moodRepository.AddMoodTo(_dataSource, pumped, _playlist);

            pumped = _moodRepository.GetMood(_dataSource, pumped.Id);
            Assert.NotNull(pumped);

            _moodRepository.DeleteMood(_dataSource, pumped);

            pumped = _moodRepository.GetMood(_dataSource, pumped.Id);
            Assert.IsNull(pumped);
        }
    }
}
