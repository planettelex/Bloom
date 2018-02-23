using System;
using System.IO;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Bloom.Data.Tests.Repositories
{
    /// <summary>
    /// Tests the photo repository class.
    /// </summary>
    [TestFixture]
    public class PhotoRespositoryTests
    {
        private const string TestFileName = "PhotoRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IPhotoRespository _photoRespository;
        private Guid _photo1Id;
        private Guid _photo2Id;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            _photoRespository = new PhotoRespository();

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
            var photo1 = Photo.Create("c:\\photos\\photo-1.png");
            _photo1Id = photo1.Id;
            _photoRespository.AddPhoto(_dataSource, photo1);

            var photo2 = Photo.Create("c:\\photos\\photo-2.png");
            _photo2Id = photo2.Id;
            photo2.Caption = "Photo Two";
            photo2.TakenOn = DateTime.Parse("2/2/2002");
            _photoRespository.AddPhoto(_dataSource, photo2);
        }

        /// <summary>
        /// Tests the photo exists method.
        /// </summary>
        [Test]
        public void PhotoExistsTest()
        {
            Assert.IsFalse(_photoRespository.PhotoExists(_dataSource, Guid.NewGuid()));
            Assert.IsTrue(_photoRespository.PhotoExists(_dataSource, _photo1Id));

            Assert.IsFalse(_photoRespository.PhotoExists(_dataSource, "c:\\no-file.jpg"));
            Assert.IsTrue(_photoRespository.PhotoExists(_dataSource, "c:\\Photos\\Photo-1.png"));
        }

        /// <summary>
        /// Tests the get photo method.
        /// </summary>
        [Test]
        public void GetPhotoTest()
        {
            var photo = _photoRespository.GetPhoto(_dataSource, _photo2Id);
            Assert.NotNull(photo);
            Assert.AreEqual(_photo2Id, photo.Id);
            Assert.AreEqual("c:\\photos\\photo-2.png", photo.FilePath);
            Assert.AreEqual("Photo Two", photo.Caption);
            Assert.AreEqual(DateTime.Parse("2/2/2002"), photo.TakenOn);

            photo = _photoRespository.GetPhoto(_dataSource, "c:\\photos\\photo-1.png");
            Assert.NotNull(photo);
            Assert.AreEqual(_photo1Id, photo.Id);
            Assert.AreEqual("c:\\photos\\photo-1.png", photo.FilePath);
            Assert.IsNull(photo.Caption);
            Assert.IsNull(photo.TakenOn);
        }

        /// <summary>
        /// Tests the delete photo method.
        /// </summary>
        [Test]
        public void DeletePhotoTest()
        {
            var photo3 = Photo.Create("c:\\photos\\photo-3.png");
            var photo3Id = photo3.Id;
            photo3.Caption = "Photo Three";
            photo3.TakenOn = DateTime.Parse("3/3/2003");
            _photoRespository.AddPhoto(_dataSource, photo3);

            var photo = _photoRespository.GetPhoto(_dataSource, photo3Id);
            Assert.NotNull(photo);

            _photoRespository.DeletePhoto(_dataSource, photo);

            var deletedPhoto = _photoRespository.GetPhoto(_dataSource, photo3Id);
            Assert.IsNull(deletedPhoto);
        }
    }
}
