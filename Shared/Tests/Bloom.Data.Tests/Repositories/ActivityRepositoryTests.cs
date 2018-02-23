using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the activity repository class.
    /// </summary>
    [TestFixture]
    public class ActivityRepositoryTests
    {
        private const string TestFileName = "ActivityRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IActivityRepository _activityRepository;
        private ISongRepository _songRepository;
        private IArtistRepository _artistRepository;
        private IRoleRepository _roleRepository;
        private IPersonRepository _personRepository;
        private IAlbumRepository _albumRepository;
        private IPlaylistRepository _playlistRepository;
        private Guid _cleaningId;
        private Guid _workingOutId;
        private Guid _chillingId;
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
            _activityRepository = new ActivityRepository();

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

            var cleaning = Activity.Create("Cleaning");
            _cleaningId = cleaning.Id;
            _activityRepository.AddActivity(_dataSource, cleaning);
            var workingOut = Activity.Create("Working Out");
            _workingOutId = workingOut.Id;
            _activityRepository.AddActivity(_dataSource, workingOut);
            var chilling = Activity.Create("Chilling");
            _chillingId = chilling.Id;
            _activityRepository.AddActivity(_dataSource, chilling);

            _activityRepository.AddActivityTo(_dataSource, cleaning, _wholeLottaLove);
            _activityRepository.AddActivityTo(_dataSource, cleaning, _youShookMe);
            _activityRepository.AddActivityTo(_dataSource, cleaning, _ledZeppelin);
            _activityRepository.AddActivityTo(_dataSource, cleaning, _ledZeppelinII);
            _activityRepository.AddActivityTo(_dataSource, cleaning, _playlist);

            _activityRepository.AddActivityTo(_dataSource, workingOut, _wholeLottaLove);
            _activityRepository.AddActivityTo(_dataSource, workingOut, _blackSabbathSong);
            _activityRepository.AddActivityTo(_dataSource, workingOut, _ledZeppelin);
            _activityRepository.AddActivityTo(_dataSource, workingOut, _blackSabbathAlbum);
            _activityRepository.AddActivityTo(_dataSource, workingOut, _playlist);

            _activityRepository.AddActivityTo(_dataSource, chilling, _dazedAndConfused);
            _activityRepository.AddActivityTo(_dataSource, chilling, _blackSabbathSong);
        }

        /// <summary>
        /// Tests the get activity method.
        /// </summary>
        [Test]
        public void GetActivityTest()
        {
            var chilling = _activityRepository.GetActivity(_dataSource, _chillingId);
            Assert.NotNull(chilling);
            Assert.AreEqual(_chillingId, chilling.Id);
            Assert.AreEqual("Chilling", chilling.Name);
        }

        /// <summary>
        /// Tests the list activities method.
        /// </summary>
        [Test]
        public void ListActivitiesTest()
        {
            var activities = _activityRepository.ListActivities(_dataSource);
            Assert.NotNull(activities);
            Assert.AreEqual(3, activities.Count);
            Assert.AreEqual(_chillingId, activities[0].Id);
            Assert.AreEqual("Chilling", activities[0].Name);
            Assert.AreEqual(_cleaningId, activities[1].Id);
            Assert.AreEqual("Cleaning", activities[1].Name);
            Assert.AreEqual(_workingOutId, activities[2].Id);
            Assert.AreEqual("Working Out", activities[2].Name);
        }

        /// <summary>
        /// Tests the list activities for song method.
        /// </summary>
        [Test]
        public void ListActivitiesForSongTest()
        {
            var activities = _activityRepository.ListActivities(_dataSource, _wholeLottaLove);
            Assert.NotNull(activities);
            Assert.AreEqual(2, activities.Count);
            Assert.AreEqual(_cleaningId, activities[0].Id);
            Assert.AreEqual("Cleaning", activities[0].Name);
            Assert.AreEqual(_workingOutId, activities[1].Id);
            Assert.AreEqual("Working Out", activities[1].Name);
        }

        /// <summary>
        /// Tests the list activities for album method.
        /// </summary>
        [Test]
        public void ListActivitiesForAlbumTest()
        {
            var activities = _activityRepository.ListActivities(_dataSource, _ledZeppelin);
            Assert.NotNull(activities);
            Assert.AreEqual(2, activities.Count);
            Assert.AreEqual(_cleaningId, activities[0].Id);
            Assert.AreEqual("Cleaning", activities[0].Name);
            Assert.AreEqual(_workingOutId, activities[1].Id);
            Assert.AreEqual("Working Out", activities[1].Name);
        }

        /// <summary>
        /// Tests the list activities for playlist method.
        /// </summary>
        [Test]
        public void ListActivitiesForPlaylistTest()
        {
            var activities = _activityRepository.ListActivities(_dataSource, _playlist);
            Assert.NotNull(activities);
            Assert.AreEqual(2, activities.Count);
            Assert.AreEqual(_cleaningId, activities[0].Id);
            Assert.AreEqual("Cleaning", activities[0].Name);
            Assert.AreEqual(_workingOutId, activities[1].Id);
            Assert.AreEqual("Working Out", activities[1].Name);
        }

        /// <summary>
        /// Tests the delete activity from song method.
        /// </summary>
        [Test]
        public void DeleteActivityFromSongTest()
        {
            var chilling = _activityRepository.GetActivity(_dataSource, _chillingId);
            Assert.NotNull(chilling);
            var activities = _activityRepository.ListActivities(_dataSource, _wholeLottaLove);
            Assert.NotNull(activities);
            Assert.AreEqual(2, activities.Count);

            _activityRepository.AddActivityTo(_dataSource, chilling, _wholeLottaLove);

            activities = _activityRepository.ListActivities(_dataSource, _wholeLottaLove);
            Assert.AreEqual(3, activities.Count);
            Assert.AreEqual(_chillingId, activities[0].Id);

            _activityRepository.DeleteActivityFrom(_dataSource, chilling, _wholeLottaLove);

            activities = _activityRepository.ListActivities(_dataSource, _wholeLottaLove);
            Assert.AreEqual(2, activities.Count);
            Assert.AreEqual(_cleaningId, activities[0].Id);
        }

        /// <summary>
        /// Tests the delete activity from album method.
        /// </summary>
        [Test]
        public void DeleteActivityFromAlbumTest()
        {
            var chilling = _activityRepository.GetActivity(_dataSource, _chillingId);
            Assert.NotNull(chilling);
            var activities = _activityRepository.ListActivities(_dataSource, _blackSabbathAlbum);
            Assert.NotNull(activities);
            Assert.AreEqual(1, activities.Count);

            _activityRepository.AddActivityTo(_dataSource, chilling, _blackSabbathAlbum);

            activities = _activityRepository.ListActivities(_dataSource, _blackSabbathAlbum);
            Assert.AreEqual(2, activities.Count);
            Assert.AreEqual(_chillingId, activities[0].Id);

            _activityRepository.DeleteActivityFrom(_dataSource, chilling, _blackSabbathAlbum);

            activities = _activityRepository.ListActivities(_dataSource, _blackSabbathAlbum);
            Assert.AreEqual(1, activities.Count);
            Assert.AreEqual(_workingOutId, activities[0].Id);
        }

        /// <summary>
        /// Tests the delete activity from playlist method.
        /// </summary>
        [Test]
        public void DeleteActivityFromPlaylistTest()
        {
            var chilling = _activityRepository.GetActivity(_dataSource, _chillingId);
            Assert.NotNull(chilling);
            var activities = _activityRepository.ListActivities(_dataSource, _playlist);
            Assert.NotNull(activities);
            Assert.AreEqual(2, activities.Count);

            _activityRepository.AddActivityTo(_dataSource, chilling, _playlist);

            activities = _activityRepository.ListActivities(_dataSource, _playlist);
            Assert.AreEqual(3, activities.Count);
            Assert.AreEqual(_chillingId, activities[0].Id);

            _activityRepository.DeleteActivityFrom(_dataSource, chilling, _playlist);

            activities = _activityRepository.ListActivities(_dataSource, _playlist);
            Assert.AreEqual(2, activities.Count);
            Assert.AreEqual(_cleaningId, activities[0].Id);
        }

        /// <summary>
        /// Tests the delete activity method.
        /// </summary>
        [Test]
        public void DeleteActivityTest()
        {
            var videoGames = Activity.Create("Video Games");
            _activityRepository.AddActivity(_dataSource, videoGames);
            _activityRepository.AddActivityTo(_dataSource, videoGames, _wholeLottaLove);
            _activityRepository.AddActivityTo(_dataSource, videoGames, _blackSabbathSong);
            _activityRepository.AddActivityTo(_dataSource, videoGames, _ledZeppelinII);
            _activityRepository.AddActivityTo(_dataSource, videoGames, _blackSabbathAlbum);
            _activityRepository.AddActivityTo(_dataSource, videoGames, _playlist);

            videoGames = _activityRepository.GetActivity(_dataSource, videoGames.Id);
            Assert.NotNull(videoGames);

            _activityRepository.DeleteActivity(_dataSource, videoGames);

            videoGames = _activityRepository.GetActivity(_dataSource, videoGames.Id);
            Assert.IsNull(videoGames);
        }
    }
}
