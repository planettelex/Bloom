using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the time signature model class.
    /// </summary>
    [TestFixture]
    public class TimeSignatureModelTests
    {
        /// <summary>
        /// Tests the time signature create method.
        /// </summary>
        [Test]
        public void CreateTimeSignatureTest()
        {
            var timeSignature = TimeSignature.Create(4, NoteLength.Quarter);

            Assert.AreNotEqual(timeSignature.Id, Guid.Empty);
            Assert.AreEqual(timeSignature.Beats, 4);
            Assert.AreEqual(timeSignature.NoteLength, NoteLength.Quarter);
            Assert.AreEqual(timeSignature.ToString(), "4/4");
        }
    }
}