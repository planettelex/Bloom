using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the source repository class.
    /// </summary>
    [TestFixture]
    public class SourceRespositoryTests
    {
        private const string TestFileName = "SourceRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ISourceRepository _sourceRepository;
        private Guid _allMusicId;
        private Guid _discogsId;
        private Guid _wikipediaId;
        private Guid _lastFmId;
        private Guid _gutterbubblesId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            _sourceRepository = new SourceRepository();

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
            var allMusic = Source.Create("AllMusic.com");
            _allMusicId = allMusic.Id;
            allMusic.Type = SourceType.Publication;
            allMusic.WebsiteUrl = "http://www.allmusic.com";
            _sourceRepository.AddSource(_dataSource, allMusic);

            var discogs = Source.Create("Discogs");
            _discogsId = discogs.Id;
            discogs.Type = SourceType.Marketplace;
            discogs.WebsiteUrl = "http://www.discogs.com";
            _sourceRepository.AddSource(_dataSource, discogs);

            var wikipedia = Source.Create("Wikipedia");
            _wikipediaId = wikipedia.Id;
            wikipedia.Type = SourceType.Wiki;
            wikipedia.WebsiteUrl = "http://www.wikipedia.org";
            _sourceRepository.AddSource(_dataSource, wikipedia);

            var lastFm = Source.Create("Last FM");
            _lastFmId = lastFm.Id;
            lastFm.Type = SourceType.Service;
            lastFm.WebsiteUrl = "http://last.fm";
            _sourceRepository.AddSource(_dataSource, lastFm);

            var gutterbubbles = Source.Create("Gutterbubbles");
            _gutterbubblesId = gutterbubbles.Id;
            gutterbubbles.Type = SourceType.Blog;
            gutterbubbles.WebsiteUrl = "http://www.gutterbubbles.com";
            _sourceRepository.AddSource(_dataSource, gutterbubbles);
        }

        /// <summary>
        /// Tests the source exists method.
        /// </summary>
        [Test]
        public void SourceExistsTest()
        {
            Assert.IsFalse(_sourceRepository.SourceExists(_dataSource, Guid.NewGuid()));
            Assert.IsTrue(_sourceRepository.SourceExists(_dataSource, _gutterbubblesId));
        }

        /// <summary>
        /// Tests the get source method.
        /// </summary>
        [Test]
        public void GetSourceTest()
        {
            var source = _sourceRepository.GetSource(_dataSource, _gutterbubblesId);
            Assert.NotNull(source);
            Assert.AreEqual(_gutterbubblesId, source.Id);
            Assert.AreEqual("Gutterbubbles", source.Name);
            Assert.AreEqual(SourceType.Blog, source.Type);
            Assert.AreEqual("http://www.gutterbubbles.com", source.WebsiteUrl);
        }

        /// <summary>
        /// Tests the list sources method.
        /// </summary>
        [Test]
        public void ListSourcesTest()
        {
            var sources = _sourceRepository.ListSources(_dataSource);
            Assert.NotNull(sources);
            Assert.AreEqual(5, sources.Count);
            Assert.AreEqual(_allMusicId, sources[0].Id);
            Assert.AreEqual(_discogsId, sources[1].Id);
            Assert.AreEqual(_gutterbubblesId, sources[2].Id);
            Assert.AreEqual(_lastFmId, sources[3].Id);
            Assert.AreEqual(_wikipediaId, sources[4].Id);
        }

        /// <summary>
        /// Tests the delete source method.
        /// </summary>
        [Test]
        public void DeleteSourceTest()
        {
            var pitchfork = Source.Create("Pitchfork");
            var pitchforkId = pitchfork.Id;
            pitchfork.Type = SourceType.Publication;
            pitchfork.WebsiteUrl = "http://www.pitchfork.com";
            _sourceRepository.AddSource(_dataSource, pitchfork);

            var source = _sourceRepository.GetSource(_dataSource, pitchforkId);
            Assert.NotNull(source);

            _sourceRepository.DeleteSource(_dataSource, source);

            var deletedSource = _sourceRepository.GetSource(_dataSource, pitchforkId);
            Assert.IsNull(deletedSource);
        }
    }
}
