using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the genre repository class.
    /// </summary>
    [TestFixture]
    public class GenreRespositoryTests
    {
        private const string TestFileName = "GenreRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IGenreRepository _genreRepository;
        private Guid _bluesId;
        private Guid _jazzId;
        private Guid _rocknrollId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            _genreRepository = new GenreRepository();

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
            var blues = Genre.Create("Blues");
            _bluesId = blues.Id;
            _genreRepository.AddGenre(_dataSource, blues);

            var jazz = Genre.Create("Jazz");
            _jazzId = jazz.Id;
            _genreRepository.AddGenre(_dataSource, jazz);

            var rocknroll = Genre.Create("Rock n' Roll");
            rocknroll.ParentGenre = blues;
            _rocknrollId = rocknroll.Id;
            _genreRepository.AddGenre(_dataSource, rocknroll);
        }

        /// <summary>
        /// Tests the get genre method.
        /// </summary>
        [Test]
        public void GetGenreTest()
        {
            var genre = _genreRepository.GetGenre(_dataSource, _jazzId);
            Assert.NotNull(genre);
            Assert.AreEqual(_jazzId, genre.Id);
            Assert.AreEqual("Jazz", genre.Name);

            genre = _genreRepository.GetGenre(_dataSource, _rocknrollId);
            Assert.NotNull(genre);
            Assert.AreEqual(_rocknrollId, genre.Id);
            Assert.AreEqual("Rock n' Roll", genre.Name);
            Assert.AreEqual(_bluesId, genre.ParentGenreId);
            Assert.NotNull(genre.ParentGenre);
            Assert.AreEqual(_bluesId, genre.ParentGenre.Id);
            Assert.AreEqual("Blues", genre.ParentGenre.Name);
        }

        /// <summary>
        /// Tests the list genres method.
        /// </summary>
        [Test]
        public void ListGenresTest()
        {
            var genres = _genreRepository.ListGenres(_dataSource);
            Assert.NotNull(genres);
            Assert.AreEqual(3, genres.Count);
            Assert.AreEqual(_bluesId, genres[0].Id);
            Assert.AreEqual(_jazzId, genres[1].Id);
            Assert.AreEqual(_rocknrollId, genres[2].Id);
        }

        /// <summary>
        /// Tests the delete genre method.
        /// </summary>
        [Test]
        public void DeleteGenreTest()
        {
            var genre4 = Genre.Create("R&B");
            genre4.ParentGenreId = _bluesId;
            var genre4Id = genre4.Id;
            _genreRepository.AddGenre(_dataSource, genre4);

            var genre = _genreRepository.GetGenre(_dataSource, genre4Id);
            Assert.NotNull(genre);
            Assert.NotNull(genre.ParentGenre);

            _genreRepository.DeleteGenre(_dataSource, genre);

            var deletedGenre = _genreRepository.GetGenre(_dataSource, genre4Id);
            Assert.IsNull(deletedGenre);

            var parentGenre = _genreRepository.GetGenre(_dataSource, _bluesId);
            Assert.NotNull(parentGenre);
        }
    }
}
