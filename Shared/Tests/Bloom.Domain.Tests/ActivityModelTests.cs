using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests
{
    /// <summary>
    /// Tests for the activity model class.
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
            const string activityName = "Test Activity";
            var activity = Activity.Create(activityName);

            Assert.AreNotEqual(activity.Id, Guid.Empty);
            Assert.AreEqual(activity.Name, activityName);
        }
    }
}