using System;
using System.IO;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.State.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the tab repository class.
    /// </summary>
    [TestFixture]
    public class TabRespositoryTests
    {
        private const string TestFileName = "TabRespositoryTests.blms";
        private StateDataSource _dataSource;
        private IUnityContainer _container;
        private IUserRepository _userRepository;
        private ITabRepository _tabRepository;
        private Guid _thomId;
        private User _thomYorke;
        private readonly Guid _thomLibraryId = Guid.Parse("43d08fa4-2cbb-48a4-9379-f4942c1d4424");
        private Guid _jonnyId;
        private readonly Guid _jonnyLibraryId = Guid.Parse("ecf1a318-3565-47df-ad59-951e92fae7bd");
        private Guid _testTabId;
        private Buid _testTabBuid;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new StateDataSource(_container);
            _userRepository = new UserRepository(_dataSource);
            _tabRepository = new TabRepository(_dataSource);

            var testFolder = Bloom.Data.Settings.TestsDataPath;
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
            _thomYorke = User.Create(Person.Create("Thom York"));
            _thomId = _thomYorke.PersonId;
            _thomYorke.Birthday = DateTime.Parse("10/7/1968");
            _thomYorke.Twitter = "@thomyorke";
            _thomYorke.ProfileImagePath = "c:\\images\\profiles\\thom-yorke.jpg";
            _thomYorke.LastLogin = DateTime.Now.AddDays(-7);
            _userRepository.AddUser(_thomYorke);

            var thomAlbumBuid = new Buid(_thomLibraryId, BloomEntity.Album, Guid.NewGuid());
            var thomBrowserTab1 = Tab.Create(ProcessType.Browser, _thomYorke, thomAlbumBuid, 1, TabType.Album, "OK Computer");
            _tabRepository.AddTab(thomBrowserTab1);

            var thomSongBuid = new Buid(_thomLibraryId, BloomEntity.Song, Guid.NewGuid());
            var thomBrowserTab2 = Tab.Create(ProcessType.Browser, _thomYorke, thomSongBuid, 2, TabType.Song, "Paranoid Android");
            _tabRepository.AddTab(thomBrowserTab2);

            var thomLibraryBuid = new Buid(_thomLibraryId, BloomEntity.Library, _thomLibraryId);
            var thomBrowserTab3 = Tab.Create(ProcessType.Browser, _thomYorke, thomLibraryBuid, 3, TabType.Song, "Thom's Library", "Spines");
            _tabRepository.AddTab(thomBrowserTab3);

            var thomAnalyticsTab1 = Tab.Create(ProcessType.Analytics, _thomYorke, Buid.Empty, 1, TabType.Home, "Home");
            _tabRepository.AddTab(thomAnalyticsTab1);

            var thomArtistBuid = new Buid(_thomLibraryId, BloomEntity.Artist, _thomLibraryId);
            var thomAnalyticsTab2 = Tab.Create(ProcessType.Analytics, _thomYorke, thomArtistBuid, 2, TabType.Artist, "Radiohead", "Charts");
            _tabRepository.AddTab(thomAnalyticsTab2);

            var jonnyGreenwood = User.Create(Person.Create("Jonny Greenwood"));
            _jonnyId = jonnyGreenwood.PersonId;
            jonnyGreenwood.Birthday = DateTime.Parse("11/5/1971");
            jonnyGreenwood.Twitter = "@JnnyG";
            jonnyGreenwood.ProfileImagePath = "c:\\images\\profiles\\jonny-greenwood.jpg";
            jonnyGreenwood.LastLogin = DateTime.Now.AddDays(-9);
            _userRepository.AddUser(jonnyGreenwood);

            var jonnyBrowserTab1 = Tab.Create(ProcessType.Browser, jonnyGreenwood, Buid.Empty, 1, TabType.GettingStarted, "Getting Started");
            _tabRepository.AddTab(jonnyBrowserTab1);

            var jonnyLibraryBuid = new Buid(_jonnyLibraryId, BloomEntity.Library, _jonnyLibraryId);
            var jonnyBrowserTab2 = Tab.Create(ProcessType.Browser, jonnyGreenwood, jonnyLibraryBuid, 2, TabType.Library, "Jonny's Library", "Grid");
            _tabRepository.AddTab(jonnyBrowserTab2);

            _testTabBuid = new Buid(_jonnyLibraryId, BloomEntity.Artist, Guid.NewGuid());
            var jonnyAnalyticsTab1 = Tab.Create(ProcessType.Analytics, jonnyGreenwood, _testTabBuid, 1, TabType.Artist, "Jonny Greenwood", "Slideshow");
            _testTabId = jonnyAnalyticsTab1.Id;
            _tabRepository.AddTab(jonnyAnalyticsTab1);

            var jonnyPersonBuid = new Buid(_jonnyLibraryId, BloomEntity.Person, Guid.NewGuid());
            var jonnyAnalyticsTab2 = Tab.Create(ProcessType.Analytics, jonnyGreenwood, jonnyPersonBuid, 2, TabType.Person, "Jonny Greenwood");
            _tabRepository.AddTab(jonnyAnalyticsTab2);

            _dataSource.Save();
        }

        /// <summary>
        /// Tests the get tab method.
        /// </summary>
        [Test]
        public void GetTabTest()
        {
            var tab = _tabRepository.GetTab(_testTabId);
            Assert.NotNull(tab);
            Assert.AreEqual(_testTabId, tab.Id);
            Assert.AreEqual(_testTabBuid.EntityId, tab.EntityId);
            Assert.AreEqual(TabType.Artist, tab.Type);
            Assert.AreEqual(_jonnyId, tab.UserId);
            Assert.AreEqual("Jonny Greenwood", tab.Header);
            Assert.AreEqual(1, tab.Order);
            Assert.AreEqual(ProcessType.Analytics, tab.Process);
            Assert.AreEqual("Slideshow", tab.View);
        }

        /// <summary>
        /// Tests the list tabs method.
        /// </summary>
        [Test]
        public void ListTabsTest()
        {
            var thomBrowserTabs = _tabRepository.ListTabs(ProcessType.Browser, _thomId);
            Assert.NotNull(thomBrowserTabs);
            Assert.AreEqual(3, thomBrowserTabs.Count);

            var thomAnalyticsTabs = _tabRepository.ListTabs(ProcessType.Analytics, _thomId);
            Assert.NotNull(thomAnalyticsTabs);
            Assert.AreEqual(2, thomAnalyticsTabs.Count);

            var jonnyBrowserTabs = _tabRepository.ListTabs(ProcessType.Browser, _jonnyId);
            Assert.NotNull(jonnyBrowserTabs);
            Assert.AreEqual(2, jonnyBrowserTabs.Count);

            var jonnyAnalyticsTabs = _tabRepository.ListTabs(ProcessType.Analytics, _jonnyId);
            Assert.NotNull(jonnyAnalyticsTabs);
            Assert.AreEqual(2, jonnyAnalyticsTabs.Count);

            var nobodyBrowserTabs = _tabRepository.ListTabs(ProcessType.Browser, Guid.NewGuid());
            Assert.IsNull(nobodyBrowserTabs);

            var thomNoApplicationTabs = _tabRepository.ListTabs(ProcessType.None, _thomId);
            Assert.IsNull(thomNoApplicationTabs);
        }

        /// <summary>
        /// Tests the delete tab method.
        /// </summary>
        [Test]
        public void DeleteTabTest()
        {
            var thomAlbumBuid = new Buid(_thomLibraryId, BloomEntity.Album, Guid.NewGuid());
            var thomBrowserTab4 = Tab.Create(ProcessType.Browser, _thomYorke, thomAlbumBuid, 4, TabType.Album, "Kid A");
            _tabRepository.AddTab(thomBrowserTab4);

            _dataSource.Save();

            var thomBrowserTabs = _tabRepository.ListTabs(ProcessType.Browser, _thomId);
            Assert.NotNull(thomBrowserTabs);
            Assert.AreEqual(4, thomBrowserTabs.Count);

            var newTab = _tabRepository.GetTab(thomBrowserTab4.Id);
            Assert.NotNull(newTab);
            Assert.AreEqual("Kid A", newTab.Header);

            _tabRepository.DeleteTab(newTab);
            _dataSource.Save();

            thomBrowserTabs = _tabRepository.ListTabs(ProcessType.Browser, _thomId);
            Assert.NotNull(thomBrowserTabs);
            Assert.AreEqual(3, thomBrowserTabs.Count);

            var deletedTab = _tabRepository.GetTab(thomBrowserTab4.Id);
            Assert.IsNull(deletedTab);
        }
    }
}
