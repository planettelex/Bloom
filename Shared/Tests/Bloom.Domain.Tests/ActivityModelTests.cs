using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests
{
    [TestFixture]
    public class ActivityModelTests
    {
        [Test]
        public void CreateActivityTest()
        {
            var activity = Activity.Create("Test");
            Assert.AreNotEqual(activity.Id, Guid.Empty);
            Assert.AreEqual(activity.Name, "Test");
        }
    }
}