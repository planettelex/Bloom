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
        /// Tests the person properties.
        /// </summary>
        [Test]
        public void PersonPropertiesTest()
        {
            var id = Guid.NewGuid();
            var person = new Person
            {
                Id = id,
                Name = PersonName,
                Bio = "Bio",
                BornOn = DateTime.Parse("7/7/1957"),
                DiedOn = DateTime.Parse("1/15/2015"),
                Twitter = "Twitter"
            };

            var photo = Photo.Create("c:\\images\\photo.jpg");
            person.ProfileImage = photo;

            Assert.AreEqual(person.Id, id);
            Assert.AreEqual(person.Name, PersonName);
            Assert.AreEqual(person.Bio, "Bio");
            Assert.AreEqual(person.Twitter, "Twitter");
            Assert.AreEqual(person.BornOn, DateTime.Parse("7/7/1957"));
            Assert.AreEqual(person.DiedOn, DateTime.Parse("1/15/2015"));
            Assert.AreEqual(person.ProfileImage.Id, photo.Id);
            Assert.AreEqual(person.ProfileImage.FilePath, "c:\\images\\photo.jpg");

            var photo2 = Photo.Create("c:\\images\\photo2.jpg");
            person.ProfileImage = photo2;

            Assert.AreEqual(person.ProfileImage.Id, photo2.Id);
            Assert.AreEqual(person.ProfileImage.FilePath, "c:\\images\\photo2.jpg");

            person.Photos.Clear();
            person.ProfileImage = photo;

            Assert.AreEqual(person.ProfileImage.Id, photo.Id);
            Assert.AreEqual(person.ProfileImage.FilePath, "c:\\images\\photo.jpg");
        }

        /// <summary>
        /// Tests the person photo create method.
        /// </summary>
        [Test]
        public void CreatePersonPhotoTest()
        {
            var person = Person.Create(PersonName);
            var photo = Photo.Create("c:\\images\\photo.jpg");
            var personPhoto = PersonPhoto.Create(person, photo, 5);

            Assert.AreEqual(personPhoto.PersonId, person.Id);
            Assert.AreEqual(personPhoto.PhotoId, photo.Id);
            Assert.AreEqual(personPhoto.Priority, 5);
        }

        /// <summary>
        /// Tests the person reference create method.
        /// </summary>
        [Test]
        public void CreatePersonReferenceTest()
        {
            var person = Person.Create(PersonName);
            var reference = Reference.Create("Reference Title", "http://www.test.com");
            var personReference = PersonReference.Create(person, reference);

            Assert.AreEqual(personReference.PersonId, person.Id);
            Assert.AreEqual(personReference.ReferenceId, reference.Id);
        }
    }
}