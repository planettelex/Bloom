using System;
using Bloom.Domain.Models;
using NUnit.Framework;

namespace Bloom.Domain.Tests.Models
{
    /// <summary>
    /// Tests for the person model class.
    /// </summary>
    [TestFixture]
    public class PersonModelTests
    {
        const string PersonName = "Test Person";

        /// <summary>
        /// Tests the person create method.
        /// </summary>
        [Test]
        public void CreatePersonTest()
        {
            var person = Person.Create(PersonName);

            Assert.AreNotEqual(person.Id, Guid.Empty);
            Assert.AreEqual(person.Name, PersonName);
        }

        /// <summary>
        /// Tests adding photo to a person.
        /// </summary>
        [Test]
        public void AddPhotoToPersonTest()
        {
            const string url1 = "http://www.test.com/image1.jpg";
            const string url2 = "http://www.test.com/image2.jpg";
            var person = Person.Create(PersonName);
            var photo1 = Photo.Create(url1);
            var photo2 = Photo.Create(url2);
            // Todo

            Assert.AreEqual(person.Photos.Count, 2);
        }
    }
}