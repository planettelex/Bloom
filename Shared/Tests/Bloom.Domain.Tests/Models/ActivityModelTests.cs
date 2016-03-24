using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the activity model classes.
    /// </summary>
    [TestFixture]
    public class ActivityModelTests
    {
        /// <summary>
        /// Tests the activity create method.
        /// </summary>
        [Test]
        public void CreateActivityTest()
        {
            var activity = Activity.Create("Test Activity");

            Assert.AreNotEqual(activity.Id, Guid.Empty);
            Assert.AreEqual(activity.Name, "Test Activity");
        }

        /// <summary>
        /// Tests the activity properties.
        /// </summary>
        [Test]
        public void ActivityPropertiesTest()
        {
            var activityId = Guid.NewGuid();

            var activity = new Activity
            {
                Id = activityId,
                Name = "Test Activity"
            };

            Assert.AreEqual(activity.Id, activityId);
            Assert.AreEqual(activity.Name, "Test Activity");
        }

        /// <summary>
        /// Tests the activity to string method.
        /// </summary>
        [Test]
        public void ActivityToStringTest()
        {
            var activity = Activity.Create("Test Activity");

            Assert.AreEqual(activity.ToString(), "Test Activity");
        }
    }
}