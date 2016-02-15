using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the filterset model class.
    /// </summary>
    [TestFixture]
    public class FiltersetModelTests
    {
        /// <summary>
        /// Tests the filterset create method.
        /// </summary>
        [Test]
        public void CreateFiltersetTest()
        {
            var filterset = Filterset.Create();

            Assert.AreNotEqual(filterset.Id, Guid.Empty);
        }
    }
}