using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the mood model class.
    /// </summary>
    [TestFixture]
    public class MoodModelTests
    {
        /// <summary>
        /// Tests the mood create method.
        /// </summary>
        [Test]
        public void CreateMoodTest()
        {
            var mood = Mood.Create("Test Mood");

            Assert.AreNotEqual(mood.Id, Guid.Empty);
            Assert.AreEqual(mood.Name, "Test Mood");
        }

        /// <summary>
        /// Tests the mood properties.
        /// </summary>
        [Test]
        public void MoodPropertiesTest()
        {
            var moodId = Guid.NewGuid();

            var activity = new Mood
            {
                Id = moodId,
                Name = "Test Mood"
            };

            Assert.AreEqual(activity.Id, moodId);
            Assert.AreEqual(activity.Name, "Test Mood");
        }

        /// <summary>
        /// Tests the mood to string method.
        /// </summary>
        [Test]
        public void MoodToStringTest()
        {
            var mood = Mood.Create("Test Mood");

            Assert.AreEqual(mood.ToString(), "Test Mood");
        }
    }
}