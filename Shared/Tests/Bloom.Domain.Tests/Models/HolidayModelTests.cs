using System;
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
        /// <summary>
        /// Tests the holiday create method.
        /// </summary>
        [Test]
        public void CreateHolidayTest()
        {
            const string holidayName = "Test Holiday";
            var holiday = Holiday.Create(holidayName);

            Assert.AreNotEqual(holiday.Id, Guid.Empty);
            Assert.AreEqual(holiday.Name, holidayName);
        }
    }
}