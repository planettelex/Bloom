using System.IO;
using System.Windows;
using Bloom.Domain.Models;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.State.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the player state repository class.
    /// </summary>
    [TestFixture]
    public class PlayerStateRespositoryTests
    {
        private const string TestFileName = "PlayerStateRespositoryTests.blms";
        private StateDataSource _dataSource;
        private IUnityContainer _container;
        private IPlayerStateRepository _playerStateRepository;
        private User _user;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _user = User.Create(Person.Create("Test User"));
            _container = new UnityContainer();
            _dataSource = new StateDataSource(_container);
            var connectionRepository = new LibraryConnectionRepository(_dataSource);
            _playerStateRepository = new PlayerStateRepository(_dataSource, connectionRepository);

            var testFolder = Bloom.Data.Settings.TestsDataPath;
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);

            var testFilePath = Path.Combine(testFolder, TestFileName);
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);

            _dataSource.Create(testFilePath);
            Assert.IsFalse(_playerStateRepository.PlayerStateExists(_user));
            PopulateDataSource();
        }

        /// <summary>
        /// Populates the data source.
        /// </summary>
        private void PopulateDataSource()
        {
            var playerState = new PlayerState
            {
                ProcessName = "Player",
                SkinName = "Skin",
                RecentWidth = 100,
                UpcomingWidth = 200,
                WindowState = WindowState.Maximized,
                UserId = _user.PersonId,
                User = _user
            };

            _playerStateRepository.AddPlayerState(playerState);
            _dataSource.Save();
        }

        /// <summary>
        /// Tests the get player state method.
        /// </summary>
        [Test]
        public void GetPlayerStateTest()
        {
            var playerState = _playerStateRepository.GetPlayerState(_user);
            Assert.NotNull(playerState);
            Assert.AreEqual("Player", playerState.ProcessName);
            Assert.AreEqual("Skin", playerState.SkinName);
            Assert.AreEqual(100, playerState.RecentWidth);
            Assert.AreEqual(200, playerState.UpcomingWidth);
            Assert.AreEqual(WindowState.Maximized, playerState.WindowState);
            Assert.AreEqual(_user.PersonId, playerState.UserId);
        }

        /// <summary>
        /// Tests the player state exists method.
        /// </summary>
        [Test]
        public void PlayerStateExistsTest()
        {
            Assert.IsTrue(_playerStateRepository.PlayerStateExists(_user));
        }
    }
}
