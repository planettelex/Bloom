using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the holiday model class.
    /// </summary>
    [TestFixture]
    public class HolidayModelTests
    {
        const string HolidayName = "Test Holiday";

        /// <summary>
        /// Tests the holiday create method.
        /// </summary>
        [Test]
        public void CreateHolidayTest()
        {
            var holiday = Holiday.Create(HolidayName);

            Assert.AreNotEqual(holiday.Id, Guid.Empty);
            Assert.AreEqual(holiday.Name, HolidayName);
        }

        /// <summary>
        /// Tests the holiday properties.
        /// </summary>
        [Test]
        public void HolidayPropertiesTest()
        {
            var id = Guid.NewGuid();
            var holiday = new Holiday
            {
                Id = id,
                Name = HolidayName,
                Description = "Holiday description.",
                StartMonth = Month.March,
                StartDay = 10,
                EndMonth = Month.April,
                EndDay = 11
            };

            Assert.AreEqual(holiday.Id, id);
            Assert.AreEqual(holiday.Name, HolidayName);
            Assert.AreEqual(holiday.Description, "Holiday description.");
            Assert.AreEqual(holiday.StartMonth, Month.March);
            Assert.AreEqual(holiday.StartDay, 10);
            Assert.AreEqual(holiday.EndMonth, Month.April);
            Assert.AreEqual(holiday.EndDay, 11);
        }
    }
}