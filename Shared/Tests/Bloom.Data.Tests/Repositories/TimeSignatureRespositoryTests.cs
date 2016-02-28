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
    /// Tests the time signature repository class.
    /// </summary>
    [TestFixture]
    public class TimeSignatureRespositoryTests
    {
        private const string TestFileName = "TimeSignatureRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private ITimeSignatureRepository _timeSignatureRepository;
        private Guid _timeSignatureId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            _timeSignatureRepository = new TimeSignatureRepository();

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
            var timeSignature = TimeSignature.Create(5, BeatLength.Quarter);
            _timeSignatureId = timeSignature.Id;
            _timeSignatureRepository.AddTimeSignature(_dataSource, timeSignature);
        }

        /// <summary>
        /// Tests the get time signature methods.
        /// </summary>
        [Test]
        public void GetTimeSignatureTest()
        {
            var timeSignature = _timeSignatureRepository.GetTimeSignature(_dataSource, _timeSignatureId);
            Assert.NotNull(timeSignature);
            Assert.AreEqual(_timeSignatureId, timeSignature.Id);
            Assert.AreEqual(5, timeSignature.BeatsPerMeasure);
            Assert.AreEqual(BeatLength.Quarter, timeSignature.BeatLength);

            timeSignature = _timeSignatureRepository.GetTimeSignature(_dataSource, 5, BeatLength.Quarter);
            Assert.NotNull(timeSignature);
            Assert.AreEqual(_timeSignatureId, timeSignature.Id);
            Assert.AreEqual(5, timeSignature.BeatsPerMeasure);
            Assert.AreEqual(BeatLength.Quarter, timeSignature.BeatLength);
        }

        /// <summary>
        /// Tests the delete time signature method.
        /// </summary>
        [Test]
        public void DeleteTimeSignatureTest()
        {
            var timeSignature = TimeSignature.Create(4, BeatLength.Quarter);
            var timeSignatureId = timeSignature.Id;
            _timeSignatureRepository.AddTimeSignature(_dataSource, timeSignature);

            Assert.NotNull(timeSignature);
            Assert.AreEqual(timeSignatureId, timeSignature.Id);

            _timeSignatureRepository.DeleteTimeSignature(_dataSource, timeSignature);
            Assert.NotNull(timeSignature);

            var deletedTimeSignature = _timeSignatureRepository.GetTimeSignature(_dataSource, timeSignatureId);
            Assert.IsNull(deletedTimeSignature);
        }
    }
}
