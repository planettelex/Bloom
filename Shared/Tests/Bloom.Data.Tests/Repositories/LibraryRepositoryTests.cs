using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the library repository class.
    /// </summary>
    [TestFixture]
    public class LibraryRepositoryTests
    {
        private const string TestFileName = "LibraryRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ILibraryRepository _libraryRepository;
        private IPersonRepository _personRepository;
        private Guid _libraryId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            var photoRepository = new PhotoRespository();
            _personRepository = new PersonRepository(photoRepository);
            _libraryRepository = new LibraryRepository(_personRepository);

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
            var owner = Person.Create("Jane Doe");
            var library = Library.Create(owner, "Test Library", "c:\\Music\\Libraries");
            library.OwnerLastConnected = DateTime.Now;
            _libraryId = library.Id;
            _libraryRepository.AddLibrary(_dataSource, library);
        }

        /// <summary>
        /// Tests the get library method.
        /// </summary>
        [Test]
        public void GetLibraryTest()
        {
            var library = _libraryRepository.GetLibrary(_dataSource);
            Assert.NotNull(library);
            Assert.AreEqual(_libraryId, library.Id);
            Assert.AreEqual("Test Library", library.Name);
            Assert.AreEqual("c:\\Music\\Libraries", library.FolderPath);
            Assert.LessOrEqual(library.OwnerLastConnected, DateTime.Now);
            Assert.Greater(library.OwnerLastConnected, DateTime.Now.AddMinutes(-1));
            Assert.NotNull(library.Owner);
            Assert.AreEqual("Jane Doe", library.Owner.Name);
        }
    }
}
