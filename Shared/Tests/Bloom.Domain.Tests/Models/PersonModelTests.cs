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
            var personPhoto1 = person.AddPhoto(photo1);
            var personPhoto2 = person.AddPhoto(photo2);

            Assert.AreEqual(person.Photos.Count, 2);
            Assert.AreEqual(personPhoto1.PersonId, person.Id);
            Assert.AreEqual(personPhoto1.PhotoId, photo1.Id);
            Assert.AreEqual(personPhoto1.Photo.Url, url1);
            Assert.AreEqual(personPhoto1.Priority, 1);
            Assert.AreEqual(personPhoto2.PersonId, person.Id);
            Assert.AreEqual(personPhoto2.PhotoId, photo2.Id);
            Assert.AreEqual(personPhoto2.Photo.Url, url2);
            Assert.AreEqual(personPhoto2.Priority, 2);
        }

        /// <summary>
        /// Tests adding a reference to a person.
        /// </summary>
        [Test]
        public void AddReferenceToPersonTest()
        {
            const string referenceName = "Test Reference";
            const string referenceUrl = "http://www.test.com/";
            var person = Person.Create(PersonName);
            var reference = Reference.Create(referenceName, referenceUrl);
            var artistReference = person.AddReference(reference);

            Assert.AreEqual(person.References.Count, 1);
            Assert.AreEqual(artistReference.PersonId, person.Id);
            Assert.AreEqual(artistReference.ReferenceId, reference.Id);
            Assert.AreEqual(artistReference.Reference.Name, referenceName);
            Assert.AreEqual(artistReference.Reference.Url, referenceUrl);
        }
    }
}