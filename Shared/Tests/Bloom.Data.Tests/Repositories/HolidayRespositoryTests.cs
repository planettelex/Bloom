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
    /// Tests the holiday repository class.
    /// </summary>
    [TestFixture]
    public class HolidayRespositoryTests
    {
        private const string TestFileName = "HolidayRespositoryTests.blm";
        private LibraryDataSource _dataSource;
        private IUnityContainer _container;
        private IHolidayRepository _holidayRepository;
        private Guid _christmasId;
        private Guid _halloweenId;
        private Guid _thanksgivingId;

        /// <summary>
        /// Sets up the tests by creating a test data source and adding data.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            _container = new UnityContainer();
            _dataSource = new LibraryDataSource(_container);
            _holidayRepository = new HolidayRepository();

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
            var christmas = Holiday.Create("Christmas");
            _christmasId = christmas.Id;
            christmas.Description = "Christmas description";
            christmas.StartDay = 1;
            christmas.StartMonth = Month.December;
            christmas.EndDay = 26;
            christmas.EndMonth = Month.December;
            _holidayRepository.AddHoliday(_dataSource, christmas);

            var halloween = Holiday.Create("Halloween");
            _halloweenId = halloween.Id;
            halloween.Description = "Halloween description";
            halloween.StartDay = 1;
            halloween.StartMonth = Month.October;
            halloween.EndDay = 31;
            halloween.EndMonth = Month.October;
            _holidayRepository.AddHoliday(_dataSource, halloween);

            var thanksgiving = Holiday.Create("Thanksgiving");
            _thanksgivingId = thanksgiving.Id;
            thanksgiving.Description = "Thanksgiving description";
            thanksgiving.StartDay = 22;
            thanksgiving.StartMonth = Month.November;
            thanksgiving.EndDay = 26;
            thanksgiving.EndMonth = Month.November;
            _holidayRepository.AddHoliday(_dataSource, thanksgiving);
        }

        /// <summary>
        /// Tests the get holiday method.
        /// </summary>
        [Test]
        public void GetHolidayTest()
        {
            var christmas = _holidayRepository.GetHoliday(_dataSource, _christmasId);
            Assert.NotNull(christmas);
            Assert.AreEqual(_christmasId, christmas.Id);
            Assert.AreEqual("Christmas", christmas.Name);
            Assert.AreEqual("Christmas description", christmas.Description);
            Assert.AreEqual(1, christmas.StartDay);
            Assert.AreEqual(Month.December, christmas.StartMonth);
            Assert.AreEqual(26, christmas.EndDay);
            Assert.AreEqual(Month.December, christmas.EndMonth);
        }

        /// <summary>
        /// Tests the list holidays method.
        /// </summary>
        [Test]
        public void ListHolidaysTest()
        {
            var holidays = _holidayRepository.ListHolidays(_dataSource);
            Assert.NotNull(holidays);
            Assert.AreEqual(3, holidays.Count);
            Assert.AreEqual(_christmasId, holidays[0].Id);
            Assert.AreEqual(_halloweenId, holidays[1].Id);
            Assert.AreEqual(_thanksgivingId, holidays[2].Id);
        }

        /// <summary>
        /// Tests the delete holiday method.
        /// </summary>
        [Test]
        public void DeleteHolidayTest()
        {
            var newYears = Holiday.Create("New Year's");
            var newYearsId = newYears.Id;
            newYears.Description = "New Year's description";
            newYears.StartDay = 31;
            newYears.StartMonth = Month.December;
            newYears.EndDay = 2;
            newYears.EndMonth = Month.January;
            _holidayRepository.AddHoliday(_dataSource, newYears);

            var holiday = _holidayRepository.GetHoliday(_dataSource, newYearsId);
            Assert.NotNull(holiday);

            _holidayRepository.DeleteHoliday(_dataSource, holiday);

            var deletedHoliday = _holidayRepository.GetHoliday(_dataSource, newYearsId);
            Assert.IsNull(deletedHoliday);
        }
    }
}
