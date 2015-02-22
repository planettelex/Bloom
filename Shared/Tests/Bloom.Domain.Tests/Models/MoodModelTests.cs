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
            const string moodName = "Test Mood";
            var mood = Mood.Create(moodName);

            Assert.AreNotEqual(mood.Id, Guid.Empty);
            Assert.AreEqual(mood.Name, moodName);
        }
    }
}